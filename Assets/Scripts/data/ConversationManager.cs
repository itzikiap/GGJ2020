using System.Collections;
using System.Collections.Generic;

public class ConversationManager : IConversationManager
{
    Conversation conversation;
    int currentIndex;
    Sentence currentSentence;
    Key[] ObtainedKeys;
    public ConversationManager() {

    }

    void ConstructConversation(Sentence[] sentenceList, ConversationNode tree) {
        
    }

    // ----------- INTERFACE METHODS --------
    public void LoadConversationFromFile(string conversationFileName) {

    }
    public int GetIndex() {

    }
    public Sentance GetCurrentSentence() {

    }
    public Sentance[] GetConversationChain(int Count) {

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
