public interface IConversationManager {
    /**
     * Loads a JSON file containing all the conversation data
     */
    void LoadConversationFromFile(string fileName);
    /**
     * Get the index of the current sentence in the conversation
     * The Index tracking the conversation is linear,
     * and folowwing the path of "activeOption"
     */
    int GetIndex();
    /**
     * Get the Sentence Object of the current index.
     * The Index tracking the conversation is linear,
     * and folowwing the path of "activeOption"
     */
    Sentence GetCurrentSentence();
    /**
     * Get a list of Sentence object from current index, with the specified count
     * It follows the ActiveOption index, choosing the next option with the active index
     * The Index tracking the conversation is linear,
     * and folowwing the path of "activeOption"
     * int Count    How many Sentence object to return in the list. 0 will get everything until the end
     */
    Sentence[] GetConversationChain(int Count);
     /**
     * Checks all optional conversations wheter they've got UnlockedBy Key defined
     * If so, checks to see if the player already got the key.
     * Return a list with sentences without a key, or with an obtained key
     */
    Sentence[] GetOptionalNextSentences();
    
    /**
     *  Return true if current sentence by the tracking index is a leaf
     * I.E Don't have any Next Options (Not even hidden by key)
     */
    bool IsCurrentIndexLeaf();
    /**
     * Change the current tracking index to specified
     * The Index tracking the conversation is linear,
     * and folowwing the path of "activeOption"
     */
    void SeekTo(int Index);
    /**
     * Change the current tracking index by a specified amount
     * The Index tracking the conversation is linear,
     * and folowwing the path of "activeOption"
     */
    void SeekBy(int Steps);
    /**
     * Change the active option of the current Sentence.
     * After this, the sentence list needs to reload using "GetConversationChain"
     * int Index    The index of the selected option
     */
    void ChangeActiveOption(int Index);
}
