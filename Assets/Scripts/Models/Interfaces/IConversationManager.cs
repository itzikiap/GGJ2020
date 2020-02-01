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
    
    public bool IsLeaf;
    /**
     * Change the current tracking index to specified
     * The Index tracking the conversation is linear,
     * and folowwing the path of "activeOption"
     */
    public void SeekTo(int Index);
    /**
     * Change the current tracking index by a specified amount
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
