using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeLineManager : MonoBehaviour
{
    [SerializeField]
    private GameObject dot;
    [SerializeField]
    private GameObject line;
    [SerializeField]
    private GameObject optionsContainer;

    private GameManager gm;

    private void OnEnable()
    {
        gm = GetComponent<GameManager>();
        
        gm.ShowDotEvent += addDot;
        gm.ScrollEvent += addOptions;
    }

    private void OnDisable()
    {
        gm.ShowDotEvent -= addDot;
        gm.ScrollEvent -= addOptions;
    }
    
    public void addDot()
    {
        var dotInitiliazed = Instantiate(dot, Vector3.zero, Quaternion.identity);
        dotInitiliazed.transform.SetParent(line.transform);     
    }

    public void addOptions()
    {
        var optionsVisual = Instantiate(optionsContainer, Vector3.zero, Quaternion.identity);
        optionsVisual.transform.SetParent(line.transform);

        int numberOfOptions = gm.conversationManager.GetCurrentSentence().nextOptionsIds.Length;
        for(int i = 1; i <= numberOfOptions; i++)
        {
            optionsVisual.GetComponentInChildren<TextMeshProUGUI>().text += " " + i;
        }
       
    }
    
}
