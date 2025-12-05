using UnityEngine;
public class Bite : IAction
{
    [SerializeField]
    private int _cost;
    [SerializeField]
    private int _effectValue;
    [SerializeReference, SubclassSelector]
    private IWorldState _state;

    public bool PerCondition(IWorldState worldState)
    {
        var value = worldState.Value + _effectValue;
        return value <= 100 ? true : false;
    }
    public bool Effect(IWorldState worldState)
    {
        throw new System.NotImplementedException();
    }
}