using UnityEngine;

public class BackButtonController : MonoBehaviour
{
    private static BackButtonController instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android) 
        {
            if (Input.GetKeyDown(KeyCode.Escape)) 
            {
                Application.Quit();
            }
        }
    }
}
