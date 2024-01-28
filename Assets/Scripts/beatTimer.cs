using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beatTimer : MonoBehaviour
{
    public Vector3 lclEndingPos,lclStartingPos;
    Transform beatLine;
    float twoBeatTimer;
    // Start is called before the first frame update
    void Start()
    {
        twoBeatTimer = 0f;
        beatLine = transform.Find("beatLine");
    }

    // Update is called once per frame
    void Update()
    {
        twoBeatTimer += Time.deltaTime;
        if(twoBeatTimer >= beatManager.timeTillNextBeat*2){
            twoBeatTimer = 0f;
        }
        beatLine.localPosition = Vector3.Lerp(lclStartingPos,lclEndingPos,twoBeatTimer/(beatManager.timeTillNextBeat*2));
    }
}
