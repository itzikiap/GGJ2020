using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dialog : MonoBehaviour
{
    [SerializeField]
    private GameObject FemaleDialog;
    [SerializeField]
    private GameObject MaleDialog;  

    [SerializeField]
    public string[] sentences;


    private List<GameObject> alreadyshowed;

    private bool speakingFirst = true;

    private void Start()
    {
        alreadyshowed = new List<GameObject>();
        StartCoroutine(showSentences());
    }

    public IEnumerator showSentences()
    {
        Debug.Log("Showing sentences");
        foreach(string sentence in sentences)
        {
            moveUp();
            if (speakingFirst)
            {
                addSentenceMale(sentence);
                Debug.Log("Showing: "+sentence);
                speakingFirst = false;
            }
            else
            {
                addSentenceFemale(sentence);
                Debug.Log("Showing sentences"+sentence);
                speakingFirst = true;
            }
           
            yield return new WaitForSeconds(1f);
        }

       
    }

    [SerializeField]
    private int upSteps;

    public void moveUp()
    {
        foreach (GameObject go in alreadyshowed)
        {
            Vector3 old = go.GetComponent<RectTransform>().localPosition;
            go.GetComponent<RectTransform>().localPosition = new Vector3(old.x, old.y + upSteps, old.z);               
        }
    }

    int last_y = -100;

    public void addSentenceFemale(string s)
    {
        GameObject dialog = Instantiate(FemaleDialog, Vector3.zero, Quaternion.identity) as GameObject;
        dialog.transform.SetParent(transform);
        dialog.GetComponent<RectTransform>().localPosition = new Vector3(-100, -130, 0);
        dialog.GetComponent<RectTransform>().localScale = new Vector3(2.5f, 5, 1);
        dialog.GetComponentInChildren<TextMeshProUGUI>().text = s;
        alreadyshowed.Add(dialog);
    }
    public void addSentenceMale(string s)
    {
        GameObject dialog = Instantiate(MaleDialog, Vector3.zero, Quaternion.identity) as GameObject;
        dialog.transform.SetParent(transform);
        dialog.GetComponent<RectTransform>().localPosition = new Vector3(100, -130, 0);
        dialog.GetComponent<RectTransform>().localScale = new Vector3(2.5f, 5, 1);
        dialog.GetComponentInChildren<TextMeshProUGUI>().text = s;
        alreadyshowed.Add(dialog);
    }
}

