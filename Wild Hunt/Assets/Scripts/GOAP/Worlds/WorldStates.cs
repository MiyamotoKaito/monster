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

}
