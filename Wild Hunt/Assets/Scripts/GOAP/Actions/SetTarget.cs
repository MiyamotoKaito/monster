using System.Collections.Generic;
using UnityEngine;

public class SetTarget : IAction
{
    // 前提条件を空にする（ターゲットがいてもいなくても、探し直せるようにする）
    public Dictionary<string, int> Preconditions => new Dictionary<string, int>();

    public Dictionary<string, int> Effects => new Dictionary<string, int>() { { _effect.ToString(), 1 } };
    public int Cost => _cost;
    [SerializeField] private int _cost = 1;
    [SerializeField] private WorldStateType _effect;

    public bool CheckPrecondition(GAgent agent) => true; // 常に実行可能にする

    public void Execute(GAgent agent) { }

    public bool Perform(GAgent agent)
    {
        // 食べ物があるかチェック
        var targetObj = GameObject.FindGameObjectWithTag("Food");
        if (targetObj == null) return false;

        // 【重要】実行時にワールドステートを更新して、次のMoveアクションを動かせるようにする
        GOAP.WorldStates.WorldStates.Instance.ModifyState(_effect.ToString(), 1);
        return true;
    }
}