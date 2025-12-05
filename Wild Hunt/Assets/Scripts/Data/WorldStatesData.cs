using GOAP.WorldStates;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/WorldState")]
public class WorldStatesData : ScriptableObject
{
    public List<IWorldState> WorldStates => _worldStates;

    [SerializeReference, SubclassSelector]
    private List<IWorldState> _worldStates;
}