using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerMainMenu : MonoBehaviour
{
    public delegate void GameManagerEventHandler();

    public event GameManagerEventHandler StartGameEvent;
    public event GameManagerEventHandler ExitGameEvent;

    public void CallStartGameEvent()
    {
        StartGameEvent?.Invoke();
    }
    public void CallExitGameEvent()
    {
        ExitGameEvent?.Invoke();
    }

}


