using GOAP.WorldStates;
using UnityEngine;
public class Bite : IAction
{
    [SerializeField]
    private int _priority;

    [SerializeReference, SubclassSelector]
    private IWorldState _state;
    public bool PerCondition(IWorldState worldState)
    {
        return worldState.Value <= 100 ? true : false;
    }
    public bool Effect(IWorldState worldState)
    {
        throw new System.NotImplementedException();
    }
}
