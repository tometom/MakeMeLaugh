using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static beatManager;

public class CameraShakeScript : MonoBehaviour
{

    private float shakeMultiplier = 0.015f;
    private float startingX;
    private float startingY;
    private float startingZ;
    private float currentAngle = 0;

    // Start is called before the first frame update
    void Start()
    {
        startingX = this.transform.position.x;
        startingY = this.transform.position.y;
        startingZ = this.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        currentAngle += Time.deltaTime;
        this.transform.position = new Vector3(startingX + Mathf.Sin(currentAngle) *shakeMultiplier, startingY + Mathf.Cos(currentAngle+4)*0.4f* shakeMultiplier, startingZ);
    }

    public void shakeMultiplierChange(KingHappiness happiness)
    {
        switch (happiness)
        {
            case KingHappiness.FRUSTRATED:
                shakeMultiplier = 0.025f;
                break;


            case KingHappiness.CONFUSED:
                shakeMultiplier = 0.02f;
                break;

            case KingHappiness.NEUTRAL:
                shakeMultiplier = 0.015f;
                break;

            case KingHappiness.HAPPY:
                shakeMultiplier = 0.01f;
                break;

            case KingHappiness.VERYHAPPY:
                shakeMultiplier = 0.005f;
                break;


            default:
                shakeMultiplier = 0.015f;
                break;

        }
    }

}
