public interface IAction
{
    /// <summary>
    /// 前提条件
    /// </summary>
    /// <returns></returns>
    bool PerCondition(IWorldState worldState);
    /// <summary>
    /// 効果
    /// </summary>
    /// <returns></returns>
    bool Effect(IWorldState worldState);
}