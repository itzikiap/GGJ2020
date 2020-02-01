using System.Collections;
using System.IO;
using UnityEngine;

public class ConversationManager : MonoBehaviour, IConversationManager
{
    string path;
    string jsonRawData;
    int currentIndex;
    Sentence currentSentence;
    Key[] ObtainedKeys;
    DialogData conversation;
    public ConversationManager() {
    }

    void Start() {
        Debug.Log("Got Here");
        path = Application.streamingAssetsPath;
        LoadConversationFromFile("dialogs.json");        
    }

    void ConstructConversation(Sentence[] sentenceList) {
        
    }

    // ----------- INTERFACE METHODS --------
    public void LoadConversationFromFile(string conversationFileName) {
        jsonRawData = File.ReadAllText(path + '/' + conversationFileName);
        conversation = JsonUtility.FromJson<DialogData>(jsonRawData);
        Debug.Log(conversation);
    }
    public int GetIndex() {
        return 0;
    }
    public bool IsCurrentIndexLeaf() {
        return false;
    }
    public Sentence GetCurrentSentence() {
        return null;
    }
    public Sentence[] GetConversationChain(int Count) {
        return new Sentence[0];
    }
    public Sentence[] GetOptionalNextSentences() {
        return new Sentence[0];
    }
    public void SeekTo(int Index) {

    }
    public void SeekBy(int Steps) {

    }
    public void ChangeActiveOption(int Index) {

    }
    // -----------------------------------
}
