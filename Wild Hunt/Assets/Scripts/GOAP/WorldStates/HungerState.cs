using UnityEngine;

[System.Serializable]
public class Hunger : IWorldState
{
    public string Name => _name;
    public int Value => _value;

    [SerializeField]
    private string _name;
    [SerializeField, Range(0, 100)]
    private int _value;
}