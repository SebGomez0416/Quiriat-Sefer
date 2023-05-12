using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject controls;
    [SerializeField] private GameObject pause;
    private bool isPaused;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            pause.SetActive(true);
            UpdateGameState();
        }
           
    }

    private void UpdateGameState()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
    }


    public void Resume()
    {
        pause.SetActive(false);
        UpdateGameState();
    }
    
    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GamePlay");
    }
    
    public void MenuButton()
    {  
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    
    public void ControlsButton(bool set)
    {
        pause.SetActive(!set);
        controls.SetActive(set);
    }
    
    public void SettingsButton(bool set)
    {
       pause.SetActive(!set);
        settings.SetActive(set);
    }
    
    public void ExitButton()
    {
        Application.Quit();
    }
}
