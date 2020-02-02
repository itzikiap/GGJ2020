
/**
* Main structure to hold the sentenses definition
*/
[System.Serializable]
public class DialogData {
    public Sentence[] sentences;
    public Key[] keys;
    public bool processed;
}

/**
* The base sentence structure
* Holds the information about the content of a sentence,
* and meta information about the structure of the tree
*/
[System.Serializable]
public class Sentence {
    // Content definitions
    public int id;
    public int tone;
    public string bubble = "1A";
    public string text;
    public int speaker;
    public int audio = 1;
    public int[] expression;
    public bool audioloop = true;
    public int[] hasKeysIds;
    public int unlockedByKey = -1;
    public int previousId;
    public int[] nextOptionsIds;
    public int activeIndex;
}

/**
* A key is a work that can open other paths
*/
[System.Serializable]
public class Key {
    public int id;
    public string text;
    public int tone;
}
