using System.Collections.Generic;
using UnityEngine;

public class Eat : ActionBase, IAction
{
    public Dictionary<string, int> Preconditions => new Dictionary<string, int>() { { _precondition.ToString(), 1 } };

    public Dictionary<string, int> Effects => new Dictionary<string, int>() { { _subGoal.ToString(), 1 }, { "HasTarget", 0 }, {"AtTarget", 0 } };

    public int Cost => _cost;

    [SerializeField] private string _targetTag;
    [SerializeField] private WorldStateType _precondition;
    [SerializeField] private SubGoalType _subGoal;

    private Transform _target;
    private Dictionary<string, int> _preconditions;
    private Dictionary<string, int> _effects;
    public bool CheckPrecondition(GAgent agent)
    {
        return _target != null;
    }
    public void Execute(GAgent agent)
    {
        _target = agent.TargetObj.transform;
    }
    public bool Perform(GAgent agent)
    {
        // ターゲットが消滅していたら失敗
        if (_target == null) return false;

        // SurvivalStatsに通知して空腹タイマーをリセット
        var survivalStats = agent.GetComponent<SurvivalStats>();
        if (survivalStats != null)
        {
            survivalStats.OnAte();
        }
        else
        {
            Debug.LogWarning("EAT: SurvivalStatsコンポーネントが見つかりません");
        }

        // 食べ物を消去
        GameObject.Destroy(_target.gameObject);
        return true; // 完了
    }
}
