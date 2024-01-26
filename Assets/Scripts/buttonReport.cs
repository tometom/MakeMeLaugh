using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonReport : MonoBehaviour
{
    AudioSource soundToPlay;
    // Start is called before the first frame update
    void Start()
    {
        soundToPlay = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void report(){
        print("Button " + this.name + " pressed");
        soundToPlay.Play();
    }
}
