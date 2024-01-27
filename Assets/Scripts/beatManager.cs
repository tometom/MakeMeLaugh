using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beatManager : MonoBehaviour
{

    public int[] positionModifier = {2, 4, 3, 6};

    static Dictionary<string, string> Bars = new Dictionary<string, string>{
        { "drop" , "0001" },
        { "zero" , "0000" },
        { "full" , "1111" },
        { "1step", "1101" },
        { "rep"  , "0101" },
        { "split" , "1001" },
        { "init" , "1000" },        
    };

    static Dictionary<string, string> points = new Dictionary<string, int>{
        { "Muu", 5},
        { "Muu", 5},
        { "Muu", 5},
        { "Muu", 5},
        { "Muu", 5},
    };




    private string[][] BEAT_TRACKS;


    public int currentTrack = 0;
    public int currentBar   = 0;
    public int currentBeat  = 0;

    private Dictionary<string, int> repetitionCounter = new Dictionary<string, int>();
    AudioSource beatSound;
    [SerializeField]
    public static int bpm = 120;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        BEAT_TRACKS = new string[][] {
            new string[] { Bars["full"], Bars["split"], Bars["full"], Bars["split"], Bars["init"], Bars["zero"], Bars["drop"], Bars["full"], Bars["split"], Bars["full"], Bars["split"], Bars["init"], Bars["zero"], Bars["drop"],Bars["full"], Bars["split"], Bars["full"], Bars["split"], Bars["init"], Bars["zero"], Bars["drop"],Bars["full"], Bars["split"], Bars["full"], Bars["split"], Bars["init"], Bars["zero"], Bars["drop"],Bars["full"], Bars["split"], Bars["full"], Bars["split"], Bars["init"], Bars["zero"], Bars["drop"],  },
        };

        Debug.Log(BEAT_TRACKS);


        beatSound = GetComponent<AudioSource>();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float timeTillNextBeat = 60f/bpm;
        timer+=Time.deltaTime;

        if(timer>= timeTillNextBeat){
            
            if(isOne())
                beatSound.Play();
            
            if(currentBeat >= 3){
                currentBar++; 
                currentBeat = 0;
            } else 
                currentBeat++;

            timer = 0f;
        }
    }

    private Boolean isOne() {
        return BEAT_TRACKS[currentTrack][currentBar][currentBeat] == '1';
    }


    public void register(string soundName) {
        if(!repetitionCounter.ContainsKey(soundName))
            repetitionCounter.Add(soundName, 0);
        repetitionCounter[soundName] = repetitionCounter[soundName] + 1;
    
        int supposedScore = 

        int score = 0;
        // check if on beat
        if(!isOne())

    
    }
}
