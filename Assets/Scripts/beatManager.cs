using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beatManager : MonoBehaviour
{

    public int[] positionModifier = {2, 4, 3, 6};

    static int BEAT_SETTER_SOUND = 1;
    static int PUNCHLINE_SOUND = 3;

    static public float MISS_BAR_PENALTY_MULTIPLIER = 1f;

    static string SILENCE_SOUND_ENUM = "SILENCE";

    static string HIT_ENUM = "HIT";
    static string MISS_ENUM = "MISS";

    static Dictionary<string, string> Bars = new Dictionary<string, string>{
        { "drop" , "0001" },
        { "zero" , "0000" },
        { "full" , "1111" },
        { "1step", "1101" },
        { "rep"  , "0101" },
        { "split" , "1001" },
        { "init" , "1000" },        
    };

    static Dictionary<string, float> points = new Dictionary<string, float>{
        { "Fart", 5f},
        { "Boom", 5f},
        { "Muu", 5f},
        { "Chicken", 5f},
        { "PLACE_HOLDER", 5f},
        { "Yoda", 5f},
        { "Error", 5f},
        { "Chipmunk", 5f},
        { "Silence", 0f },
    };

    static Dictionary<string, int> soundCategories = new Dictionary<string, int>{
        { "Fart", BEAT_SETTER_SOUND},
        { "Boom", PUNCHLINE_SOUND},
        { "Muu", PUNCHLINE_SOUND},
        { "Chicken", BEAT_SETTER_SOUND},
        { "PLACE_HOLDER", BEAT_SETTER_SOUND},
        { "Yoda", PUNCHLINE_SOUND},
        { "Error", BEAT_SETTER_SOUND},
        { "Chipmunk", BEAT_SETTER_SOUND},
    };




    private string[][] BEAT_TRACKS;
    private List<Tuple<string, string>> playerInputCurrentBar = new List<Tuple<string, string>>();


    public int currentTrack = 0;
    public int currentBar   = 0;
    public int currentBeat  = 0;

    float barScore = 0;
    float totalScore = 0;

    private Dictionary<string, int> repetitionCounter = new Dictionary<string, int>();
    AudioSource beatSound;
    [SerializeField]
    public static int bpm = 120;
    public float timeTillNextBeat;
    public float timer;
    [Range(0f, 1f)]
    public float hitTolerance;
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
        timeTillNextBeat = 60f/bpm;
        timer+=Time.deltaTime;

        if(timer>= timeTillNextBeat){
            
            if(isOne())
                beatSound.Play();
            
            if(currentBeat >= 3){
                currentBar++; 
                currentBeat = 0;
                // TODO: Calculate the Bar Score and Add to TotalScore
                // Calculating base score * position modifier
                int beatIndex = 0;
                for(int i = 0; i < playerInputCurrentBar.Count; i++)
                {   
                    string soundName = playerInputCurrentBar[i].Item1;
                    string hitOrMiss = playerInputCurrentBar[i].Item2;

                    if(hitOrMiss.Equals(HIT_ENUM))
                    {
                        barScore += points[soundName] * positionModifier[beatIndex];
                        beatIndex++;
                    }else if (hitOrMiss.Equals(MISS_ENUM))
                    {
                        barScore -= MISS_BAR_PENALTY_MULTIPLIER * points[soundName];
                    } 
                }

                totalScore += barScore;

                playerInputCurrentBar.Clear();
            } else
            {   //Inside the current Bar
                currentBeat++;
                // Checking if User chose to skip beat

                int hitCounter = 0;
                foreach (var elem in playerInputCurrentBar) {
                    if (elem.Item2.Equals(HIT_ENUM))
                    {
                        hitCounter++;
                    } 
                }

                if (hitCounter < currentBeat)
                {
                    playerInputCurrentBar.Add(new Tuple<string, string>(SILENCE_SOUND_ENUM, HIT_ENUM));
                }
            }


            timer = 0f;
        }
    }

    private bool isOne() {
        return BEAT_TRACKS[currentTrack][currentBar][currentBeat] == '1';
    }

    public float getScore()
    {
        return totalScore;
    }


    public void register(string soundName) {
        if(!repetitionCounter.ContainsKey(soundName))
            repetitionCounter.Add(soundName, 0);
        repetitionCounter[soundName] = repetitionCounter[soundName] + 1;
        if(timeTillNextBeat-timer > timeTillNextBeat * hitTolerance)
        {
            //Hit
            playerInputCurrentBar.Add(new Tuple<string, string>(soundName, HIT_ENUM));

        }
        else
        {
            //Miss
            playerInputCurrentBar.Add(new Tuple<string, string>(soundName, MISS_ENUM));
        }
        //TODO combo
        

        //TODO: Scoring



    }
}
