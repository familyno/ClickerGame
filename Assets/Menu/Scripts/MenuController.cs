using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject _panelSettings;
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowPanelSettings()
    {
        _panelSettings.SetActive(true);
    }

    public void ClosePanelSettings()
    {
        _panelSettings.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
