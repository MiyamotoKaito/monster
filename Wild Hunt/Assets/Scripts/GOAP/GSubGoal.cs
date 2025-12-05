using System.Collections.Generic;

public class GSubGoal
{
    public Dictionary<string, int> SubGoals;

    public bool Remove;
    public GSubGoal(string goalName, int priority, bool remove)
    {
        SubGoals = new Dictionary<string, int>();
        SubGoals.Add(goalName, priority);
        Remove = remove;
    }
}
