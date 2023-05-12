using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject play;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject settings;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        DataBetweenScenes.instance.Init();
    }

    public void PlayButton(bool set)
    {
        _animator.SetTrigger("GetUp");
        menu.SetActive(!set);
        play.SetActive(set);
    }
    
    public void NewGameButton()
    { 
        SceneManager.LoadScene("GamePlay");
    }
    
    public void SettingsButton(bool set)
    {
        menu.SetActive(!set);
        settings.SetActive(set);
    }
    

    public void CreditsButton(bool set)
    {
        menu.SetActive(!set);
        credits.SetActive(set);
    }
    
    public void ExitButton()
    {
        Application.Quit();
    }
    
    
}
