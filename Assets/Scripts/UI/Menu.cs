using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject play;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject settings;
  
    public void PlayButton(bool set)
    {
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
