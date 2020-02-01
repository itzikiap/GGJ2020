﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void GameManagerEventHandler();

    public event GameManagerEventHandler ExitGameEvent;
        
    private ConversationManager conversationManager;


    void Start()
    {
        conversationManager = new ConversationManager();   
    }

    public void CallExitGameEvent()
    {
        ExitGameEvent?.Invoke();
    }

}
