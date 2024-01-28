using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class beatManager : MonoBehaviour
{
    public kingScript king;
    const float VERY_HAPPY_THRESHOLD = 75f;
    const float HAPPY_THRESHOLD = 50f;

    const float NEUTRAL_THRESHOLD = 30f;

    const float CONFUSED_THRESHOLD = 20f;

    const float FRUSTRATED_THRESHOLD = 0f;
    public int[] positionModifier = {5, 2, 5, 3};

    static int BEAT_SETTER_SOUND = 1;
    static int PUNCHLINE_SOUND = 3;

    static public float MISS_BAR_PENALTY_MULTIPLIER = 1f;

    static string SILENCE_SOUND_ENUM = "Silence";

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
    static float BAR_BONUS = 10f;

    static Dictionary<string, float> points = new Dictionary<string, float>{
        { "Fart", 5f},
        { "Boom", 5f},
        { "Muu", 5f},
        { "Chicken", 5f},
        { "PLACE_HOLDER", 5f},
        { "Yoda", 5f},
        { "Error", 5f},
        { "Chipmunk", 5f},
        { SILENCE_SOUND_ENUM, 0f },
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
        {SILENCE_SOUND_ENUM, BEAT_SETTER_SOUND}
    };
    public enum KingHapiness{
        FRUSTRATED,CONFUSED,NEUTRAL,HAPPY,VERYHAPPY
    }
    public static KingHapiness hapiness;

    private string[][] BEAT_TRACKS;
    private List<Tuple<string, string>> playerInputCurrentBar = new List<Tuple<string, string>>();


    public int currentTrack = 0;
    public int currentBar   = 0;
    public int currentBeat  = 0;

    int lastInputBeat = 0;

    float barScore = 0;
    public static float totalScore = 0;

    int comboCount = 1;

    private Dictionary<string, int> repetitionCounter = new Dictionary<string, int>();
    AudioSource beatSound;
    [SerializeField]
    public static int bpm = 120;
    public static float timeTillNextBeat;
    public static float timer;
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
            
            //if(isOne())
                beatSound.Play();
            
            if(currentBeat >= 3){
                currentBar++; 
                currentBeat = 0;
                // TODO: Calculate the Bar Score and Add to TotalScore
                // Calculating base score * position modifier
                int beatIndex = 0;
                string[] hitArray = new string[4];
                for(int i = 0; i < playerInputCurrentBar.Count; i++)
                {   
                    string soundName = playerInputCurrentBar[i].Item1;
                    string hitOrMiss = playerInputCurrentBar[i].Item2;

                    if(hitOrMiss.Equals(HIT_ENUM))
                    {
                        if(!soundName.Equals(SILENCE_SOUND_ENUM)){
                            barScore += points[soundName] / repetitionCounter[soundName] * positionModifier[beatIndex];
                        }
                        hitArray[beatIndex] = soundName;
                        beatIndex++;

                    }else if (hitOrMiss.Equals(MISS_ENUM))
                    {
                        barScore -= MISS_BAR_PENALTY_MULTIPLIER * points[soundName];
                    }
                }
                if(hitArray[3] == null){
                    hitArray[3] = SILENCE_SOUND_ENUM;
                }
                StringBuilder sb = new StringBuilder(4);
                foreach(string sound in hitArray){
                    if(sound.Equals(SILENCE_SOUND_ENUM)){
                        sb.Append('0');
                    }
                    else{
                        sb.Append('1');
                    }
                }
                print(sb.ToString());
                bool hasPattern = false;
                foreach(var pattern in Bars){
                    if(pattern.Value.Equals(sb.ToString())){
                        barScore +=BAR_BONUS;
                        hasPattern = true;
                    }
                }
                if(hasPattern){
                    comboCount++;
                }else{
                    comboCount = 1;
                }
                if(barScore <= 0f){
                    barScore = 0f;
                }
                totalScore += barScore*comboCount;
                hapiness = calcHapiness(barScore);
                king.triggerChange(hapiness);
                
                barScore = 0f;
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
    KingHapiness calcHapiness(float score){
        if(score >= VERY_HAPPY_THRESHOLD){
            return KingHapiness.VERYHAPPY;
        }
        if(score >= HAPPY_THRESHOLD){
            return KingHapiness.HAPPY;
        } 
        if(score >= NEUTRAL_THRESHOLD){
            return  KingHapiness.NEUTRAL;
        }
        if(score >= CONFUSED_THRESHOLD){
            return KingHapiness.CONFUSED;
        }
            return KingHapiness.FRUSTRATED;
        
    }

    public float getScore()
    {
        return totalScore;
    }


    public void register(string soundName) {
        if(timeTillNextBeat-timer > timeTillNextBeat * hitTolerance && currentBeat != lastInputBeat)
        {
            //Hit
            playerInputCurrentBar.Add(new Tuple<string, string>(soundName, HIT_ENUM));
            if(!repetitionCounter.ContainsKey(soundName))
                repetitionCounter.Add(soundName, 0);
            repetitionCounter[soundName] = repetitionCounter[soundName] + 1;
            lastInputBeat = currentBeat;
        }
        else
        {
            //Miss
            playerInputCurrentBar.Add(new Tuple<string, string>(soundName, MISS_ENUM));
        }
        
    }
}
