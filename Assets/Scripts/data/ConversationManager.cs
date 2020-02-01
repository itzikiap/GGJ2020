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
        path = Application.streamingAssetsPath;
        LoadConversationFromFile("dialogs.json");
    }

    void ConstructConversation(Sentence[] sentenceList) {
        
    }

    // ----------- INTERFACE METHODS --------
    public void LoadConversationFromFile(string conversationFileName) {
        jsonRawData = File.ReadAllText(path + '/' + conversationFileName);
        conversation = JSONUtility.FromJson<DialogData>(jsonRawData);
        Debug.log(conversation);
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
        return [];
    }
    public Sentence[] GetOptionalNextSentences() {
        return [];
    }
    public void SeekTo(int Index) {

    }
    public void SeekBy(int Steps) {

    }
    public void ChangeActiveOption(int Index) {

    }
    // -----------------------------------
}
