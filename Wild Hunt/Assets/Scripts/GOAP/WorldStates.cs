using System.Collections.Generic;
using UnityEngine;

namespace GOAP.WorldStates
{
    /// <summary>
    /// ワールドステートの状態を管理するクラス
    /// </summary>
    public class WorldStates : MonoBehaviour
    {
        public static WorldStates Instance;

        [SerializeField]
        [Header("ワールドステートデータ")]
        private WorldStatesData _worldStatesData;

        private List<IWorldState> _worldStates = new();
        private Dictionary<string, int> _worldStateDictionary = new();

        private void Awake()
        {
            //IWorldStateのリストと辞書を初期化
            foreach (var data in _worldStatesData.WorldStates)
            {
                _worldStates.Add(data);
            }
            foreach (var state in _worldStates)
            {
                _worldStateDictionary.Add(state.Name, state.Value);
            }
        }
        /// <summary>
        /// ステートを持っているか判別する
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool HasState(string name)
        {
            return _worldStateDictionary.ContainsKey(name);
        }
        /// <summary>
        /// ステートを追加する
        /// </summary>
        /// <param name="name"></param>
        public void AddState(string name, int value)
        {
            if (!_worldStateDictionary.ContainsKey(name))
            {
                _worldStateDictionary.Add(name, value);
            }
        }
        /// <summary>
        /// ステートを削除する
        /// </summary>
        /// <param name="name"></param>
        public void RemoveState(string name)
        {
            if (_worldStateDictionary.ContainsKey(name))
            {
                _worldStateDictionary.Remove(name);
            }
        }
        /// <summary>
        /// ステートの値を変更する
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void ModifyState(string name, int value)
        {
            if (_worldStateDictionary.ContainsKey(name))
            {
                _worldStateDictionary[name] = value;
            }
            else
            {
                AddState(name, value);
            }
        }
        /// <summary>
        /// ステートの値を上書きする
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void SetState(string name, int value)
        {
            if (_worldStateDictionary.ContainsKey(name))
            {
                _worldStateDictionary[name] = value;
            }
            else
            {
                _worldStateDictionary.Add(name, value);
            }
        }
        /// <summary>
        /// ワールドステートの辞書を返す
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int> GetStates()
        {
            return _worldStateDictionary;
        }
    }
}
