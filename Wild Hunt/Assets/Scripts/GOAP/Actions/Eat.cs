using GOAP.WorldState;
using UnityEngine;

public class Eat : IAction
{
    public float ED;

    [SerializeField]
    private string ActionName;
    [SerializeField]
    private int _priority;

    public bool Effect(WorldState state)
    {
        throw new System.NotImplementedException();
    }

    public bool PerCondition(WorldState state)
    {
        throw new System.NotImplementedException();
    }
}
