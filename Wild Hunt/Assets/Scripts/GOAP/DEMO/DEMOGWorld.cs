using GOAP.DEMO.WorldState;
/// <summary>
/// グローバルワールドステート管理クラス
/// </summary>
public class DEMOGWorld
{
    //シングルトンインスタンス
    private static readonly DEMOGWorld instanse = new DEMOGWorld();
    //グローバルワールドステート
    private static DEMOWorldStates _world;
    //静的コンストラクタ
    static DEMOGWorld()
    {
        _world = new DEMOWorldStates();
    }
    /// <summary>
    /// 
    /// </summary>
    private DEMOGWorld()
    {
    }
    //シングルトンインスタンスの取得
    public static DEMOGWorld Instance
    {
        get { return instanse; }
    }
    /// <summary>
    /// グローバルワールドステートの取得
    /// </summary>
    /// <returns></returns>
    public DEMOWorldStates GetWorld()
    {
        return _world;
    }
}
