using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beatTimer : MonoBehaviour
{
    public Vector3 lclEndingPos,lclStartingPos;
    Transform beatLine;
    // Start is called before the first frame update
    void Start()
    {
        beatLine = transform.Find("beatLine");
    }

    // Update is called once per frame
    void Update()
    {
        beatLine.localPosition = Vector3.Lerp(lclStartingPos,lclEndingPos,beatManager.timer/beatManager.timeTillNextBeat);
    }
}
