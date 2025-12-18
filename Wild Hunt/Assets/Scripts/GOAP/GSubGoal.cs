using System.Collections.Generic;
using UnityEngine;

public class GSubGoal
{
    public Dictionary<string, int> SubGoals;
    public string Name;
    public bool Remove;
    public GameObject TargetObj;
    public GSubGoal(string goalName, int priority, bool remove, GameObject target)
    {
        SubGoals = new Dictionary<string, int>();
        Name = goalName;
        TargetObj = target;
        SubGoals.Add(goalName, priority);
        Remove = remove;
    }
}
