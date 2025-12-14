using Unity.VisualScripting;
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
    private float _timer;
    private void Update()
    {
        //お腹が常に減っていく
        _timer += Time.deltaTime;
        if (_hunger > _timer)
        {
            if (!_isHungerGoalActivte)
            {
                GSubGoal eatGoal = new GSubGoal("IsFull", 10, true);
                _agent.AddSubGoal(10, eatGoal);
                _isHungerGoalActivte = true;
            }
        }
    }
}
