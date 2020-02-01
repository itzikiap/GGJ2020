using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void GameManagerEventHandler();
    // Start is called before the first frame update
    private ConversationManager conversationManager;
    void Start()
    {
        conversationManager = new ConversationManager();   
    }

}
