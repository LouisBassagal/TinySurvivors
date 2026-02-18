using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "TinySurvivors/New Ability")]
public class AbilitySO : ScriptableObject
{
    public string Title;
    [TextArea]
    public string Description;
    public int MaxLevel = 5;
    public GameObject Prefab;
}