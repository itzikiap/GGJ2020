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
    private int upSteps;

    [SerializeField]
    private GameObject FemaleDialog;

    [SerializeField]
    private GameObject MaleDialog;

    [SerializeField]
    private GameObject SpeakerDialog;

    [SerializeField]
    private GameObject ParentDialogContainer;

    private List<GameObject> alreadyShowed;

    public void OnEnable()
    {
        gameManagerRef = GetComponent<GameManager>();
        cm = gameManagerRef.conversationManager;
        gameManagerRef.ShowDialogsEvent += initialize; 
    }
    public void OnDisable()
    {
        gameManagerRef.ShowDialogsEvent -= initialize;
    }
    public void initialize()
    {
         StartCoroutine(showSentences());
    }
    public IEnumerator showSentences()
    {
        Debug.Log(cm);
        Debug.Log("Showing sentences");
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
            moveUp();
            yield return new WaitForSeconds(1f);
        }        
    }

    public void moveUp()
    {
        foreach (GameObject go in alreadyShowed)
        {
            Vector3 old = go.GetComponent<RectTransform>().localPosition;
            go.GetComponent<RectTransform>().localPosition = new Vector3(old.x, old.y + upSteps, old.z);
        }
    }
    public void addSentenceSpeaker(string s)
    {
        GameObject dialog = Instantiate(SpeakerDialog, Vector3.zero, Quaternion.identity) as GameObject;
        dialog.transform.SetParent(ParentDialogContainer.transform);
        dialog.GetComponent<RectTransform>().localPosition = new Vector3(-100, -130, 0);
        dialog.GetComponent<RectTransform>().localScale = new Vector3(2.5f, 5, 1);
        dialog.GetComponentInChildren<TextMeshProUGUI>().text = s;
        alreadyShowed.Add(dialog);
    }
    public void addSentenceFemale(string s)
    {
        GameObject dialog = Instantiate(FemaleDialog, Vector3.zero, Quaternion.identity) as GameObject;
        dialog.transform.SetParent(ParentDialogContainer.transform);
        dialog.GetComponent<RectTransform>().localPosition = new Vector3(-100, -130, 0);
        dialog.GetComponent<RectTransform>().localScale = new Vector3(2.5f, 5, 1);
        dialog.GetComponentInChildren<TextMeshProUGUI>().text = s;
        alreadyShowed.Add(dialog);
    }
    public void addSentenceMale(string s)
    {
        GameObject dialog = Instantiate(MaleDialog, Vector3.zero, Quaternion.identity) as GameObject;
        dialog.transform.SetParent(ParentDialogContainer.transform);
        dialog.GetComponent<RectTransform>().localPosition = new Vector3(100, -130, 0);
        dialog.GetComponent<RectTransform>().localScale = new Vector3(2.5f, 5, 1);
        dialog.GetComponentInChildren<TextMeshProUGUI>().text = s;
        alreadyShowed.Add(dialog);
    }


}

