using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{

    [SerializeField]
    private AudioClip[] audios;

    [SerializeField]
    private AudioSource audioSource;

   
    public const int INTRO = 0;
    public const int SAD = 1;
    public const int ANGER_A = 2;
    public const int ANGER_B = 3;
    public const int BAD_MEMORY_A = 4;
    public const int BAD_MEMORY_B = 5;
    public const int WAIT = 6;
    public const int REVELETION_A = 7;
    public const int REVELETION_B = 8;
    public const int REVELETION_C = 9;
    public const int BAD_ENDING = 10;
    public const int HAPPY_MEMORY_A = 11;
    public const int HAPPY_MEMORY_B = 12;
    public const int GOOD_ENDING = 11;


    public void PlaySound(int sound_constant)
    {
       
        audioSource.clip = audios[sound_constant];
        audioSource.Play();
    }

    /**
     * Loop is set to false by default
     */
    public void SetLoop()
    {
        audioSource.loop = true;
    }



}
