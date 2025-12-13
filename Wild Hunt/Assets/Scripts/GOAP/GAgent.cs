using UnityEngine;
using System.Collections.Generic;
using GOAP.WorldStates;
using GOAP.GPlanner;
using System.Linq;

public class GAgent : MonoBehaviour
{
    /// <summary>サブゴールと優先度の辞書<summary>
    private Dictionary<int, GSubGoal> _subGoals = new();
    /// <summary>ソートされたアクションのQueue<summary>
    private Queue<IAction> _actionQueue = new();

    [SerializeField]
    [Header("アクションのデータ")]
    private MonsterActionsData _actionsData;

    private IAction _currentAction;
    private GSubGoal _currentGoal;
    private WorldStates _worldStates;

    private void Start()
    {
        _worldStates = WorldStates.Instance;
    }

    private void LateUpdate()
    {
        // 1. ゴール達成済み、またはアクションキューがない場合は、新しい計画を立てる
        if (_actionQueue.Count == 0 || _currentGoal == null)
        {
            //アクションをリセット
            _currentAction = null;

            // 2. 優先度の高い順にゴールをチェックし、計画を試みる
            GSubGoal bestGoal = null;
            var sortedGoals = _subGoals.OrderByDescending(entry => entry.Key); // Key = 優先度

            foreach (var entry in sortedGoals)
            {
                GSubGoal goal = entry.Value;
                if (GoalAchieved(goal.SubGoals))
                {
                    if (goal.Remove)
                    {
                        //リストが変更されたので、次のフレームで再チェック
                        _subGoals.Remove(entry.Key);
                        return;
                    }
                    continue;
                }
                //計画策定の実行
                Dictionary<string, int> currentState = _worldStates.GetStates();

                //Planningメソッドを呼び出して計画を取得
                Queue<IAction> plan = GPlanner.Planning(_actionsData.Actions.ToList(),
                                                        goal.SubGoals,
                                                        currentState);

                if (plan != null && plan.Count > 0)
                {
                    //最適な計画が見つかった場合、アクションキューを更新
                    _actionQueue = plan;
                    bestGoal = goal;
                    break;//最適な計画が見つかったのでループを抜ける
                }
            }
        }

        // 3. アクションの実行
        if (_actionQueue.Count > 0)
        {
            if (_currentAction == null)
            {
                // --- 修正点 1: 新しいアクションの開始 ---
                _currentAction = _actionQueue.Dequeue();

                // アクションがキューから取り出された直後に、初期設定のための Execute を一度だけ呼び出す
                _currentAction.Execute(this);
            }

            //アクションの前提条件を確認
            if (_currentAction.CheckPrecondition(this))
            {
                bool success = _currentAction.Perform(this);
                if (success)
                {
                    _currentAction = null; //アクション完了後にリセット
                }
                else
                {
                    //アクションが失敗した場合、計画をリセット
                    _actionQueue.Clear();
                }
            }
            else
            {
                Debug.Log("アクションの前提条件が満たされていません。計画をリセットします。");
                _actionQueue?.Clear();//計画をリセット
                _currentAction = null;
            }
        }
    }

    /// <summary>
    /// ゴールが現在のワールドステートで達成されているか確認する
    /// </summary>
    /// <param name="goal"></param>
    /// <returns></returns>
    private bool GoalAchieved(Dictionary<string, int> goal)
    {
        // 現在のワールドステートを取得
        Dictionary<string, int> currentState = _worldStates.GetStates();
        foreach (var entry in goal)
        {
            //キーが存在しない、または値が異なる場合、ゴールは達成されていない
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
        else
        {
            Debug.LogWarning($"ゴールの優先度 {priority} は既に存在します。");
        }
    }
}
