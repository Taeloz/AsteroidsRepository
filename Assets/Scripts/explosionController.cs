using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionController : MonoBehaviour {

    void Awakening (AudioClip soundClip)
    {
        // Play the supplied sound clip to accompany the explosion
        AudioSource destroySource = GetComponent<AudioSource>();
        destroySource.enabled = true;
        destroySource.clip = soundClip;
        destroySource.Play();
    }
}
