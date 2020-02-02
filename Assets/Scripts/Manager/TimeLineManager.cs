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
        Destroy(showing);
        var dotInitiliazed = Instantiate(dot, Vector3.zero, Quaternion.identity);
        dotInitiliazed.transform.SetParent(line.transform);
        
        showing = dotInitiliazed;
    }

    private GameObject showing; //showing object on the timeline

    public void addOptions()
    {
        Destroy(showing);
        
        var optionsVisual = Instantiate(optionsContainer, Vector3.zero, Quaternion.identity);
        optionsVisual.transform.SetParent(line.transform);
        optionsVisual.transform.localScale = new Vector3(1, 1, 1);
        optionsVisual.transform.localPosition = new Vector3(0, 0, 0);

        int numberOfOptions = gm.conversationManager.GetOptionalNextSentences().Length;
        if (numberOfOptions > 1)
        {
            for(int i = 1; i <= numberOfOptions; i++)
            {
                optionsVisual.GetComponentInChildren<TextMeshProUGUI>().text += " " + i;
            }
        }
        showing = optionsVisual;
    }
    
}
