using System.Collections.Generic;
using UnityEngine;
public class Bite : IAction
{
    [SerializeField]
    private int _cost;
    [SerializeField]
    private int _effectValue;
    [SerializeReference, SubclassSelector]
    private IWorldState _state;

    public Dictionary<string, int> Preconditions => throw new System.NotImplementedException();

    public Dictionary<string, int> Effects => throw new System.NotImplementedException();

    public int Cost => throw new System.NotImplementedException();

    public bool PreCondition()
    {
        var value = _state.Value + _effectValue;
        return value <= 100 ? true : false;
    }
    public bool Effect()
    {
        throw new System.NotImplementedException();
    }
}