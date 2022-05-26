using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject tutorialPanel;
    public void ChangeSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void ToggleTutorialView(bool isOn)
    {
        menuPanel.SetActive(!isOn);
        tutorialPanel.SetActive(isOn);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
