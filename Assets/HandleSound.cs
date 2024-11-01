using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandleSound : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(float start, float end)
    {
        audioSource.volume = 50;
        audioSource.time = start;
        audioSource.Play();
        StartCoroutine(WaitTilEnd(end));
    }

    IEnumerator WaitTilEnd(float end)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            audioSource.volume -= 0.1f;
            if (audioSource.time >= end)
            {
                audioSource.Stop();
                break;
            }
        }
    }
}
