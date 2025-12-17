using UnityEngine;

public class SurvivalStats : MonoBehaviour
{
    [SerializeField]
    [Header("空腹度")]
    private float _hunger = 0;
    [SerializeField]
    private SubGoalType _subGoal;
    /// <summary>サブゴールを追加するためのGAgent</summary>
    private GAgent _agent;
    /// <summary>目標を追加するかどうか</summary>
    private bool _isHungerGoalActive = false;
    private float _timer;
    private Transform _target;
    private void Start()
    {
        _agent = GetComponent<GAgent>();
    }
    private void Update()
    {
        //お腹が常に減っていく
        _timer += Time.deltaTime;
        if (_hunger < _timer)
        {
            if (!_isHungerGoalActive)
            {
                Debug.Log($"{_hunger}サブゴール追加");
                GSubGoal eatGoal = new GSubGoal(_subGoal.ToString(), 10, true);
                _agent.AddSubGoal(10, eatGoal);
                _isHungerGoalActive = true;
            }
        }
    }
    /// <summary>
    /// このサブゴールが達成されたときに呼ぶ関数
    /// ゴールの削除は達成時にRemoveされるので書かなくて良い
    /// </summary>
    public void OnFed()
    {
        _isHungerGoalActive = false;
        _timer = 0;
    }
}
