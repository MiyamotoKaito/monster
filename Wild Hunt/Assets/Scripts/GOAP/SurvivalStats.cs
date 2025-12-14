using UnityEngine;

public class SurvivalStats : MonoBehaviour
{
    [SerializeField]
    [Header("空腹度")]
    private float _hunger = 0;
    /// <summary>サブゴールを追加するためのGAgent</summary>
    private GAgent _agent;
    /// <summary>目標を追加するかどうか</summary>
    private bool _isHungerGoalActivte = false;


}
