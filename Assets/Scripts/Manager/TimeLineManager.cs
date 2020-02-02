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


    private List<GameObject> showing; //showing object on the timeline


    private void OnEnable()
    {
        gm = GetComponent<GameManager>();
        
        // gm.ShowDotEvent += addDot;
        gm.ScrollEvent += onScrollEvent;
        gm.ChangeOptionEvent += onScrollEvent;
        showing = new List<GameObject>();
    }

    void onScrollEvent() {
        foreach (GameObject go in showing)
        {
            Destroy(go);
        }
        showing = new List<GameObject>();

        addDot();
        int numberOfOptions = gm.conversationManager.GetOptionalNextSentences().Length;
        if (numberOfOptions > 1)
        {
            addOptions(numberOfOptions);
            int index = gm.conversationManager.GetSentenceInIndex(gm.conversationManager.GetIndex()).activeIndex + 1;
            addDot(150, index.ToString());
        }
    }

    private void OnDisable()
    {
        // gm.ShowDotEvent -= addDot;
        gm.ScrollEvent -= onScrollEvent;
    }
    
    public void addDot(int pos = 0, string text = "")
    {
        var dotInitiliazed = Instantiate(dot, Vector3.zero, Quaternion.identity);
        dotInitiliazed.transform.SetParent(line.transform);
        dotInitiliazed.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        dotInitiliazed.transform.localPosition = new Vector3(0 + pos, 0, 0);
        dotInitiliazed.GetComponentInChildren<TextMeshProUGUI>().text = text;
        showing.Add(dotInitiliazed);
    }
    public void addOptions(int numberOfOptions)
    {
        var optionsVisual = Instantiate(optionsContainer, Vector3.zero, Quaternion.identity);
        optionsVisual.transform.SetParent(line.transform);
        optionsVisual.transform.localScale = new Vector3(1, 1, 1);
        optionsVisual.transform.localPosition = new Vector3(0, 0, 0);


        for(int i = 1; i <= numberOfOptions; i++)
        {
            optionsVisual.GetComponentInChildren<TextMeshProUGUI>().text += " " + i;
        }
        showing.Add(optionsVisual);
    }
    
}
