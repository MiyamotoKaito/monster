using GOAP.WorldState;
using UnityEngine;
public class Bite : IAction
{
    [SerializeField]
    private string ActionName;
    [SerializeField]
    private int _priority;

    public bool PerCondition(WorldState state)
    {
        throw new System.NotImplementedException();
    }

    public bool Effect(WorldState state)
    {
        throw new System.NotImplementedException();
    }
}
