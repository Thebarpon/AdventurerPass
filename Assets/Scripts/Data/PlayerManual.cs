using UnityEngine;

public class PlayerManual : ScriptableObject
{
    //Ability scores
    public enum EAbilityScore
    {
        Force,
        Dexterity,
        Constitution,
        Intelligence,
        Wisdom,
        Charisma
    }
    public int abilityScore;
    public CharacterData<EAbilityScore, int> GenerateCharacterSheet()
    {
        var newCharacterData = ScriptableObject.CreateInstance<CharacterData<EAbilityScore, int>>();

        //TODO allocate character data values

        return newCharacterData;
    }
}
