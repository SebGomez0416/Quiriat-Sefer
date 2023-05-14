using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject controls;
    [SerializeField] private GameObject pause;
    [SerializeField] private TextMeshProUGUI currentEnemiesTex;
    [SerializeField] private TextMeshProUGUI totalEnemies;
    [SerializeField] private GameObject youWin;
    [SerializeField] private GameObject gameOver;
    private bool updateScore;
    private int currentEnemies;
    
    public static event Action OnPause;
    public static event Action OnChangeScene;
    public static event Action OnGameOver;


    private void Start()
    {
        DataBetweenScenes.instance.Init();
        OnChangeScene?.Invoke();
        currentEnemies = 0;
    }

    private void OnEnable()
    {
        BotDeathState.OnDying +=UpdateScore;
        ColossusDeathState.OnDying +=UpdateScore;
        TurretDeathState.OnDying +=UpdateScore;
        PlayerDeathState.OnDying += GameOver;
    }

    private void OnDisable()
    {
        BotDeathState.OnDying -=UpdateScore;
        ColossusDeathState.OnDying -=UpdateScore;
        TurretDeathState.OnDying -=UpdateScore;
        PlayerDeathState.OnDying -= GameOver;
    }

    private void Update()
    {
        if (!updateScore)
        {
            updateScore = true;
            totalEnemies.text = ""+DataBetweenScenes.instance.numberOfEnemies;
            currentEnemiesTex.text = ""+currentEnemies;
            
        }
        
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
        DataBetweenScenes.instance.numberOfEnemies = 0f;
        SceneManager.LoadScene("GamePlay");
    }
    
    public void MenuButton()
    {  
        Time.timeScale = 1f;
        DataBetweenScenes.instance.numberOfEnemies = 0f;
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

    private void UpdateScore()
    {
        currentEnemies++;
        currentEnemiesTex.text = ""+currentEnemies;
        if (DataBetweenScenes.instance.numberOfEnemies == currentEnemies && !DataBetweenScenes.instance.isDead)
            Win();
    }

    private void GameOver()
    {
        Time.timeScale = 0f;
       gameOver.SetActive(true);
    }
    
    private void Win()
    {
        Time.timeScale = 0f;
        youWin.SetActive(true);
    }

}
