using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static beatManager;

public class kingScript : MonoBehaviour
{

    public Animator animator;
    public SpotLightScript highlight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void triggerChange(KingHapiness hapiness){
        highlight.ChangeLightColor(hapiness);
        //TODO: Animator
    }
}
