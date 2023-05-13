using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject controls;
    [SerializeField] private GameObject pause;
    
    public static event Action OnPause;
    public static event Action OnChangeScene;

    private void Start()
    {
        DataBetweenScenes.instance.Init();
        OnChangeScene?.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !DataBetweenScenes.instance.isPaused)
            UpdateGameState();
    }

    private void UpdateGameState()
    {
        DataBetweenScenes.instance.isPaused = !DataBetweenScenes.instance.isPaused;
        pause.SetActive(DataBetweenScenes.instance.isPaused);
        OnPause?.Invoke();
        Time.timeScale = DataBetweenScenes.instance.isPaused ? 0f : 1f;
    }

    public void Play()
    {
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
