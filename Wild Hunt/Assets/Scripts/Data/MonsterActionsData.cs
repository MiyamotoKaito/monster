using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName ="Data/Monster/Actions")]
public class MonsterActionsData : ScriptableObject
{
    [SerializeReference, SubclassSelector]
    private IAction[] Actions;
}
