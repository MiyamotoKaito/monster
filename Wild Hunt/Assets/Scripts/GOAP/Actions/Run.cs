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
    public bool PerCondition(IWorldState worldState)
    {
        throw new System.NotImplementedException();
    }
    public bool Effect(IWorldState worldState)
    {
        throw new System.NotImplementedException();
    }

}
