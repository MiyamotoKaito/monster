using UnityEngine;
using System.Collections.Generic;

public class GAgent : MonoBehaviour
{
    /// <summary>サブゴールと優先度の辞書<summary>
    private Dictionary<GSubGoal, int> _subGoals = new();
    /// <summary>ソートされたアクションのQueue<summary>
    private Queue<IAction> _actionQueue = new();


    [SerializeField, ReadOnly]
    private IAction _currentAction;
    [SerializeField, ReadOnly]
    private GSubGoal _currentGoal;

    private void 
}
