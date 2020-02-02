using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowDialogs : MonoBehaviour
{
    private GameManager gameManagerRef;
    private ConversationManager cm;

    [SerializeField]
    private int FIRST_NUMBER_FETCH; //how many sentences should fetch are the beggining

    [SerializeField]
    private int UP_STEPS;

    [SerializeField]
    private GameObject FemaleDialog;

    [SerializeField]
    private GameObject MaleDialog;

    [SerializeField]
    private GameObject SpeakerDialog;

    [SerializeField]
    private GameObject ParentDialogContainer;

    private List<GameObject> alreadyShowed = new List<GameObject>();

    public void OnEnable()
    {
        gameManagerRef = GetComponent<GameManager>();
        cm = gameManagerRef.conversationManager;
        gameManagerRef.ShowDialogsEvent += initialize; 
        gameManagerRef.ScrollEvent += showSentences;
        gameManagerRef.ChangeOptionEvent += showSentences;
    }
    public void OnDisable()
    {
        gameManagerRef.ShowDialogsEvent -= initialize;
    }
    public void initialize()
    {
        showSentences();
    }
    private void showSentences() {
        foreach (GameObject go in alreadyShowed)
        {
            Destroy(go);
        }
        alreadyShowed = new List<GameObject>();
        Sentence[] chain = cm.GetConversationChain(FIRST_NUMBER_FETCH);
        int i = chain.Length - 1;
        foreach(Sentence s in chain)
        {
            if (i == 0) {
                if(s.speaker == 0) {
                    addSentenceSpeaker(s.text);
                }
                else if(s.speaker == 1)
                {
                    addSentenceMale(s.text);
                }
                else
                {
                    addSentenceFemale(s.text);
                }
            }
            i--;
        }     
        moveUp(FIRST_NUMBER_FETCH);
    }
    public IEnumerator showSentencesAnimation()
    {
        foreach(Sentence s in cm.GetConversationChain(FIRST_NUMBER_FETCH))
        {
            if(s.speaker == 0) {
                addSentenceSpeaker(s.text);
            }else if(s.speaker == 1)
            {
                addSentenceMale(s.text);
            }
            else
            {
                addSentenceFemale(s.text);
            }
            moveUp(1);
            yield return new WaitForSeconds(1f);
        }        
    }

    public void moveUp(int amount)
    {
        int i = 1;
        foreach (GameObject go in alreadyShowed)
        {
            Vector3 old = go.GetComponent<RectTransform>().localPosition;
            go.GetComponent<RectTransform>().localPosition = new Vector3(old.x, old.y + UP_STEPS * (amount - i) , old.z);
            i++;
        }
    }
    public void addSentenceSpeaker(string text)
    {
        addSentence(SpeakerDialog, text, 0);
    }
    public void addSentenceFemale(string text)
    {
        addSentence(FemaleDialog, text, 0);
    }
    public void addSentenceMale(string text)
    {
        addSentence(MaleDialog, text, 0);
    }

    private void addSentence(GameObject type, string text, int position) {
        GameObject dialog = Instantiate(type, Vector3.zero, Quaternion.identity) as GameObject;
        dialog.transform.SetParent(ParentDialogContainer.transform);
        dialog.GetComponent<RectTransform>().localPosition = new Vector3(-100, 100, 0);
        dialog.GetComponent<RectTransform>().localScale = new Vector3(2.5f, 5, 1);
        dialog.GetComponentInChildren<TextMeshProUGUI>().text = text;
        alreadyShowed.Add(dialog);
    }
}

