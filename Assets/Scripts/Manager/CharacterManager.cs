using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{

    [SerializeField]
    private Sprite[] imagesDaughter;
    [SerializeField]
    private Sprite[] imagesFather;

    [SerializeField]
    private Image fatherImage;
    [SerializeField]
    private Image daughterImage;
    private GameManager gameManagerRef;
    private ConversationManager cm;

    public const int CONFUSED = 0;
    public const int SURPRISED = 1;
    public const int ANGRY = 2;
    public const int VERY_ANGRY = 3;
    public const int NORMAL = 4;
    public const int HAPPY = 5;
    public const int UPSET = 6;
    public const int SAD = 7;
    public const int CRYING = 8;

    public void OnEnable()
    {
        gameManagerRef = GetComponent<GameManager>();
        this.cm = gameManagerRef.conversationManager;
        // gameManagerRef.ShowDialogsEvent += initialize; 
        gameManagerRef.ScrollEvent += showSentences;
        // gameManagerRef.ChangeOptionEvent += showSentences;
    }
    public void OnDisable()
    {
        // gameManagerRef.ShowDialogsEvent -= initialize;
        gameManagerRef.ScrollEvent -= showSentences;
        // gameManagerRef.ChangeOptionEvent -= showSentences;
    }

    private void showSentences() {
        Sentence[] chain = cm.GetConversationChain(2);
        Sentence s = chain[chain.Length - 1];
        if (s.expressions[0]) {
            changeDaughterExpression(s.expressions[0]);
        }
        if (s.expressions[1]) {
            changeFatherExpression(s.expressions[1]);
        }
    }
    public void changeFatherExpression(int expression_const)
    {
        fatherImage.sprite = imagesFather[expression_const];
    }
    public void changeDaughterExpression(int expression_const)
    {
        daughterImage.sprite = imagesDaughter[expression_const];
    }

}
