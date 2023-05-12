using UnityEngine;

public class DataBetweenScenes : MonoBehaviour
{
    public static DataBetweenScenes instance;
    
    public bool isPaused { get; set; }
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Init()
    {
        isPaused = false;
    }
    
}
