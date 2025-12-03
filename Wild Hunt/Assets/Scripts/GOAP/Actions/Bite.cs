using GOAP.WorldStates;
using UnityEngine;
public class Bite : IAction
{
    [SerializeField]
    private int _priority;

    public bool PerCondition(IWorldState worldState)
    {
        throw new System.NotImplementedException();
    }
    public bool Effect(IWorldState worldState)
    {
        throw new System.NotImplementedException();
    }
}
