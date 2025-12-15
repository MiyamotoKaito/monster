using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName ="Data/Monster/Actions")]
public class MonsterActionsData : ScriptableObject
{
    public IAction[] Actions => _actions;

    [SerializeReference, SubclassSelector]
    private IAction[] _actions;
}