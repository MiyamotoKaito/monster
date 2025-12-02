using System.Collections.Generic;
using UnityEngine;

namespace GOAP.WorldState
{
    /// <summary>
    /// ワールドステートの状態を表すクラス
    /// </summary>
    [System.Serializable]
    public class WorldState
    {
        /// <summary>ワールドステートの名前</summary>
        public string StateName => _stateName;
        /// <summary>ワールドステートの値</summary>
        public int Value => _value;
        [SerializeField]
        [Header("ワールドステートの名前")]
        private string _stateName;
        [SerializeField]
        [Header("ワールドステートの値")]
        private int _value;
    }
    /// <summary>
    /// ワールドステートの状態を管理するクラス
    /// </summary>
    public class WorldStates
    {
        public Dictionary<string, int> States => _states;
        [SerializeField]
        private Dictionary<string, int> _states = new Dictionary<string, int>();
        /// <summary>
        /// ステートを持っているか判別する
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool HasState(string key)
        {
            return _states.ContainsKey(key);
        }
        /// <summary>
        /// ステートを追加する
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddState(string name, int value)
        {
            if (!_states.ContainsKey(name))
            {
                _states.Add(name, value);
            }
        }
        /// <summary>
        /// ステートを削除する
        /// </summary>
        /// <param name="name"></param>
        public void RemoveState(string name)
        {
            if (_states.ContainsKey(name))
            {
                _states.Remove(name);
            }
        }
        /// <summary>
        /// ステートの値を変更する
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void ModifyState(string name, int value)
        {
            if (_states.ContainsKey(name))
            {
                _states[name] += value;
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
            if (_states.ContainsKey(name))
            {
                _states[name] = value;
            }
            else
            {
                _states.Add(name, value);
            }
        }
        /// <summary>
        /// ワールドステートの辞書を返す
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int> GetStates()
        {
            return _states;
        }
    }
}
