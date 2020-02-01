using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConversationManager : IConversationManager
{
    string path;
    string jsonRawData;
    int currentIndex;
    Sentence currentSentence;
    Key[] ObtainedKeys;
    DialogData conversation;
    public ConversationManager() {
        Start();
    }

    void Start() {
        path = Application.streamingAssetsPath;
        LoadConversationFromFile("dialogs.json");        
    }

    Sentence FindSentenceById(int id) {
        foreach (Sentence sentence in conversation.sentences) {
            if (sentence.id == id) return sentence;
        }
        return null;
    }
    bool IsSentenceLeaf(Sentence sentence) {
        return currentSentence.nextOptionsIds.GetLength(0) == 0;
    }

    // ----------- INTERFACE METHODS --------
    public void LoadConversationFromFile(string conversationFileName) {
        jsonRawData = File.ReadAllText(path + '/' + conversationFileName);
        conversation = JsonUtility.FromJson<DialogData>(jsonRawData);
    }
    public int GetIndex() {
        return currentIndex;
    }
    public bool IsCurrentIndexLeaf() {
        return IsSentenceLeaf(currentSentence);
    }
    public Sentence GetCurrentSentence() {
        return currentSentence;
    }
    public Sentence[] GetConversationChain(int Count) {
        Sentence link = GetCurrentSentence();
        List<Sentence> chain = new List<Sentence>();
        while (link != null) {
            chain.Add(link);
            link = IsSentenceLeaf(link) 
                ? null 
                : FindSentenceById(link.nextOptionsIds[link.activeIndex]);
        }
        return chain.ToArray();
    }
    public Sentence[] GetOptionalNextSentences() {
        Sentence link = GetCurrentSentence();
        List<Sentence> chain = new List<Sentence>();
        foreach (int id in link.nextOptionsIds) {
            Sentence found = FindSentenceById(id);
            if (found.unlockedByKeyId == -1 || Array.Find(ObtainedKeys, key => found.unlockedByKeyId == key.id) != null) {
                chain.Add(found);
            }
        }
        return chain.ToArray();
    }
    public void SeekTo(int Index) {

    }
    public void SeekBy(int Steps) {

    }
    public void ChangeActiveOption(int index) {
        currentSentence.activeIndex = index;
    }
    // -----------------------------------
}

class SentenceTree {
    public Sentence info;
    public Sentence previous;
    public Sentence[] nextOptions;

}