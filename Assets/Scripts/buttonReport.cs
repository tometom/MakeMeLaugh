using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class buttonReport : MonoBehaviour
{



    public UnityEvent onClick;

    AudioSource soundToPlay;
    public float angleDifference;
    float startingAngle,endingAngle;
    // Start is called before the first frame update
    void Start()
    {
        soundToPlay = GetComponent<AudioSource>();
        startingAngle = transform.rotation.eulerAngles.x;
        endingAngle = startingAngle + angleDifference;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void report(){
        print("Button " + this.name + " pressed");
        soundToPlay.Play();
        rotate();
        onClick.Invoke();
    }
    public void rotate(){
        StartCoroutine(rotateCountdown(30f/beatManager.bpm));
    }
    IEnumerator rotateCountdown(float seconds){
        transform.rotation = Quaternion.Euler(endingAngle,0,0);
        yield return new WaitForSeconds(seconds);
        transform.rotation = Quaternion.Euler(startingAngle,0,0);
    }
}
