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
    List<Key> ObtainedKeys;
    DialogData conversation;
    public ConversationManager() {
        path = Application.streamingAssetsPath;
        this.ObtainedKeys = new List<Key>();
        LoadConversationFromFile("conversation.json");        
    }

    Sentence FindSentenceById(int id) {
        foreach (Sentence sentence in conversation.sentences) {
            if (sentence.id == id) return sentence;
        }
        return null;
    }
    bool IsSentenceLeaf(Sentence sentence) {
        return sentence.nextOptionsIds.GetLength(0) == 0;
    }


    // ----------- Iterator -----------
    Sentence FirstItem { get{
        SeekTo(0);
        return currentSentence;
    }}
    Sentence NextItem{ get{
        SeekBy(1);
        return currentSentence;
    }}
    Sentence CurrentItem{ get{
        return currentSentence;
    }}
    bool IsDone { get {
        return IsCurrentIndexLeaf();
    }}
    // ----------- INTERFACE METHODS --------
    public void LoadConversationFromFile(string conversationFileName) {
        jsonRawData = File.ReadAllText(path + '/' + conversationFileName);
        conversation = JsonUtility.FromJson<DialogData>(jsonRawData);
    //    Debug.Log(JsonUtility.ToJson(conversation));
        SeekTo(0);
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
    public Sentence[] GetConversationChain(int count) {
        Sentence link = GetCurrentSentence();
        List<Sentence> chain = new List<Sentence>();

        bool leaf = false;
        int i = 0;
        int nextId = link.id;
        while (i < count && !leaf) {
            link = FindSentenceById(nextId);
            leaf = IsSentenceLeaf(link);
            // Debug.Log(i+ "," + count+ "," + leaf+ "," + JsonUtility.ToJson(link));
            if (!leaf) nextId = link.nextOptionsIds[link.activeIndex];
            chain.Add(link);
            i ++;
        }
        return chain.ToArray();
    }
    public Sentence[] GetOptionalNextSentences() {
        Sentence link = GetCurrentSentence();
        List<Sentence> chain = new List<Sentence>();
        foreach (int id in link.nextOptionsIds) {
            Sentence found = FindSentenceById(id);
            if (found.unlockedByKeyId == -1 || Array.Find(ObtainedKeys.ToArray(), (Key key) => found.unlockedByKeyId == key.id) != null) {
                chain.Add(found);
            }
        }
        return chain.ToArray();
    }
    public void SeekTo(int index) {
        bool leaf = false;
        int i = 0;
        Sentence link = conversation.sentences[0];
        while (i < index && !leaf) {
            link = FindSentenceById(link.nextOptionsIds[link.activeIndex]);
            leaf = IsSentenceLeaf(link);
            i ++;
        }
        currentIndex = i;
        currentSentence = link;
    }
    public void SeekBy(int steps) {
        SeekTo(currentIndex + steps);
    }
    public void AddKeysIds(int[] keysIds) {
        foreach(int k in keysIds) {
            Key found = Array.Find(this.conversation.keys, (Key key) => k == key.id);
            if (found != null && Array.Find(this.ObtainedKeys.ToArray(), (Key key) => found.id == key.id) != null) {
                this.ObtainedKeys.Add(found);
            }
        }
    }
    public Key[] GetObtainedKeys() {
        return this.ObtainedKeys.ToArray();
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