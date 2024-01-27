using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followMouse : MonoBehaviour
{
    public float xOffset, yOffset;
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
                GetComponent<RectTransform>().position = Input.mousePosition + new Vector3(xOffset, yOffset, 0);
            }
            else
            {
                currLerpFactor +=Time.deltaTime;
                currLerpFactor*=currLerpFactor +0.9f;
                lerpToTarget( new Vector3(-350f, 70f),currLerpFactor);
            }
        }
        else
        {
            if (Input.mousePosition.x > Screen.width / 2)
            {
                currLerpFactor = 0f;
                GetComponent<RectTransform>().position = Input.mousePosition + new Vector3(-xOffset, yOffset, 0);
            }
            else
            {
                currLerpFactor +=Time.deltaTime;
                currLerpFactor*=currLerpFactor + 0.9f;
                lerpToTarget(new Vector3(350f, 70f),currLerpFactor);
            }
        }
    }
    void lerpToTarget(Vector3 targetPos, float lerpFactor)
    {
        GetComponent<RectTransform>().localPosition = Vector3.Lerp(GetComponent<RectTransform>().localPosition, targetPos,lerpFactor);
    }
}
