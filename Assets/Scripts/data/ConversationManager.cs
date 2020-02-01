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

public interface IConversationManager {
    /**
     * Loads a JSON file containing all the conversation data
     */
    public void LoadConversationFromFile(string fileName);
    /**
     * Get the index of the current sentance in the conversation
     * The Index tracking the conversation is linear,
     * and folowwing the path of "activeOption"
     */
    public int GetIndex();
    /**
     * Get the Sentance Object of the current index.
     * The Index tracking the conversation is linear,
     * and folowwing the path of "activeOption"
     */
    public Sentance GetCurrentSentence();
    /**
     * Get a list of Sentence object from current index, with the specified count
     * It follows the ActiveOption index, choosing the next option with the active index
     * The Index tracking the conversation is linear,
     * and folowwing the path of "activeOption"
     * int Count    How many Sentence object to return in the list. 0 will get everything until the end
     */
    public Sentence[] GetConversationChain(int Count);
     /**
     * Checks all optional conversations wheter they've got UnlockedBy Key defined
     * If so, checks to see if the player already got the key.
     * Return a list with sentences without a key, or with an obtained key
     */
    public Sentence[] GetOptionalNextSentences();
    /**
     * Change the current tracking index to specified
     * The Index tracking the conversation is linear,
     * and folowwing the path of "activeOption"
     */
    public void SeekTo(int Index);
    /**
     * Change the current tracking object by a specified amount
     * The Index tracking the conversation is linear,
     * and folowwing the path of "activeOption"
     */
    public void SeekBy(int Steps);
    /**
     * Change the active option of the current Sentence.
     * After this, the sentence list needs to reload using "GetConversationChain"
     * int Index    The index of the selected option
     */
    public void ChangeActiveOption(int Index);
}

public interface ConversationNode {
    int SentenceId;
    int PreviousId;
    ConversationNode[] NextOptions;
    int ActiveIndex;
}

public interface Sentence {
    // Content definitions
    int Id;
    int Tone;
    string Text;
    int Speaker;
    Key OwnKey;
    Key UnlockedBy;

    // Structure definitions
    Sentance Previous;
    Sentence[] NextOptions;
    int ActiveIndex;
}

public interface Key {
    int Id;
    string Text;
    int Tone;
}

public interface Conversation {
    Sentence First;
}
