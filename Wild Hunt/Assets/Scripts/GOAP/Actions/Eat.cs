using System.Collections.Generic;
using UnityEngine;

public class Eat : IAction
{
    [SerializeField]
    private int _cost;
    [SerializeReference, SubclassSelector]
    private IWorldState state;

    public Dictionary<string, int> Preconditions => throw new System.NotImplementedException();

    public Dictionary<string, int> Effects => throw new System.NotImplementedException();

    public int Cost => throw new System.NotImplementedException();

    public bool PreCondition()
    {
        if(worldState.Value <= )
    }

    public bool Effect()
    {
        throw new System.NotImplementedException();
    }
}
