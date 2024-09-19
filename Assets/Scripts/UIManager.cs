using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _logInPanel;
    [SerializeField] private List<GameObject> _UIPanels;
    // Start is called before the first frame update
    void Start()
    {
        SwitchPanel(_logInPanel);
    }

    public void SwitchPanel(GameObject newPanel)
    {
        foreach (GameObject panel in _UIPanels)
        {
            if (panel != newPanel)
            {
                panel.SetActive(false);
            }
            else
            {
                panel.SetActive(true);
            }
        }
    }
}
