using GOAP.WorldState;
using UnityEngine;

public class Run : IAction
{
    [SerializeField]
    private string ActionName;
    [SerializeField]
    private int _priority;
    [SerializeField]
    private float _speed;

    public bool Effect(WorldState state)
    {
        throw new System.NotImplementedException();
    }

    public bool PerCondition(WorldState state)
    {
        throw new System.NotImplementedException();
    }
}
