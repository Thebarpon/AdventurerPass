using TMPro;
using UnityEngine;

public class CharacterSelectionDropdown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _characterSelection;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private GameObject _characterPanel;

    private void Start()
    {
        _characterSelection.onValueChanged.AddListener(OnValueChange);
    }
    public void OnValueChange(int index)
    {
        if (_characterSelection.options[index].text == "NouveauPersonnage")
        {
            Debug.Log("Create new character");
        }
        else
        {
            _uiManager.SwitchPanel(_characterPanel);
        }
    }
}
