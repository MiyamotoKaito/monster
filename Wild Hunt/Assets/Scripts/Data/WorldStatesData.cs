using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/WorldState")]
public class WorldStatesData : ScriptableObject
{
    public IWorldState[] WorldStates => _worldStates;

    [SerializeReference, SubclassSelector]
    private IWorldState[] _worldStates;
}