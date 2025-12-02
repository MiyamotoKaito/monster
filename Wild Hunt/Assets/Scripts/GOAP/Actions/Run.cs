using UnityEngine;

public class Run : IAction
{
    [SerializeField]
    private string ActionName;
    [SerializeField]
    private int _priority;
    [SerializeField]
    private float _speed;
}
