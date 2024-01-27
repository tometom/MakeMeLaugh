using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followMouse : MonoBehaviour
{
    [Range(0,1)]
    public float xOffsetPct, yOffsetPct;
    public Vector3 restingPosition;
    public bool isLeft;

    float currLerpFactor;
    // Start is called before the first frame update
    void Start()
    {
        currLerpFactor = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLeft)
        {
            if (Input.mousePosition.x < Screen.width / 2)
            {
                currLerpFactor = 0f;
                GetComponent<RectTransform>().position = Input.mousePosition + new Vector3(-xOffsetPct*Screen.width, yOffsetPct*Screen.height, 0);
            }
            else
            {
                currLerpFactor +=Time.deltaTime;
                currLerpFactor*=currLerpFactor +0.9f;
                lerpToTarget(restingPosition,currLerpFactor);
            }
        }
        else
        {
            if (Input.mousePosition.x > Screen.width / 2)
            {
                currLerpFactor = 0f;
                GetComponent<RectTransform>().position = Input.mousePosition + new Vector3(xOffsetPct*Screen.width, yOffsetPct*Screen.height, 0);
            }
            else
            {
                currLerpFactor +=Time.deltaTime;
                currLerpFactor*=currLerpFactor + 0.9f;
                lerpToTarget(restingPosition,currLerpFactor);
            }
        }
    }
    void lerpToTarget(Vector3 targetPos, float lerpFactor)
    {
        GetComponent<RectTransform>().localPosition = Vector3.Lerp(GetComponent<RectTransform>().localPosition, targetPos,lerpFactor);
    }
}
