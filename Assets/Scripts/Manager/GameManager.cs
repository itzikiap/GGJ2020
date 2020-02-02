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
    //Timeline Events
    public event GameManagerEventHandler ShowDotEvent;//shows a dot in the timeline
    public event GameManagerEventHandler ShowOptionsEvent; //shows the graphic of options in the timeline

    public event GameManagerEventHandler ObtainedKeysEvent;//when the user gets to a sentence that has a key
    
    public event GameManagerEventHandler PauseEventEvent;
        
    private ConversationManager cm;
    private MusicManager musicManager;
    private CharacterManager characterManager;

    
    public ConversationManager conversationManager { get{
        if (this.cm == null) {
            this.cm = new ConversationManager();
        }
        return this.cm;
    }}


    void Start()
    {
        this.CallShowDialogsEvent();
        this.CallScrollEvent();
        musicManager = GetComponent<MusicManager>();
        characterManager = GetComponent<CharacterManager>();
    }

    void Update () {
        bool scrolled = true;
        if(Input.GetKeyDown(KeyCode.A)){
            this.cm.SeekBy(-1);
        }
        else if(Input.GetKeyDown(KeyCode.D)){
            this.cm.SeekBy(1);
            this.CheckForKey();
   
        } else {
            scrolled = false;
        }

        bool options = true;
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            this.cm.ChangeActiveOption(0);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2)){
            this.cm.ChangeActiveOption(1);
        } 
        else if(Input.GetKeyDown(KeyCode.Alpha3)){
            this.cm.ChangeActiveOption(2);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4)){
            this.cm.ChangeActiveOption(3);
        } else {
            options = false;
        }

        if (scrolled) {
            this.CallScrollEvent();
        }
        if (options) {
            this.CallChangeOptionsEvent();
        }
    }

    private void CheckForKey() {
        Sentence sentence = this.cm.GetSentenceInIndex(this.cm.GetIndex() + 1);
        Debug.Log(JsonUtility.ToJson(sentence));
        if ( sentence != null && sentence.hasKeysIds.Length > 0) {
            this.cm.AddKeysIds(sentence.hasKeysIds);
            this.CallObtainedKeysEvent();
        }
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
    public void CallObtainedKeysEvent() 
    {
        ObtainedKeysEvent?.Invoke();
    }


        
}
