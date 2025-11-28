using UnityEngine;

public class GoToHospital : GAction
{
    public override bool PrePerform()
    {
        Debug.Log("Going to the hospital...");
        return true;
    }
    public override bool PostPerform()
    {
        Debug.Log("Arrived at the hospital.");
        return true;
    }
}
