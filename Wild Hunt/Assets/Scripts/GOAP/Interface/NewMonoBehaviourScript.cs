using UnityEngine;

public class NewMonoBehaviourScript : IWorldState
{

    [SerializeField]
    private string _name;
    [SerializeField]
    private int _value;

    public string Name => _name;

    public int Value => _value;
}
