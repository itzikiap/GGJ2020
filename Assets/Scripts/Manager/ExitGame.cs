using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    private GameManagerMainMenu gameManagerRef;

    private void OnEnable()
    {
        gameManagerRef = GetComponent<GameManagerMainMenu>();
        gameManagerRef.ExitGameEvent += Quit;
    }
    public void OnDisable()
    {
        gameManagerRef.ExitGameEvent -= Quit;
    }

    public void Quit()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }



}
