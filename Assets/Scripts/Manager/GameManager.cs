using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void GameManagerEventHandler();

    public event GameManagerEventHandler ExitGameEvent;
    //Game Events
    public event GameManagerEventHandler ShowDialogsEvent; //shows all dialogs in the screen
    public event GameManagerEventHandler ScrollEvent;//simulates the scrolling in the timeline
    public event GameManagerEventHandler ShowKeysEvent;//shows all the keys available for the current dot
    public event GameManagerEventHandler ChangeOptionEvent;//changes another option
    public event GameManagerEventHandler AdquireKeyEvent;//when the user gets to a sentence that has a key
        
    public ConversationManager conversationManager { get; set; }


    void Start()
    {
        conversationManager = new ConversationManager();
        CallShowDialogsEvent();
    }

    public void CallExitGameEvent()
    {
        ExitGameEvent?.Invoke();
    }
    public void CallShowDialogsEvent()
    {
        ShowDialogsEvent?.Invoke();
    }
    public void CallScrollEvent() 
    {
        ScrollEvent?.Invoke();
    }
    public void CallShowKeysEvent()
    {
        ShowKeysEvent?.Invoke();
    }
    public void CallChangeOptionsEvent()
    {
        ChangeOptionEvent?.Invoke();
    }
    public void CallAdquireKeyEvent() 
    {
        AdquireKeyEvent?.Invoke();
    }


        
}
