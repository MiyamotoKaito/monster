using System.Linq;
using UnityEngine;

public class SurvivalStats : MonoBehaviour
{

    [Header("ステータス")]
    [SerializeField]
    [Tooltip("<color=lime>お腹のすき具合</color>")]
    private float _hunger = 0;
    [SerializeField]
    [Tooltip("<color=lime>喉の渇き具合</color>")]
    private float _thirst = 0;
    [Header("必要なゴール")]
    [SerializeField]
    private SubGoalType _eatSubGoal;
    [SerializeField]
    private SubGoalType _drinkSubGoal;
    /// <summary>サブゴールを追加するためのGAgent</summary>
    private GAgent _agent;
    /// <summary>目標を追加するかどうか</summary>
    [ReadOnly, SerializeField]
    private bool _isHungerGoalActive = false;
    [ReadOnly, SerializeField]
    private bool _isThirstGoalActive = false;
    private void Start()
    {
        _agent = GetComponent<GAgent>();
    }
    private void Update()
    {
        //お腹が常に減っていく
        _hunger -= Time.deltaTime;
        var defaultHunger = _hunger;
        _thirst -= Time.deltaTime;
        if (_hunger < 0)
        {
            if (!_isHungerGoalActive)
            {
                Debug.Log($"{_eatSubGoal}サブゴール追加");
                var target = GameObject.FindGameObjectWithTag("Food");
                GSubGoal eatGoal = new GSubGoal(_eatSubGoal.ToString(), 1, true, target);
                if (!_agent.SubGoals.ContainsKey(eatGoal))
                {
                    _agent.AddSubGoal(eatGoal, 1);
                    _isHungerGoalActive = true;
                }
                else
                {
                    _agent.ModifyGoalPriority(eatGoal, 1);
                    Debug.Log("すでに追加されているので優先度変更");
                }
            }
            _hunger = 5;
        }
        if (_thirst < 0)
        {
            if (!_isThirstGoalActive)
            {
                Debug.Log($"{_drinkSubGoal}サブゴール追加");
                var target = GameObject.FindGameObjectWithTag("Water");
                GSubGoal drinkGoal = new GSubGoal(_drinkSubGoal.ToString(), 1, true, target);
                if (!_agent.SubGoals.ContainsKey(drinkGoal))
                {
                    _agent.AddSubGoal(drinkGoal, 1);
                    _isThirstGoalActive = true;
                }
                else
                {
                    _agent.ModifyGoalPriority(drinkGoal, 1);
                }
            }
            _thirst = 7;
        }
    }
    /// <summary>
    /// このサブゴールが達成されたときに呼ぶ関数
    /// ゴールの削除は達成時にRemoveされるので書かなくて良い
    /// </summary>
    public void OnAte()
    {
        _isHungerGoalActive = false;
        Debug.Log("EAT: 食べました");
    }
    public void OnDrank()
    {
        _isThirstGoalActive = false;
        Debug.Log("Drink: 飲みました");
    }
}
