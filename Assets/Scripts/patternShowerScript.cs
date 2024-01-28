using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class patternShowerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] beats;
    public Sprite[] images;
    public beatManager manager;
    Dictionary<string,int> imageNames = new Dictionary<string,int>{
        { "Fart", 3},
        { "Boom", 7},
        { "Muu", 0},
        { "Chicken", 1},
        { "Drum", 4},
        { "Yoda", 5},
        { "Error", 2},
        { "Chipmunk", 6},
        { "Silence", 8}
    };
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        int j = 0;
        for(int i = 0; i < manager.playerInputCurrentBar.Count;i++){
            if(manager.playerInputCurrentBar[i].Item2.Equals("HIT") && j<4){
                beats[j].SetActive(true);
                beats[j].GetComponent<Image>().sprite =images[imageNames[manager.playerInputCurrentBar[i].Item1]];
                j++;
            }
        }
        for(int i = manager.playerInputCurrentBar.Count; i <4;i++){
            beats[i].SetActive(false);
        }   
    }
}
