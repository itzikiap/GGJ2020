using System.Collections;
using UnityEngine;

public class ConversationManager : MonoBehaviour, IConversationManager
{
    string path;
    string jsonRawData;
    int currentIndex;
    Sentence currentSentence;
    Key[] ObtainedKeys;
    public ConversationManager() {
        path = Application.streamingAssetsPath;
        LoadConversationFromFile("dialogs.json");
    }

    void ConstructConversation(Sentence[] sentenceList) {
        
    }

    // ----------- INTERFACE METHODS --------
    public void LoadConversationFromFile(string conversationFileName) {
        
    }
    public int GetIndex() {

    }
    public bool IsCurrentIndexLeaf() {

    }
    public Sentence GetCurrentSentence() {

    }
    public Sentence[] GetConversationChain(int Count) {

    }
    public Sentence[] GetOptionalNextSentences() {
        
    }
    public void SeekTo(int Index) {

    }
    public void SeekBy(int Steps) {

    }
    public void ChangeActiveOption(int Index) {

    }
    // -----------------------------------
}
