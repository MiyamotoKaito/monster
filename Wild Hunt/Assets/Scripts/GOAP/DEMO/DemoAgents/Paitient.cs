using System;

/// <summary>
/// 患者エージェントにサブゴールを追加するクラス
/// </summary>
public class Paitient : DEMOGAgent
{

    void Awake()
    {
        base.BaseAwake();
        DEMOSubGoal s1 = new DEMOSubGoal("IsWaiting", 1, true);
        _subGoals.Add(s1, 3);
    }
}
