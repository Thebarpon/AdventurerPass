using System.Collections.Generic;
using UnityEngine;

public class CharacterData<AbilityScoreName, AbilityScoreValue> : ScriptableObject
{
    //Character information
    public string characterName;

    //Ability score
    public Dictionary<AbilityScoreName, AbilityScoreValue> abilityScores = new Dictionary<AbilityScoreName, AbilityScoreValue>();
}
