using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName ="Monster/Actions")]
public class MonsterActionsData : ScriptableObject
{
    [SerializeReference, SubclassSelector]
    private IAction[] Actions;
}
