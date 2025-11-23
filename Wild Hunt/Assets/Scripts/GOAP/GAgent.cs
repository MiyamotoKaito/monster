using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SubGoal
{
    // サブゴールの条件
    public Dictionary<string, int> SubGoals;

    // サブゴールが削除されるかどうかのフラグ
    // 一部のエージェントがユーザーに目標を与え、その目標が達成された時削除する場合に使用
    public bool Remove;

    //変数を初期化するコンストラクタ
    public SubGoal(Dictionary<string, int> goals, bool remove)
    {
        SubGoals = new Dictionary<string, int>(goals);
        Remove = remove;
    }
}

public class GAgent : MonoBehaviour
{
    //実行するアクションのリスト
    private List<GAction> _gActions = new List<GAction>();
    //サブゴールの辞書
    private Dictionary<SubGoal, int> _subGoals = new Dictionary<SubGoal, int>();

    private GPlanner _gPlanner;
    private Queue<GAction> _actionQueue;
    private GAction _currentAction;
    private SubGoal _currentGoal;
    private bool invoked;
    private void Awake()
    {
        //エージェントのアクションをすべて取得する
        GAction[] actions = GetComponents<GAction>();
        foreach (GAction a in actions)
        {
            _gActions.Add(a);
        }
    }
    /// <summary>
    /// アクションが実行され終わったら呼ぶ関数
    /// </summary>
    private void CompleteAction()
    {
        _currentAction._running = false;
        _currentAction.PostPerform();
        invoked = false;
    }
    private void LateUpdate()
    {
        //現在のアクションが存在し、実行中であれば
        if (_currentAction != null && _currentAction._running)
        {
            if (_currentAction._agent.hasPath && _currentAction._agent.remainingDistance < 1f)
            {
                if (!invoked)
                {
                    Invoke("CompleteAction", _currentAction._duration);
                    invoked = true;
                }
            }
            return;
        }
        //アクションキューが存在しているか、アクションが残っていれば
        if (_gPlanner != null || _actionQueue != null)
        {
            _gPlanner = new GPlanner();

            //優先度が高いサブゴールから降順にソートする
            var sortedGoals = _subGoals.OrderByDescending(x => x.Value);
            
            foreach (var sg in sortedGoals)
            {
                _actionQueue = _gPlanner.Plan(_gActions, sg.Key.SubGoals, null);
                if (_actionQueue != null)
                {
                    _currentGoal = sg.Key;
                    break;
                }
            }
        }
    }
}
