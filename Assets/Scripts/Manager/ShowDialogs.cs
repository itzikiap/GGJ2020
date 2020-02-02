using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

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

    private MusicManager musicManager;

    
    private Sprite BUBBLE_1A,
        BUBBLE_2A,
        BUBBLE_1B,
        BUBBLE_2B,
        BUBBLE_2C,
        BUBBLE_1C,
        BUBBLE_2D,
        BUBBLE_1D,
        BUBBLE_1S,
        BUBBLE_2S,
        BUBBLE_3S,
        BUBBLE_4S,
        BUBBLE_5S,
        BUBBLE_NA;
    private Dictionary<string, Sprite> bubbleSprites = new Dictionary<string, Sprite>();

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
        musicManager = GetComponent<MusicManager>();
        initializeBubbles();
        showSentences();
    }

    private void initializeBubbles()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Dialogs");
        BUBBLE_1A = sprites[0];
        BUBBLE_1B = sprites[1];
        BUBBLE_1C = sprites[2];
        BUBBLE_1D = sprites[3];
        BUBBLE_1S = sprites[4];
        BUBBLE_2A = sprites[5];
        BUBBLE_2B = sprites[6];
        BUBBLE_2C = sprites[7];
        BUBBLE_2D = sprites[8];
        BUBBLE_2S = sprites[9];
        BUBBLE_3S = sprites[10];
        BUBBLE_4S = sprites[11];
        BUBBLE_5S = sprites[12];
        BUBBLE_NA = sprites[13];
        
        bubbleSprites.Add("1A", BUBBLE_1A);
        bubbleSprites.Add("1B", BUBBLE_1B);
        bubbleSprites.Add("1C", BUBBLE_1C);
        bubbleSprites.Add("1D", BUBBLE_1D);
        bubbleSprites.Add("1S", BUBBLE_1S);
        bubbleSprites.Add("2A", BUBBLE_2A);
        bubbleSprites.Add("2B", BUBBLE_2B);
        bubbleSprites.Add("2C", BUBBLE_2C);
        bubbleSprites.Add("2D", BUBBLE_2D);
        bubbleSprites.Add("2S", BUBBLE_2S);
        bubbleSprites.Add("3S", BUBBLE_3S);
        bubbleSprites.Add("4S", BUBBLE_4S);
        bubbleSprites.Add("5S", BUBBLE_5S);
        bubbleSprites.Add("NA", BUBBLE_NA);
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
            addSentenceSpeaker(s /*s.text, s.bubble, s.audio*/);
            
/*
            if (i == 0) {
                if(s.speaker == 0) {
                    addSentenceSpeaker(s.text, s.bubble);
                }
                else if(s.speaker == 1)
                {
                    addSentenceMale(s.text, s.bubble);
                }
                else
                {
                    addSentenceFemale(s.text, s.bubble);
                }
            }
*/
            i--;
        }     
        moveUp(FIRST_NUMBER_FETCH);
    }
    public IEnumerator showSentencesAnimation()
    {
        foreach(Sentence s in cm.GetConversationChain(FIRST_NUMBER_FETCH))
        {
            if(s.speaker == 0) {
                addSentenceSpeaker(s);
            }else if(s.speaker == 1)
            {
                addSentenceMale(s);
            }
            else
            {
                addSentenceFemale(s);
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
    public void addSentenceSpeaker(Sentence s /*string text, string bubble, int audio*/)
    {
        addSentence(SpeakerDialog, s, 0);
    }
    public void addSentenceFemale(Sentence s)
    {
        addSentence(FemaleDialog, s, 0);
    }
    public void addSentenceMale(Sentence s)
    {
        addSentence(MaleDialog, s, 0);
    }

    private void addSentence(GameObject type, Sentence s, int position) {
        GameObject dialog = Instantiate(type, Vector3.zero, Quaternion.identity) as GameObject;
        Sprite sprite = null;
        try
        {
             sprite = bubbleSprites[s.bubble];
        }
        catch (KeyNotFoundException)
        {
            Debug.LogError("Bubble not found");
        } 
        dialog.GetComponent<UnityEngine.UI.Image>().overrideSprite = sprite;
        dialog.transform.SetParent(ParentDialogContainer.transform);
        dialog.GetComponent<RectTransform>().localPosition = new Vector3(-100, 100, 0);
        dialog.GetComponent<RectTransform>().localScale = new Vector3(2.5f, 5, 1);
        dialog.GetComponentInChildren<TextMeshProUGUI>().text = s.text;
        alreadyShowed.Add(dialog);
        musicManager.PlaySound(s.audio);
        musicManager.SetLoop(s.audioloop);
    }
    
    public static Texture2D textureFromSprite(Sprite sprite)
    {
        if(sprite.rect.width != sprite.texture.width){
            Texture2D newText = new Texture2D((int)sprite.rect.width,(int)sprite.rect.height);
            Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x, 
                (int)sprite.textureRect.y, 
                (int)sprite.textureRect.width, 
                (int)sprite.textureRect.height );
            newText.SetPixels(newColors);
            newText.Apply();
            return newText;
        } else
            return sprite.texture;
    }
}

