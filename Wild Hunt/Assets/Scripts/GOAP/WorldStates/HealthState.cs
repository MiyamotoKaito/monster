using UnityEngine;

[System.Serializable]
public class HealthState : IWorldState
{
    public string Name => throw new System.NotImplementedException();

    public int Value => throw new System.NotImplementedException();
    [SerializeField]
    private string _name;
    [SerializeField]
    private int _value;
}
