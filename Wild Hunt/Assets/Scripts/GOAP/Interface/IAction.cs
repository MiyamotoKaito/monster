using System.Collections.Generic;

public interface IAction
{
    Dictionary<string, int> Preconditions { get; }
    Dictionary<string , int> Effects { get; }

    int Cost { get; }

    /// <summary>
    /// 前提条件
    /// </summary>
    /// <returns></returns>
    bool PreCondition(IWorldState worldState);
    /// <summary>
    /// 効果
    /// </summary>
    /// <returns></returns>
    bool Effect(IWorldState worldState);
}