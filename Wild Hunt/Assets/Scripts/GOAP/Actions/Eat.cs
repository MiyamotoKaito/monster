using System.Collections.Generic;
using UnityEngine;

public class Eat : IAction
{
    public Dictionary<string, int> Preconditions => new Dictionary<string, int>() { { "AtTarget", 1 } };

    public Dictionary<string, int> Effects => new Dictionary<string, int>() { { "IsFull", 1 }, { "HasTarget", 0 } };

    public int Cost => _cost;

    [SerializeField]
    private int _cost;
    private Transform _target;

    private bool _eat = false;
    public bool CheckPrecondition(GAgent agent)
    {
        return _target != null;
    }

    public bool Perform(GAgent agent)
    {
        if (!_eat) return false;


        //targetObjをどうにかして消す()
        return true;
    }

    public void Execute(GAgent agent)
    {
        _eat = true;
    }
}
