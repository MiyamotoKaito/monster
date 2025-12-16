using System.Collections.Generic;
using UnityEngine;

namespace GOAP.WorldStates
{
    [System.Serializable]
    public class WorldState
    {
        public string WorldName;
        public int Value;
    }
    /// <summary>
    /// ワールドステートの状態を管理するクラス
    /// </summary>
    public class WorldStates : MonoBehaviour
    {
        public static WorldStates Instance;
        [SerializeField]
        private List<WorldState> _worldState;

        private Dictionary<string, int> _worldStateDictionary = new();

        private void Awake()
        {
            // 1. シングルトンの初期化
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            // 2. データのロードと重複チェック
            if (_worldState != null)
            {
                foreach (var state in _worldState)
                {
                    _worldStateDictionary.Add(state.WorldName, state.Value);
                }
            }
        }
        /// <summary>
        /// ステートを持っているか判別する
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool HasState(string name)
        {
            Debug.Log($"HasState: {name} = {_worldStateDictionary.ContainsKey(name)}");
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
                Debug.Log($"AddState: {name} = {value}");
                _worldStateDictionary.Add(name, value);
            }
            else
            {
                Debug.LogWarning($"State '{name}' already exists. Use ModifyState or SetState to change its value.");
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
                Debug.Log($"RemoveState: {name}");
            }
            else
            {
                Debug.LogWarning($"State '{name}' does not exist. Cannot remove.");
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
                Debug.Log($"ModifyState: {name} = {value}");
            }
            else
            {
                AddState(name, value);
                Debug.Log($"State '{name}' did not exist. Added with value {value}.");
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
                Debug.Log($"SetState: {name} = {value}");
            }
            else
            {
                _worldStateDictionary.Add(name, value);
                Debug.Log($"State '{name}' did not exist. Added with value {value}.");
            }
        }
        /// <summary>
        /// ワールドステートの辞書を返す
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int> GetStates()
        {
            // 常にコピーを返すようにして、プランナーが元の値を壊さないようにする
            return new Dictionary<string, int>(_worldStateDictionary);
        }
    }
}
