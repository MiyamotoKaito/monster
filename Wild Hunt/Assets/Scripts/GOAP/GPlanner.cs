using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
namespace GOAP.GPlanner
{
    public static class GPlanner
    {
        public static Queue<IAction> Planning(List<IAction> actions,
                                       Dictionary<string, int> goal,
                                       Dictionary<string, int> currentState)
        {
            //利用可能なアクションのリスト
            List<IAction> usableActions = new List<IAction>();
            //利用可能なアクションをフィルタリングする
            foreach (var action in actions)
            {
                //アクションが現在のワールドステートで実行可能かの確認を行う処理を追加しないといけない
                usableActions.Add(action);
                Debug.WriteLine($"Usable Action: {action.GetType().Name}");
            }
            //グラフ構築に必要なリスト
            List<GNode> leaves = new List<GNode>();//グラフ構築で達成した「葉」ノード(計算パスの最終候補)
            //最初のノード
            GNode start = new GNode(null, 0, currentState, null);

            //A*アルゴリズムでグラフを構築し、最もコストの低いゴールパスを見つける
            if (BuildGraph(start, leaves, usableActions, goal))
            {
                //ゴールを達成できる一番コストが掛からないノードを探す
                GNode cheapest = null;
                foreach (GNode leaf in leaves)
                {
                    if (leaf == null || leaf.Cost < cheapest.Cost)
                    {
                        cheapest = leaf;
                        Debug.WriteLine($"Cheapest Action: {leaf.Action.GetType().Name} with cost {leaf.Cost}");
                    }
                }
                //最適パスを再構築して返す
                Debug.WriteLine("Reconstructing Path...");
                return ReconstructPath(cheapest);
            }

            //ゴールを達成できなかった場合はnullを返す
            Debug.WriteLine("No Plan Found.");
            return null;
        }
        /// <summary>
        /// A*アルゴリズムでグラフを再帰的に構築する
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="leaves"></param>
        /// <param name="usableAction"></param>
        /// <param name="goal"></param>
        /// <returns></returns>
        private static bool BuildGraph(GNode parent,
            List<GNode> leaves,
            List<IAction> usableAction,
            Dictionary<string, int> goal)
        {
            bool foundPath = false;

            //利用可能なアクションを1つづつチェック
            foreach (var act in usableAction)
            {
                // 1. アクションの前提条件が親ノードのステートで満たされているかを確認
                if (CheckPreconditions(act.Preconditions, parent.State))
                {
                    Debug.WriteLine($"Action {act.GetType().Name} can be performed.");
                    // 2. 新しい状態のシミュレーション
                    // 親ノードの状態にアクションの効果を適用した新しいワールドステートの辞書作成
                    Dictionary<string, int> newState = CreateNewState(parent.State, act.Effects);
                    
                    // 3. ノードの作成
                    int newCost = parent.Cost + act.Cost;
                    GNode newNode = new GNode(parent, newCost, newState, act);

                    // 4. ゴール達成度チェック

                    if (CanGoalAchieved(goal, newState))
                    {
                        //ゴール達成可能なノードをleavesに追加
                        Debug.WriteLine($"Goal Achieved with Action {act.GetType().Name}!");
                        leaves.Add(newNode);
                        foundPath = true;//パスが見つかった
                    }
                    else
                    {
                        // 5. 引数に渡されたアクションをのぞいた残りのアクションで次のノードを探す
                        //　新しいリストを作成し、現在のアクションを一時的に除外
                        Debug.WriteLine($"Goal not yet achieved. Continuing with Action {act.GetType().Name}.");
                        List<IAction> subset = usableAction.Except(new[] { act }).ToList();

                        // 再帰呼び出し: 新しいノードを親として、サブセットのアクションでグラフを構築
                        if (BuildGraph(newNode, leaves, subset, goal))
                        {
                            Debug.WriteLine($"Path found through Action {act.GetType().Name}.");
                            foundPath = true;
                        }
                    }
                }
            }
            Debug.WriteLine("BuildGraph completed for current parent node.");
            return foundPath;
        }

        /// <summary>
        /// ゴールが現在のステートで達成されたか判断する
        /// </summary>
        /// <param name="goal"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        private static bool CanGoalAchieved(Dictionary<string, int> goal, Dictionary<string, int> state)
        {
            foreach (var g in goal)
            {
                //ゴールのKeyがステートに存在し、かつ値が一致するか
                //もしくはゴールの値が0で‘Remove’の場合を考慮
                if (state.ContainsKey(g.Key) && state[g.Key] == g.Value)
                {
                    Debug.WriteLine($"Goal condition {g.Key}={g.Value} met in state.");
                    continue;
                }
                else
                {
                    Debug.WriteLine($"Goal condition {g.Key}={g.Value} NOT met in state.");
                    return false;
                }
            }
            Debug.WriteLine("All goal conditions met in state.");
            return true;
        }
        /// <summary>
        /// アクションの前提条件が現在のステートで満たされいるか判断する
        /// </summary>
        /// <param name="preconditions"></param>
        /// <param name="currentState"></param>
        /// <returns></returns>
        private static bool CheckPreconditions(Dictionary<string, int> preconditions, Dictionary<string, int> currentState)
        {
            foreach (var pre in preconditions)
            {
                //アクションの前提条件に必要なステートのキーか値がアクションが要求する値と一致しているかチェックする
                if (!currentState.ContainsKey(pre.Key) || currentState[pre.Key] != pre.Value)
                {
                    Debug.WriteLine($"Precondition {pre.Key}={pre.Value} NOT met in current state.");
                    return false;
                }
            }
            Debug.WriteLine("All preconditions met in current state.");
            return true;
        }

        /// <summary>
        /// 現在のステートにアクションの効果を適応し、新しいステートを作成する
        /// </summary>
        /// <param name="currentState"></param>
        /// <param name="effects"></param>
        /// <returns></returns>
        private static Dictionary<string, int> CreateNewState(Dictionary<string, int> currentState, Dictionary<string, int> effects)
        {
            //現在のステートのコピーを作る
            Dictionary<string, int> newState = new(currentState);

            //アクションの効果を適応し、ステートを上書き/保存
            foreach (var effect in effects)
            {
                Debug.WriteLine($"Applying effect {effect.Key}={effect.Value} to new state.");
                newState[effect.Key] = effect.Value;
            }
            Debug.WriteLine("New state created after applying effects.");
            return newState;
        }
        /// <summary>
        /// 受け取ったゴールノードから親ノードを辿り、アクションのパスを再構築する
        /// </summary>
        /// <param name="cheapestGoalNode"></param>
        /// <returns></returns>
        private static Queue<IAction> ReconstructPath(GNode cheapestGoalNode)
        {
            List<IAction> path = new List<IAction>();
            GNode current = cheapestGoalNode;

            //スタートノードにたどり着くまで子から親をたどる
            while (current.Action != null)
            {
                Debug.WriteLine($"Reconstructing path, adding Action: {current.Action.GetType().Name}");
                path.Add(current.Action);
                current = current.Parent;
            }
            Debug.WriteLine("Path reconstruction completed.");
            path.Reverse();//リストを逆にして、正規の実行順序にする
            return new Queue<IAction>(path);
        }
    }
}