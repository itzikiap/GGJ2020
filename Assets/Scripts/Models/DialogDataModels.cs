
/**
* Main structure to hold the sentenses definition
*/
[System.Serializable]
public class DialogData {
    Sentence[] sentences;
    bool processed;
}

/**
* The base sentence structure
* Holds the information about the content of a sentence,
* and meta information about the structure of the tree
*/
[System.Serializable]
public class Sentence {
    // Content definitions
    int id;
    int tone;
    string text;
    int speaker;
    Key ownKey;
    Key unlockedBy;
    int previousId;
    int[] nextOptionsIds;

    // These definitions will be filled by the class
    Sentance previous;
    Sentence[] nextOptions;
    int activeIndex;
}

/**
* A key is a work that can open other paths
*/
[System.Serializable]
public class Key {
    int id;
    string text;
    int tone;
}
