using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : IAction
{
    public Dictionary<string, int> Preconditions => new Dictionary<string, int>() { {"HasTarget", 1 } };

    public Dictionary<string, int> Effects => new Dictionary<string, int>() { { "AtTarget", 1 } };

    public int Cost => _cost;

    [SerializeField]
    private int _cost;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _stopDistance;
    
    //実行時変数
    private Transform _target;
    private bool _isMoving;
    private string _targetTag;

    public bool CheckPrecondition(GAgent agent)
    {
        return _target != null;
    }

    public void Execute(GAgent agent)
    {
        GameObject targetobj = GameObject.FindGameObjectWithTag(_targetTag);
        if (targetobj != null)
        {
            _target = targetobj.transform;
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }
    }

    public bool Perform(GAgent agent)
    {
        if (!_isMoving || _target == null) return false;

        float distance = Vector3.Distance(agent.transform.position, _target.position);
        if (distance <= _stopDistance)
        {
            Debug.Log($"{GetType().Name}アクション完了");
            return true;
        }
        //移動
        Vector3 direction = (_target.position - agent.transform.position).normalized;
        agent.transform.position += direction * _speed * Time.deltaTime;

        //まだ完了していない
        return false;
    }
}