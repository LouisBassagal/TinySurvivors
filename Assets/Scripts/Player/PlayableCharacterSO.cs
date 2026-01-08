using UnityEngine;

[CreateAssetMenu(fileName = "PlayableCharacterSO", menuName = "TinySurvivors/PlayableCharacterSO")]
public class PlayableCharacterSO : ScriptableObject
{
    public string characterName;

    public float baseHealth = 100f;
    public float baseMoveSpeed = 5f;
    public float baseDamage = 10f;
    public float baseLuck = 1f;
}
