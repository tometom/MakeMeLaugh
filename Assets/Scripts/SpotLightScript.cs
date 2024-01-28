using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static beatManager;

public class SpotLightScript : MonoBehaviour
{

    Light spotlight;
    
    // Start is called before the first frame update
    void Start()
    {
        spotlight = GetComponent<Light>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeLightColor(KingHapiness.FRUSTRATED);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeLightColor(KingHapiness.CONFUSED);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeLightColor(KingHapiness.NEUTRAL);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeLightColor(KingHapiness.HAPPY);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangeLightColor(KingHapiness.VERYHAPPY);
        }
    }

    public void ChangeLightColor(KingHapiness happiness)
    {
        Color colorToChange = new Color(r: (255f/255f), g: (0f / 255f), b: (0f / 255f));
        switch (happiness)
        {
            case KingHapiness.FRUSTRATED:
                colorToChange = new Color(r: (255f / 255f), g: (0f / 255f), b: (0f / 255f));
                break;


            case KingHapiness.CONFUSED:
                colorToChange = new Color(r: (242f / 255f), g: (120f / 255f), b: (58f / 255f));
                break;

            case KingHapiness.NEUTRAL:
                colorToChange = new Color(r: (240f / 255f), g: (222f / 255f), b: (164f / 255f));
                break;

            case KingHapiness.HAPPY:
                colorToChange = new Color(r: (156f / 255f), g: (255f / 255f), b: (0f / 255f));
                break;

            case KingHapiness.VERYHAPPY:
                colorToChange = new Color(r: (0f / 255f), g: (255f / 255f), b: (0f / 255f));
                break;


            default:
                colorToChange = new Color(r: (240 / 255), g: (222 / 255), b: (164 / 255));
                break;

        }

        spotlight.color = colorToChange;
    }

}
