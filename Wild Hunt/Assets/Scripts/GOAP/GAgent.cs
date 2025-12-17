using UnityEngine;
using System.Collections.Generic;
using GOAP.WorldStates;
using GOAP.GPlanner;
using System.Linq;

public class GAgent : MonoBehaviour
{
    private Dictionary<int, GSubGoal> _subGoals = new();
    private Queue<IAction> _actionQueue = new();

    [SerializeField]
    [Header("アクションのデータ")]
    private MonsterActionsData _actionsData;

    private IAction _currentAction;
    private WorldStates _worldStates;

    // 計画再計算のためのクールダウン
    private float _planningTimer = 0f;
    private float _planningInterval = 0.2f;

    private void Start()
    {
        _worldStates = WorldStates.Instance;
    }

    private void LateUpdate()
    {
        if (_currentAction != null || _actionQueue.Count > 0)
        {
            _planningTimer = 0f;

            if (_currentAction == null)
            {
                _currentAction = _actionQueue.Dequeue();
                Debug.Log($"<color=yellow>[GAgent] 次のアクション開始: {_currentAction.GetType().Name}</color>");
                _currentAction.Execute(this);
            }

            // 前提条件チェック
            if (!_currentAction.CheckPrecondition(this))
            {
                Debug.LogError($"[GAgent] 実行エラー: {_currentAction.GetType().Name} の前提条件が実行中に崩れました。計画を破棄します。");
                _actionQueue.Clear();
                _currentAction = null;
                return;
            }

            // Performの実行
            if (_currentAction.Perform(this))
            {
                Debug.Log($"<color=lime>[GAgent] アクション完了: {_currentAction.GetType().Name}</color>");
                ApplyEffect(_currentAction.Effects);
                _currentAction = null;
            }
            return;
        }

        // プランニング処理
        _planningTimer += Time.deltaTime;
        if (_planningTimer < _planningInterval) return;
        _planningTimer = 0f;

        Dictionary<string, int> currentState = _worldStates.GetStates();
        var sortedGoals = _subGoals.OrderByDescending(entry => entry.Key).ToList();

        foreach (var entry in sortedGoals)
        {
            if (GoalAchieved(entry.Value.SubGoals, currentState)) continue;

            Queue<IAction> plan = GPlanner.Planning(_actionsData.Actions.ToList(), entry.Value.SubGoals, currentState);

            if (plan != null && plan.Count > 0)
            {
                _actionQueue = plan;
                Debug.Log("[GAgent] 新しい計画を採用しました。実行フェーズに移行します。");
                break;
            }
        }
    }

    // --- 以下、ヘルパーメソッド ---

    private bool GoalAchieved(Dictionary<string, int> goal, Dictionary<string, int> currentState)
    {
        foreach (var entry in goal)
        {
            if (!currentState.ContainsKey(entry.Key) || currentState[entry.Key] != entry.Value)
            {
                return false;
            }
        }
        return true;
    }

    public void AddSubGoal(int priority, GSubGoal goal)
    {
        if (!_subGoals.ContainsKey(priority))
        {
            _subGoals.Add(priority, goal);
        }
    }

    private void ApplyEffect(Dictionary<string, int> effect)
    {
        foreach (var entry in effect)
        {
            _worldStates.ModifyState(entry.Key, entry.Value);
        }
    }
}