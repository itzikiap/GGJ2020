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


    private bool speakingFirst = true;

    private void Start()
    {
        StartCoroutine(showSentences());
    }

    public IEnumerator showSentences()
    {
        Debug.Log("Showing sentences");
        foreach(string sentence in sentences)
        {
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

    int last_y = -100;

    public void addSentenceFemale(string s)
    {
        GameObject dialog = Instantiate(FemaleDialog, Vector3.zero, Quaternion.identity) as GameObject;
        dialog.transform.SetParent(transform);
        dialog.GetComponent<RectTransform>().localPosition = new Vector3(-100, last_y + 80, 0);
        dialog.GetComponent<RectTransform>().localScale = new Vector3(2.5f, 5, 1);
        dialog.GetComponentInChildren<TextMeshProUGUI>().text = s;
    }
    public void addSentenceMale(string s)
    {
        GameObject dialog = Instantiate(MaleDialog, Vector3.zero, Quaternion.identity) as GameObject;
        dialog.transform.SetParent(transform);
        dialog.GetComponent<RectTransform>().localPosition = new Vector3(100, last_y + 80, 0);
        dialog.GetComponent<RectTransform>().localScale = new Vector3(2.5f, 5, 1);
        dialog.GetComponentInChildren<TextMeshProUGUI>().text = s;
    }
}

