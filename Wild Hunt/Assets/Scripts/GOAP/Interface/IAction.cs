using System.Collections.Generic;

public interface IAction
{
    // --- 計画策定フェーズ (GPlannerが参照する静的な定義) ---

    /// <summary>
    /// プランナーがグラフ構築に使用する、必要なワールドステートの条件リスト
    /// </summary>
    Dictionary<string, int> Preconditions { get; }

    /// <summary>
    /// プランナーがシミュレーションに使用する、ワールドステートへの影響リスト
    /// </summary>
    Dictionary<string, int> Effects { get; }

    /// <summary>
    /// アクションの実行コスト (A*のg値)
    /// </summary>
    int Cost { get; }


    // --- 実行フェーズ (GAgentが参照する動的なロジック) ---

    /// <summary>
    /// 実行前にリアルタイムの環境で前提条件が満たされているかチェックする
    /// </summary>
    /// <param name="agent">アクションを実行するエージェント</param>
    /// <returns>実行可能なら true</returns>
    bool CheckPrecondition(GAgent agent);
    // ※ GAgentクラスはまだ実装中かと思いますが、実行者を渡すのが最も汎用的です。

    /// <summary>
    /// アクションの実際の実行ロジック
    /// </summary>
    /// <param name="agent">アクションを実行するエージェント</param>
    /// <returns>アクションの完了（成功）なら true</returns>
    bool Perform(GAgent agent);

    /// <summary>
    /// アクションQueueから取り出されたときに一度だけ呼ばれる関数
    /// </summary>
    /// <param name="agent"></param>
    void Execute(GAgent agent);
}