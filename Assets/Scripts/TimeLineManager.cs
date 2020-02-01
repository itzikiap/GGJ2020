using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineManager : MonoBehaviour
{
    [SerializeField]
    private GameObject dot;
    [SerializeField]
    private GameObject line;

    public List<GameObject> dots = new List<GameObject>();

    private void Start()
    {

    }
    public void addDot(int x)
    {

        var dotInitiliazed = Instantiate(dot, Vector3.zero, Quaternion.identity);
        dotInitiliazed.transform.SetParent(line.transform);
        dotInitiliazed.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        dotInitiliazed.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
        
    }
    
}
