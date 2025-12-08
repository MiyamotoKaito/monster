using System.Collections.Generic;
using UnityEngine;

public class Eat : IAction
{
    [SerializeField]
    private int _cost;

    public Dictionary<string, int> Preconditions => throw new System.NotImplementedException();

    public Dictionary<string, int> Effects => throw new System.NotImplementedException();

    public int Cost => throw new System.NotImplementedException();

    public bool PerCondition(IWorldState worldState)
    {
        throw new System.NotImplementedException();
    }

    public bool Effect(IWorldState worldState)
    {
        throw new System.NotImplementedException();
    }
}
