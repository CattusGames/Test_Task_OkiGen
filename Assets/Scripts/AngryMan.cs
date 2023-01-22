using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryMan : MonoBehaviour
{
    [SerializeField] private CollectHandler _collectHandler;
    private AudioSource audioSource;
    private Animation anim;

    private void Start()
    {

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.7f;
        anim = GetComponent<Animation>();
        _collectHandler.onWrongItem.AddListener(() =>
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            if (!anim.isPlaying)
            {
                anim.Play();
            } 
        });
    }
}
