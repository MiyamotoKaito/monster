using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName ="Data/Monster/Actions")]
public class MonsterActionsData : ScriptableObject
{
    public IAction[] Actions => Actions;

    [SerializeReference, SubclassSelector]
    private IAction[] _acitons;
}