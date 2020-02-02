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
        
        // gm.ShowDotEvent += addDot;
        gm.ScrollEvent += onScrollEvent;
    }

    void onScrollEvent() {
        addDot();
        int numberOfOptions = gm.conversationManager.GetOptionalNextSentences().Length;
        if (numberOfOptions > 1)
        {
            addOptions(numberOfOptions);
        }
    }

    private void OnDisable()
    {
        // gm.ShowDotEvent -= addDot;
        gm.ScrollEvent -= onScrollEvent;
    }
    
    public void addDot()
    {
        Destroy(showing);
        var dotInitiliazed = Instantiate(dot, Vector3.zero, Quaternion.identity);
        dotInitiliazed.transform.SetParent(line.transform);
        dotInitiliazed.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        dotInitiliazed.transform.localPosition = new Vector3(0, 0, 0);
        showing = dotInitiliazed;
    }

    private GameObject showing; //showing object on the timeline

    public void addOptions(int numberOfOptions)
    {
        Destroy(showing);
        
        var optionsVisual = Instantiate(optionsContainer, Vector3.zero, Quaternion.identity);
        optionsVisual.transform.SetParent(line.transform);
        optionsVisual.transform.localScale = new Vector3(1, 1, 1);
        optionsVisual.transform.localPosition = new Vector3(0, 0, 0);


        for(int i = 1; i <= numberOfOptions; i++)
        {
            optionsVisual.GetComponentInChildren<TextMeshProUGUI>().text += " " + i;
        }
        showing = optionsVisual;
    }
    
}
