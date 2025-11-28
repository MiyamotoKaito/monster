/// <summary>
/// 患者エージェントにサブゴールを追加するクラス
/// </summary>
public class Paitient : GAgent
{
    void Awake()
    {
        base.BaseAwake();
        SubGoal s1 = new SubGoal("IsWaiting", 1, true);
        _subGoals.Add(s1, 3);
    }
}
