using System.Collections.Generic;
using UnityEngine;

public class Drink : ActionBase, IAction
{
    public Dictionary<string, int> Preconditions => new Dictionary<string, int>() { { _precondition.ToString(), 1 } };

    public Dictionary<string, int> Effects => new Dictionary<string, int>() { { _subGoal.ToString(), 1 } };

    public int Cost => _cost;


    [SerializeField] private string _targetTag;
    [SerializeField] private float _drinkAmount;
    [SerializeField] private WorldStateType _precondition;
    [SerializeField] private SubGoalType _subGoal;

    private Transform _target;
    //private Dictionary<string, int> _conditions = new Dictionary<string, int>() { { _precondition.ToString() } } 
    //private Dictionary<string, int>_effect = new Dictionary<string, int>();

    public bool CheckPrecondition(GAgent agent)
    {
        return _target != null;
    }

    public void Execute(GAgent agent)
    {
        _target = agent.TargetObj.transform;

        if (_target == null)
        {
            Debug.Log("<color=red>飲み物のターゲットが見つかりませんでした</color>");
        }
    }

    public bool Perform(GAgent agent)
    {
        if(!_target) return false;

        var survival = agent.GetComponent<SurvivalStats>();
        if (survival != null)
        {
            survival.OnDrank();
        }
        else
        {
            Debug.LogWarning("Drink: SurvivalStatsコンポーネントが見つかりません");
        }

        GameObject.Destroy(_target.gameObject);
        return true;
    }
}