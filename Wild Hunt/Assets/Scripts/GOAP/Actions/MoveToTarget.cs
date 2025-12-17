using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : IAction
{
    public Dictionary<string, int> Preconditions => new Dictionary<string, int>() { { _precondition.ToString(), 1 } };

    public Dictionary<string, int> Effects => new Dictionary<string, int>() { { _effect.ToString(), 1 } };

    public int Cost => _cost;

    [SerializeField]
    private int _cost;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _stopDistance;
    [SerializeField] private WorldStateType _precondition;
    [SerializeField] private WorldStateType _effect;

    //実行時変数
    private Transform _target;
    private bool _isMoving;
    [SerializeField]
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
        // ターゲットがいない場合は失敗として中断
        if (_target == null) return false;

        float distance = Vector3.Distance(agent.transform.position, _target.position);

        // まだ離れている場合
        if (distance > _stopDistance)
        {
            // 移動処理
            Vector3 direction = (_target.position - agent.transform.position).normalized;
            agent.transform.position += direction * _speed * Time.deltaTime;

            // 【重要】まだ終わっていないので false を返す
            // これにより、GAgentは次のアクションに進まずにこのメソッドを呼び続けます
            return false;
        }

        // 到着した
        Debug.Log($"{GetType().Name} : 目的地に到着しました。");
        return true;
    }
}