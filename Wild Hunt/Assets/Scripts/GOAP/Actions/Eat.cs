using UnityEngine;

public class Eat : IAction
{
    [SerializeField]
    private int _cost;

    public bool PerCondition(IWorldState worldState)
    {
        throw new System.NotImplementedException();
    }

    public bool Effect(IWorldState worldState)
    {
        throw new System.NotImplementedException();
    }
}
