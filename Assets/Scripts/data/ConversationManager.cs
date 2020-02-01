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
        path = Application.streamingAssetsPath;
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
       Debug.Log(JsonUtility.ToJson(conversation));
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
        Debug.Log(",ssssss " + JsonUtility.ToJson(link));
        chain.Add(link);
        while (i < count && !leaf) {
            link = FindSentenceById(link.nextOptionsIds[link.activeIndex]);
            leaf = IsSentenceLeaf(link);
            Debug.Log(i+ "," + count+ "," + leaf+ "," + JsonUtility.ToJson(link));
            chain.Add(link);
            i ++;
        }
        Debug.Log("Chain: " + JsonUtility.ToJson(chain.Count));
    
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