using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void LoadScene(string name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }
}
