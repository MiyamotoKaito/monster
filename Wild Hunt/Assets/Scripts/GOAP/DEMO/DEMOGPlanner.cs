using System.Collections.Generic;
using GOAP.DEMO.DemoActions;
using GOAP.DEMO.WorldState;
using UnityEngine;
public class DEMONode
{
    // ノードの親ノード
    public DEMONode Parent;
    // ノードのコスト
    public float Cost;
    // ノードの状態
    public Dictionary<string, int> State;
    // ノードのアクション
    public DEMOGAction Action;

    // コンストラクタ
    public DEMONode(DEMONode parent, float cost, Dictionary<string, int> allState, DEMOGAction action)
    {
        Parent = parent;
        Cost = cost;
        State = allState;
        Action = action;
    }
}
/// <summary>
/// GOAPのプランナー
/// </summary>
public class DEMOGPlanner
{
    /// <summary>
    /// 最短経路のアクションの道をプランニングした戻り値Queue
    /// </summary>
    /// <param name="actions"></param>
    /// <param name="goal"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    public Queue<DEMOGAction> Plan(List<DEMOGAction> actions,
                                 Dictionary<string, int> goal,
                                 DEMOWorldState state)
    {
        // 利用可能なアクションのリスト
        List<DEMOGAction> usebleActions = new List<DEMOGAction>();
        // 利用可能なアクションをフィルタリング
        foreach (DEMOGAction action in actions)
        {
            if (action.IsAchievable())
            {
                usebleActions.Add(action);
            }
        }
        // 葉ノードのリスト
        List<DEMONode> leaves = new List<DEMONode>();

        // 開始ノードの作成
        //最初のノードなので親ノードはnull、コストは0、
        //状態はエージェントの現在の状態、アクションはnull
        DEMONode start = new DEMONode(null, 0, DEMOGWorld.Instance.GetWorld().GetStates(), null);

        bool success = BuildGraph(start, leaves, usebleActions, goal);
        if (!success)
        {
            Debug.Log("NO Plan");
            return null;
        }

        // 最小コストの葉ノード
        DEMONode cheapest = null;
        // 最小コストの葉ノードを見つける
        foreach (DEMONode leaf in leaves)
        {
            if (cheapest == null)
            {
                cheapest = leaf;
            }
            else
            {
                if (leaf.Cost < cheapest.Cost)
                {
                    cheapest = leaf;
                }
            }
        }
        // プランのアクションリスト
        List<DEMOGAction> result = new List<DEMOGAction>();
        DEMONode n = cheapest;
        // ルートノードまでさかのぼりアクションを収集
        while (n != null)
        {
            if (n.Action != null)
            {
                result.Insert(0, n.Action);
            }
            n = n.Parent;
        }
        // アクションリストをキューに変換
        Queue<DEMOGAction> queue = new Queue<DEMOGAction>();
        foreach (DEMOGAction a in result)
        {
            queue.Enqueue(a);
        }
        Debug.Log($"The Plan is : ");
        foreach (DEMOGAction a in queue)
        {
            Debug.Log($"Q : {a.name}");
        }
        // プランを返す
        return queue;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="leaves"></param>
    /// <param name="usebleActions"></param>
    /// <param name="goal"></param>
    /// <returns></returns>
    private bool BuildGraph(DEMONode parent,
                            List<DEMONode> leaves,
                            List<DEMOGAction> usebleActions,
                            Dictionary<string, int> goal)
    {
        // フラグ：パスが見つかったかどうか
        bool foundPath = false;
        // すべての利用可能なアクションをループ
        foreach (DEMOGAction action in usebleActions)
        {
            // アクションが親ノードの状態で実行可能かどうかをチェック
            if (action.IsAchievableGiven(parent.State))
            {
                // 新しい状態を作成
                Dictionary<string, int> currentState = new Dictionary<string, int>(parent.State);
                // アクションの効果を新しい状態に適用
                foreach (KeyValuePair<string, int> eff in action.effects)
                {
                    // 効果が既に存在する場合は更新、存在しない場合は追加
                    if (!currentState.ContainsKey(eff.Key))
                    {
                        // 効果が存在しない場合は追加
                        currentState.Add(eff.Key, eff.Value);
                    }
                }
                // 新しいノードを作成
                DEMONode node = new DEMONode(parent, parent.Cost + action.cost, currentState, action);
                // ゴールが達成されたかどうかをチェック
                if (GoalAchived(goal, currentState))
                {
                    leaves.Add(node);
                    foundPath = true;
                }
                // ゴールが達成されていない場合、再帰的にグラフを構築
                else
                {
                    List<DEMOGAction> subset = ActionSubSet(usebleActions, action);
                    bool found = BuildGraph(node, leaves, subset, goal);
                    if (found)
                    {
                        foundPath = true;
                    }
                }
            }
        }
        return foundPath;
    }
    ///ゴールが達成されたかどうかをチェック
    private bool GoalAchived(Dictionary<string, int> goal, Dictionary<string, int> state)
    {
        // ゴールの各条件をチェック
        foreach (KeyValuePair<string, int> g in goal)
        {
            // 状態にゴールの条件が存在しない場合、ゴールは達成されていない
            if (!state.ContainsKey(g.Key))
            {
                return false;
            }
        }
        // すべてのゴール条件が状態に存在する場合、ゴールは達成されている
        return true;
    }
    private List<DEMOGAction> ActionSubSet(List<DEMOGAction> usebleActions, DEMOGAction removeMe)
    {
        List<DEMOGAction> subset = new List<DEMOGAction>();
        foreach (DEMOGAction a in usebleActions)
        {
            if (!a.Equals(removeMe))
            {
                subset.Add(a);
            }
        }
        return subset;
    }
}
