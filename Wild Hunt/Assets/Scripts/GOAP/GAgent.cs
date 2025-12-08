using UnityEngine;
using System.Collections.Generic;
using GOAP.WorldStates;
using GOAP.GPlanner;

public class GAgent : MonoBehaviour
{
    /// <summary>サブゴールと優先度の辞書<summary>
    private Dictionary<GSubGoal, int> _subGoals = new();
    /// <summary>ソートされたアクションのQueue<summary>
    private Queue<IAction> _actionQueue = new();

    [SerializeField]
    [Header("アクションのデータ")]
    private MonsterActionsData _actionsData;

    [SerializeField, ReadOnly]
    private IAction _currentAction;
    [SerializeField, ReadOnly]
    private GSubGoal _currentGoal;


    private GPlanner _planner;
    private WorldStates _worldStates;

    private void Start()
    {
        _planner = GetComponent<GPlanner>();
        _worldStates = WorldStates.Instance;
    }

    private void LateUpdate()
    {
        // 1. ゴール達成済み、またはアクションキューがない場合は、新しい計画を立てる
        if (_actionQueue.Count == 0 || _currentGoal == null)
        {
            _currentAction = null;
        }
    }
}
