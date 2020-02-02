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

    public const int CONFUSED = 0;
    public const int SURPRISED = 1;
    public const int ANGRY = 2;
    public const int VERY_ANGRY = 3;
    public const int NORMAL = 4;
    public const int HAPPY = 5;
    public const int UPSET = 6;
    public const int SAD = 7;
    public const int CRYING = 8;

   

    public void changeFatherExpression(int expression_const)
    {
        fatherImage.sprite = imagesFather[expression_const];
    }
    public void changeDaughterExpression(int expression_const)
    {
        daughterImage.sprite = imagesDaughter[expression_const];
    }

}
