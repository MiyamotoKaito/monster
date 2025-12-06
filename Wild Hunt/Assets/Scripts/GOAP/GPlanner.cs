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
        private bool BuildGraph(GNode parent,
            List<GNode> leaves,
            List<IAction> usebleAction,
            Dictionary<string, int> goal)
        {
            bool foundPath = false;
            foreach (var act in usebleAction)
            {
                if (act.PerCondition())
            }

        }

        private void CanGoalAchieved()
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
