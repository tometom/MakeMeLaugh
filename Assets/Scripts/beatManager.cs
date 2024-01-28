using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class beatManager : MonoBehaviour
{
    public GameObject winScreen;
    public kingScript king;

    bool hasInputThisBar = false;
    const float VERY_HAPPY_THRESHOLD = 75f;
    const float HAPPY_THRESHOLD = 50f;

    const float NEUTRAL_THRESHOLD = 30f;

    const float CONFUSED_THRESHOLD = 20f;

    public int[] positionModifier = {5, 2, 5, 3};

    static public float MISS_BAR_PENALTY_MULTIPLIER = 1f;

    static string SILENCE_SOUND_ENUM = "Silence";

    static string HIT_ENUM = "HIT";
    static string MISS_ENUM = "MISS";

    static string BEAT_SETTER_ENUM = "BEAT_SETTER";
    static string PUNCH_LINE_ENUM = "PUNCH_LINE";

    static Dictionary<string, string> Bars = new Dictionary<string, string>{
        { "drop" , "0001" },
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
        { "Drum", 5f},
        { "Yoda", 5f},
        { "Error", 5f},
        { "Chipmunk", 5f},
        { SILENCE_SOUND_ENUM, 0f },
    };

    static Dictionary<string, string> soundCategories = new Dictionary<string, string>{
        { "Fart", BEAT_SETTER_ENUM},
        { "Boom", PUNCH_LINE_ENUM},
        { "Muu", PUNCH_LINE_ENUM},
        { "Chicken", BEAT_SETTER_ENUM},
        { "Drum", BEAT_SETTER_ENUM},
        { "Yoda", PUNCH_LINE_ENUM},
        { "Error", BEAT_SETTER_ENUM},
        { "Chipmunk", BEAT_SETTER_ENUM},
        {SILENCE_SOUND_ENUM, BEAT_SETTER_ENUM}
    };

    static float[] beatSetterMultiplier = { 2f, 2f, 1f, 1f };
    static float[] punchLineMultiplier = { 1f, 1f, 2.5f, 2.5f };

    public enum KingHappiness{
        FRUSTRATED,CONFUSED,NEUTRAL,HAPPY,VERYHAPPY
    }
    public static KingHappiness happiness;

    public List<Tuple<string, string>> playerInputCurrentBar = new List<Tuple<string, string>>();


    public int currentTrack = 0;
    public int currentBar   = 0;
    public int currentBeat  = 0;

    int lastInputBeat = 0;

    float barScore = 0;
    public static float totalScore = 0;

    public static int comboCount = 0;

    private Dictionary<string, int> repetitionCounter = new Dictionary<string, int>();
    public AudioSource beatSound, oneSound;
    [SerializeField]
    public static int bpm = 90;
    public static float timeTillNextBeat;
    public static float timer;
    [Range(0f, 1f)]
    public float hitTolerance;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        winScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(totalScore > 5f){
            king.killKing();
            StartCoroutine(endGame(4.5f));
        }
        timeTillNextBeat = 60f/bpm;
        timer+=Time.deltaTime;
        if(timer>= timeTillNextBeat){
            timer = 0f;
            if(currentBeat == 0){
                oneSound.Play();
            }
            else{
                beatSound.Play();
            }
            //if(isOne())
            
            if(currentBeat >= 3){
                hasInputThisBar = false;
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
                            float repPenaltyMultiplier = 1;
                            if(repetitionCounter[soundName]>1)
                                repPenaltyMultiplier = repetitionCounter[soundName];
                            print(repPenaltyMultiplier);
                            float soundScore = points[soundName] / repPenaltyMultiplier * positionModifier[beatIndex];

                            float categoryMultiplier = 1f;

                            if (soundCategories[soundName].Equals(BEAT_SETTER_ENUM))
                            {
                                categoryMultiplier = beatSetterMultiplier[beatIndex];

                            }else if(soundCategories[soundName].Equals(BEAT_SETTER_ENUM))
                            {
                                categoryMultiplier = punchLineMultiplier[beatIndex];
                            }

                            barScore += soundScore * categoryMultiplier;

                        }
                        hitArray[beatIndex] = soundName;
                        beatIndex++;

                    }else if (hitOrMiss.Equals(MISS_ENUM))
                    {
                        barScore -= MISS_BAR_PENALTY_MULTIPLIER * points[soundName];
                    }
                }
                if(hitArray[3] == null){
                    playerInputCurrentBar.Add(new Tuple<string, string>(SILENCE_SOUND_ENUM,HIT_ENUM));
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
                    if(comboCount>=10){
                        comboCount = 10;
                    }
                }else{
                    comboCount = 0;
                }
                if(barScore <= 0f){
                    barScore = 0f;
                }
                totalScore += barScore*(comboCount>0?comboCount:1);
                happiness = calchappiness(barScore);
                
                barScore = 0f;
                king.triggerChange(happiness);

            } else {   
                //Inside the current Bar
                if(!hasInputThisBar && currentBeat == 0){
                    playerInputCurrentBar.Clear();
                }
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
        }
    }

    KingHappiness calchappiness(float score){
        if(score >= VERY_HAPPY_THRESHOLD){
            return KingHappiness.VERYHAPPY;
        }
        if(score >= HAPPY_THRESHOLD){
            return KingHappiness.HAPPY;
        } 
        if(score >= NEUTRAL_THRESHOLD){
            return  KingHappiness.NEUTRAL;
        }
        if(score >= CONFUSED_THRESHOLD){
            return KingHappiness.CONFUSED;
        }
            return KingHappiness.FRUSTRATED;
        
    }

    public float getScore()
    {
        return totalScore;
    }


    public void register(string soundName) {
        if(timeTillNextBeat-timer < timeTillNextBeat * hitTolerance && currentBeat != lastInputBeat)
        {
            //Hit
            hasInputThisBar = true;
            if(currentBeat ==0 && playerInputCurrentBar.Count !=0 ){
                playerInputCurrentBar.Clear();
            }
            playerInputCurrentBar.Add(new Tuple<string, string>(soundName, HIT_ENUM));
            if(!repetitionCounter.ContainsKey(soundName))
                repetitionCounter.Add(soundName, 0);
            repetitionCounter[soundName] = repetitionCounter[soundName] + 2;
            List<string> allButSoundName = new List<string>(repetitionCounter.Count);
            foreach(var repetition in repetitionCounter.Keys){
                if(!repetition.Equals(soundName)){
                    allButSoundName.Add(repetition);
                }
            }
            foreach(var name in allButSoundName ){
                repetitionCounter[name]--;
            }
            lastInputBeat = currentBeat;
        }
        else
        {
            hasInputThisBar = true;
            //Miss
            if(currentBeat ==0 && playerInputCurrentBar.Count !=0 ){
                playerInputCurrentBar.Clear();
            }
            lastInputBeat = currentBeat;
            playerInputCurrentBar.Add(new Tuple<string, string>(soundName, MISS_ENUM));

        }
        
    }
    IEnumerator endGame(float seconds){
        yield return new WaitForSeconds(seconds);
        winScreen.SetActive(true);
        GameObject.Find("MainCamera/UI").SetActive(false);
        Time.timeScale = 0f;
    }
    public static void restartGame(){
        SceneManager.LoadScene("Main Menu");
    }
}
