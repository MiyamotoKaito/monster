public interface IAction
{
    /// <summary>
    /// 前提条件
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    bool PerCondition(IWorldState worldState);
    /// <summary>
    /// 効果
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    bool Effect(IWorldState worldState);
}