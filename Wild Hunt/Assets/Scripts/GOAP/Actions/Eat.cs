using UnityEngine;

public class Eat : IAction
{
    [SerializeField]
    private string ActionName;
    [SerializeField]
    private int _priority;
}
