using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldState
{
    public string Key;
    public int Value;
}

public class WorldStates
{
    public Dictionary<string, int> _states;
    //変数初期化用のコンストラクタ
    public WorldStates()
    {
        _states = new Dictionary<string, int>();
    }
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
    /// 引数に渡された値を辞書に登録する
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    private void AddState(string key, int value)
    {
        _states.Add(key, value);
    }
}
