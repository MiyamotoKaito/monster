using UnityEngine;

[System.Serializable]
public class Hunger : IWorldState
{
    public int Value => throw new System.NotImplementedException();

    public string Name => throw new System.NotImplementedException();
    [SerializeField]
    private string _name;
    [SerializeField]
    private int _value;
}
