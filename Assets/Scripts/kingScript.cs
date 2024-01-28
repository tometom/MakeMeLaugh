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
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void triggerChange(KingHappiness happiness){
        highlight.ChangeLightColor(happiness);
        setAndResetTrigger(happiness);
    }


    private void setAndResetTrigger(KingHappiness happiness)
    {
        switch (happiness)
        {
            case KingHappiness.FRUSTRATED:
                animator.ResetTrigger("GetDead");
                animator.ResetTrigger("GetHappy");
                animator.ResetTrigger("GetIdle");
                animator.ResetTrigger("GetSmiling");
                animator.ResetTrigger("GetSus");
                animator.SetTrigger("GetAngry");
                break;


            case KingHappiness.CONFUSED:
                animator.ResetTrigger("GetDead");
                animator.ResetTrigger("GetHappy");
                animator.ResetTrigger("GetIdle");
                animator.ResetTrigger("GetSmiling");
                animator.ResetTrigger("GetAngry");
                animator.SetTrigger("GetSus");
                break;

            case KingHappiness.NEUTRAL:
                animator.ResetTrigger("GetDead");
                animator.ResetTrigger("GetHappy");
                animator.ResetTrigger("GetSus");
                animator.ResetTrigger("GetSmiling");
                animator.ResetTrigger("GetAngry");
                animator.SetTrigger("GetIdle");
                break;

            case KingHappiness.HAPPY:
                animator.ResetTrigger("GetDead");
                animator.ResetTrigger("GetIdle");
                animator.ResetTrigger("GetSus");
                animator.ResetTrigger("GetSmiling");
                animator.ResetTrigger("GetAngry");
                animator.SetTrigger("GetHappy");
                break;

            case KingHappiness.VERYHAPPY:
                animator.ResetTrigger("GetDead");
                animator.ResetTrigger("GetIdle");
                animator.ResetTrigger("GetSus");
                animator.ResetTrigger("GetHappy");
                animator.ResetTrigger("GetAngry");
                animator.SetTrigger("GetSmiling");
                break;


            default:
                animator.ResetTrigger("GetDead");
                animator.ResetTrigger("GetHappy");
                animator.ResetTrigger("GetSus");
                animator.ResetTrigger("GetSmiling");
                animator.ResetTrigger("GetAngry");
                animator.SetTrigger("GetIdle");
                break;

        }
    }
}
