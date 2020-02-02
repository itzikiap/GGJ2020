using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private GameManagerMainMenu gameManagerRef;

    private void OnEnable()
    {
        gameManagerRef = GetComponent<GameManagerMainMenu>();
        gameManagerRef.StartGameEvent += LoadMainScreen;
    }
    public void OnDisable()
    {
        gameManagerRef.StartGameEvent -= LoadMainScreen;
    }

    public void LoadMainScreen()
    {
        SceneManager.LoadScene(1);
    }

   

}
