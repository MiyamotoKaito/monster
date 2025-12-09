using System.Collections.Generic;
using System.Linq;
using GOAP.DEMO.DemoActions;
using UnityEngine;

public class DEMOSubGoal
{
    // サブゴールの条件
    public Dictionary<string, int> SubGoals;

    // サブゴールが削除されるかどうかのフラグ
    // 一部のエージェントがユーザーに目標を与え、その目標が達成された時削除する場合に使用
    public bool Remove;

    //変数を初期化するコンストラクタ
    public DEMOSubGoal(string goalName,int priority, bool remove)
    {
        SubGoals = new Dictionary<string, int>();
        SubGoals.Add(goalName, priority);
        Remove = remove;
    }
}

public class DEMOGAgent : MonoBehaviour
{
    //実行するアクションのリスト
    [SerializeField]
    protected List<DEMOGAction> _gActions = new List<DEMOGAction>();
    //サブゴールの辞書
    protected Dictionary<DEMOSubGoal, int> _subGoals = new Dictionary<DEMOSubGoal, int>();

    protected DEMOGPlanner _gPlanner;
    protected Queue<DEMOGAction> _actionQueue;
    [SerializeField]
    protected DEMOGAction _currentAction;
    protected DEMOSubGoal _currentGoal;
    protected bool invoked;
    protected void BaseAwake()
    {
        //エージェントのアクションをすべて取得する
        DEMOGAction[] actions = GetComponents<DEMOGAction>();
        foreach (DEMOGAction a in actions)
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
            _gPlanner = new DEMOGPlanner();

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

        //ゴールが達成された場合の処理
        if (_actionQueue != null && _actionQueue.Count == 0)
        {
            if (_currentGoal.Remove)
            {
                _subGoals.Remove(_currentGoal);
            }
            _gPlanner = null;
        }
        //アクションキュー取り出し
        if (_actionQueue != null && _actionQueue.Count > 0)
        {
            _currentAction = _actionQueue.Dequeue();
            if (_currentAction.PrePerform())
            {
                //特定のタグを持つオブジェクトをターゲットに設定する
                if (_currentAction._target == null && _currentAction._targetTag != "")
                {
                    _currentAction._target = GameObject.FindWithTag(_currentAction._targetTag);
                }
                //アクションを実行中に設定する
                if (_currentAction._targetTag != null)
                {
                    _currentAction._running = true;
                    _currentAction._agent.SetDestination(_currentAction._target.transform.position);
                }
            }
            //実行しようとしたアクションのいずれかが失敗した場合、
            //アクションキューをクリアして全体を再設計
            else
            {
                _actionQueue = null;
            }
        }
    }
}
