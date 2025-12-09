using System.Collections.Generic;
using UnityEngine;
using GOAP.WorldStates;

// GAgentクラスの定義が必要ですが、ここではアクセスできるものとして実装します。
// public class GAgent : MonoBehaviour { public GameObject Target { get; set; } /* ... */ }

public class BiteAction : IAction
{
    // --- フィールド ---

    [Header("アクション設定")]
    [SerializeField] private int _cost = 5;
    [SerializeField] private int _damageAmount = 20;
    [SerializeField] private float _attackRange = 1.5f; // 噛みつき可能な距離
    [SerializeField] GameObject _target;

    private bool _hasBitten = false; // 噛みつきが完了したかどうか (実行中の状態管理)
    private const string TargetHealthKey = "TargetHealth";
    private const string TargetNearKey = "IsAtTarget";

    // --- 計画策定フェーズ (GPlannerが参照する静的な定義) ---

    public Dictionary<string, int> Preconditions => new()
    {
        // プランナー: アクションを実行する前に、ターゲットが近くにいる状態(1)を要求する
        { TargetNearKey, 1 }
    };

    public Dictionary<string, int> Effects => new()
    {
        // プランナー: アクションの結果、ターゲットのHPが減少する状態(-_damageAmount)になる
        // 注意: プランナーは加算をシミュレーションするため、-20 のように負の値を入れる
        { TargetHealthKey, -_damageAmount }
    };

    public int Cost => _cost;

    // --- 実行フェーズ (GAgentが参照する動的なロジック) ---

    public bool CheckPrecondition(GAgent agent)
    {
        // リアルタイムチェック: ターゲットが有効で、かつ十分に近接しているかを確認する
        if (_target == null) return false;

        // 距離チェック
        return Vector3.Distance(agent.transform.position, _target.transform.position) <= _attackRange;
    }

    public void Execute(GAgent agent)
    {
        // アクションがキューから取り出された直後に一度だけ実行される
        Debug.Log($"[BiteAction] 噛みつきアクションを開始します。ターゲット: {_target.name}");
        _hasBitten = false;

        // ターゲットを視界に入れる、アニメーション開始、チャージ時間開始など、初期設定を行う
        // agent.Animator.SetTrigger("StartBite");
    }

    public bool Perform(GAgent agent)
    {
        // 既に噛みつきが終わっていれば、すぐに完了を報告し、次のアクションへ
        if (_hasBitten) return true;

        // リアルタイムでターゲットの存在を再確認
        if (_target == null)
        {
            Debug.LogError("[BiteAction] ターゲットが見つかりません。アクション失敗。");
            return true; // 失敗として完了（GAgent側で計画破棄のロジックが動くことを期待）
        }

        // 1. 実際のダメージ処理（ゲーム世界の変更）

        // 例: ターゲットにダメージを与えるコンポーネントを呼び出す
        // agent.Target.GetComponent<HealthComponent>()?.TakeDamage(_damageAmount);

        // 2. ワールドステートの更新（GOAPシステムへのフィードバック）
        // 実行者であるエージェントが、世界の状況が変化したことをWorldStatesに伝達する
        WorldStates.Instance.ModifyState(TargetHealthKey, -_damageAmount);

        Debug.Log($"[BiteAction] ターゲット {_target.name} に {_damageAmount} ダメージを与え、WorldStateを更新しました。");

        // 噛みつきが完了したことをマークし、次のフレームでこのアクションの完了(true)を報告させる
        _hasBitten = true;

        // このフレームで完了したので true を返す (または、次のフレームで true を返すために false を返すロジックも考えられますが、ここでは即時完了とします)
        return true;
    }
}