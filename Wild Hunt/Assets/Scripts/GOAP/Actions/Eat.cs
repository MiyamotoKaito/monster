using System.Collections.Generic;
using UnityEngine;

public class Eat : IAction
{
    public Dictionary<string, int> Preconditions => new Dictionary<string, int>() { { "AtTarget", 1 } };

    public Dictionary<string, int> Effects => new Dictionary<string, int>() { { "IsFull", 1 }, { "HasTarget", 0 } };

    public int Cost => _cost;

    [SerializeField]
    private int _cost;
    [SerializeField]
    private string _targetTag;
    private Transform _target;

    private bool _eat = false;
    public bool CheckPrecondition(GAgent agent)
    {
        return _target != null;
    }

    public bool Perform(GAgent agent)
    {
        if (!_eat || _target == null) return false;

        Debug.Log("EAT");
        //targetObjをどうにかして消す()
        return true;
    }

    public void Execute(GAgent agent)
    {
        GameObject targetobj = GameObject.FindGameObjectWithTag(_targetTag);
        if (targetobj != null)
        {
            _target = targetobj.transform;
            _eat = true;
        }
        else
        {
            _eat = false;
        }
    }
}
