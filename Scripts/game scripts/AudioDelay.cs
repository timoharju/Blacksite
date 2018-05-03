using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioDelay : MonoBehaviour
{
    AudioSource audioBeam;
    

    void Awake()
    {
        audioBeam = GameObject.Find("Beam").GetComponent<AudioSource>();
        audioBeam.PlayDelayed(3.8f);


    }
}
