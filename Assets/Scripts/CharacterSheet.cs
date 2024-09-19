using UnityEngine;

public class CharacterSheet : MonoBehaviour
{
    private PlayerManual _manual;
    private ScriptableObject _characterData;
    // Start is called before the first frame update
    private void Start()
    {
        _characterData = _manual.GenerateCharacterSheet();
    }
}
