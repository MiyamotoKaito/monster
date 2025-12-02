using GOAP.WorldState;
using UnityEngine;

namespace GOAP.GWorld
{
    /// <summary>
    /// グローバルワールドステート管理クラス
    /// </summary>
    public class GWorld : MonoBehaviour
    {
        /// <summary>GWorldのシングルトン </summary>
        public static GWorld Instance = new GWorld();
        private static WorldStates _worldStates;
        private GWorld()
        {
            _worldStates = new WorldStates();
        }
    }
}