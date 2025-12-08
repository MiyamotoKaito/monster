using System.Collections.Generic;
using UnityEngine;
namespace GOAP.GPlanner
{
    public class GPlanner : MonoBehaviour
    {
        public Queue<IAction> Planning(List<IAction> actions,
                                       Dictionary<string, int> goal,
                                       Dictionary<string, int> currntState)
        {
            //利用可能なアクションのリスト
            List<IAction> usableActions = new List<IAction>();
            //利用可能なアクションをフィルタリングする
            foreach (var action in actions)
            {
                //※修正
                //アクションが現在のワールドステートで実行可能かの確認を行う処理を追加しないといけない
                usableActions.Add(action);
            }
            //グラフ構築に必要なリスト
            List<GNode> leaves = new List<GNode>();//グラフ構築で達成した「葉」ノード(計算パスの最終候補)
            //最初のノード
            GNode start = new GNode(null, 0, currntState, null);

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
                    }
                }
                //最適パスを再構築して返す
                return ReconstructPath(cheapest);
            }

            //ゴールを達成できなかった場合はnullを返す
            return null;
        }
        /// <summary>
        /// A*アルゴリズムでグラフを再帰的に構築する
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="leaves"></param>
        /// <param name="usebleAction"></param>
        /// <param name="goal"></param>
        /// <returns></returns>
        private bool BuildGraph(GNode parent,
            List<GNode> leaves,
            List<IAction> usebleAction,
            Dictionary<string, int> goal)
        {
            bool foundPath = false;

            //利用可能なアクションを1つづつチェック
            foreach (var act in usebleAction)
            {
                // 1. アクションの前提条件が親ノードのステートで満たされているかを確認
                // act.PerCondition(parent.State) の実装がIActionインターフェースに必要ですが、
                // IActionインターフェースは IWorldState を引数にとるので、
                // ここでは一旦、現在のノードの状態とアクションの前提条件を比較するヘルパー関数を使用します。

                // 【注意】 IActionインターフェースのPerCondition/EffectはIWorldStateを引数にとっていますが、
                // 実際にはアクションごとに「このステートがこの値なら実行可能」というチェックが必要です。
                // ここでは、そのチェックを `CheckPreconditions` で抽象化します。


            }

        }

        private void CanGoalAchieved()
        {

        }
        /// <summary>
        /// アクションの前提条件が現在のステートで満たされいるか判断する
        /// </summary>
        /// <param name="preconditions"></param>
        /// <param name="currentState"></param>
        /// <returns></returns>
        private bool CheckPreconditions(Dictionary<string, int> preconditions, Dictionary<string, int> currentState)
        {
            foreach (var pre in preconditions)
            {
                //アクションの前提条件に必要なステートがあるかチェックする
                if (!currentState.ContainsKey(pre.Key))
                {
                    return false;
                }
            }
            return true;
        }

        private Dictionary<string, int> CreateNewState()
        {

        }
        /// <summary>
        /// 受け取ったゴールノードから親ノードを辿り、アクションのパスを再構築する
        /// </summary>
        /// <param name="cheapestGoalNode"></param>
        /// <returns></returns>
        private Queue<IAction> ReconstructPath(GNode cheapestGoalNode)
        {
            List<IAction> path = new List<IAction>();
            GNode current = cheapestGoalNode;

            while (current.Action != null)
            {
                path.Add(current.Action);
                current = current.Parent;
            }
            path.Reverse();
            return new Queue<IAction>(path);
        }
    }
}
