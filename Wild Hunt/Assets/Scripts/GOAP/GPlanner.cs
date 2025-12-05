using System.Collections.Generic;
using UnityEngine;
namespace GOAP.GPlanner
{
    public class GPlanner : MonoBehaviour
    {
        public Queue<IAction> Planning(List<IAction> actions,
                                       Dictionary<string, int> goal,
                                       Dictionary<string, int>currntState)
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
            List<GNode> leaves = new List<GNode>();
            GNode start = new GNode(null, 0, currntState, null);
        }
        public bool BuildGraph(GNode parent,
            List<GNode> leaves,
            List<IAction> usebleAction,
            Dictionary<string, int> goal)
        {
            bool foundPath = false;
            foreach (var act in usebleAction)
            {
                if(act.PerCondition())
            }

        }

        private void CanGoalAchieved()
        {

        }
    }
}
