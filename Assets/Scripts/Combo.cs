using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Combo : MonoBehaviour
{

    public int previous;
    public float scale;

    public TextMeshProUGUI text;
    public GameObject obj; 



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(beatManager.comboCount <= 1)
            obj.SetActive(false);
        else {
            obj.SetActive(true);
            text.text = beatManager.comboCount + "x";

            if(previous != beatManager.comboCount)
                scale = 0;
            
            if (scale < 2)
                scale += Time.deltaTime * 10;
            else
                scale = 2f;

            obj.transform.localScale = new Vector3(scale, scale, scale);
            previous = beatManager.comboCount;
        }        

        if(Input.GetKeyDown("k"))
            beatManager.comboCount++;
        

    }
}
