using UnityEngine;

[System.Serializable]
public class GroundState : IWorldState
{

    [SerializeField]
    private string _name;
    [SerializeField]
    private int _value;

    public string Name => _name;

    public int Value => _value;
}
