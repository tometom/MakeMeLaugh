using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beatManager : MonoBehaviour
{

    AudioSource beatSound;
    public int bpm;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        beatSound = GetComponent<AudioSource>();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float timeTillNextBeat = 60/bpm;
        timer+=Time.deltaTime;
        if(timer>= timeTillNextBeat){
            beatSound.Play();
            timer = 0;
        }
    }
}
