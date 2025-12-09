using GOAP.WorldStates;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Run : IAction
{
    [SerializeField]
    private int _cost;
    [SerializeField]
    private float _speed;

    public Dictionary<string, int> Preconditions => throw new System.NotImplementedException();

    public Dictionary<string, int> Effects => throw new System.NotImplementedException();

    public int Cost => throw new System.NotImplementedException();

    public bool CheckPrecondition(GAgent agent)
    {
        throw new System.NotImplementedException();
    }

    public bool Perform(GAgent agent)
    {
        throw new System.NotImplementedException();
    }

    public void Execute(GAgent agent)
    {
        throw new System.NotImplementedException();
    }
}
