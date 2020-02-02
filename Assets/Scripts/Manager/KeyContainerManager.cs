using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyContainerManager : MonoBehaviour
{
    private GameManager gm;

    [SerializeField]
    private GameObject keyPrefab;
    [SerializeField]
    private GameObject keyContainer;

    private void OnEnable()
    {
        gm = GetComponent<GameManager>();
        gm.ScrollEvent += ShowKeys;
    }
    private void OnDisable()
    {
        gm.ScrollEvent -= ShowKeys;
    }

    public void ShowKeys()
    {
        DestroyChildren();

        Key[] keys = gm.conversationManager.GetObtainedKeys();
        foreach (Key k in keys) {
            GameObject key = Instantiate(keyPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            key.transform.SetParent(keyContainer.transform);
            key.transform.localScale = new Vector3(1, 1, 1);
            key.transform.localPosition = new Vector3(0, 0, 0);
            key.GetComponent<TextMeshProUGUI>().text = k.text;

            showingKeys.Add(key);
        }
    }

    private List<GameObject> showingKeys = new List<GameObject>();

    private void DestroyChildren()
    {
        foreach (GameObject g in showingKeys) {
            Destroy(g);
        }
    }
}
