using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSetter : MonoBehaviour
{

    public TextMeshProUGUI scoreText;


    // Update is called once per frame
    void Update()
    {
        scoreText.text = Math.Ceiling(beatManager.totalScore).ToString();        
    }
}
