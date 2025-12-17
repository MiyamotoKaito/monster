using System.Collections.Generic;
using UnityEngine;

public class Eat : IAction
{
    public Dictionary<string, int> Preconditions => new Dictionary<string, int>() { { _precondition.ToString(), 1 } };

    public Dictionary<string, int> Effects => new Dictionary<string, int>() { { _subGoal.ToString(), 10 }, { "HasTarget", 0 } };

    public int Cost => _cost;

    [SerializeField]
    private int _cost;
    [SerializeField]
    private string _targetTag;
    [SerializeField]
    private WorldStateType _precondition;
    [SerializeField]
    private SubGoalType _subGoal;
    private Transform _target;

    private Dictionary<string, int> _preconditions;
    private Dictionary<string, int> _effects;

    private bool _eat = false;
    public bool CheckPrecondition(GAgent agent)
    {
        return _target != null;
    }

    public bool Perform(GAgent agent)
    {
        // ターゲットが消滅していたら失敗
        if (_target == null) return false;

        Debug.Log("EAT: 食べました");

        // SurvivalStatsに通知して空腹タイマーをリセット
        var survivalStats = agent.GetComponent<SurvivalStats>();
        if (survivalStats != null)
        {
            survivalStats.OnFed();
        }
        else
        {
            Debug.LogWarning("EAT: SurvivalStatsコンポーネントが見つかりません");
        }

        // 食べ物を消去
        GameObject.Destroy(_target.gameObject);

        return true; // 完了
    }

    public void Execute(GAgent agent)
    {
        GameObject targetobj = GameObject.FindGameObjectWithTag(_targetTag);
        if (targetobj != null)
        {
            _target = targetobj.transform;
            _eat = true;
        }
        else
        {
            _eat = false;
        }
    }
}
