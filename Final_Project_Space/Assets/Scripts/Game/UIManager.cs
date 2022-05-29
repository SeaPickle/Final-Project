using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject deathPanel;
    [SerializeField] GameObject pausePanel;

    public void ToggleDeathPanel()
    {
        deathPanel.SetActive(true);
    }

    public void TogglePauseMenu()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
