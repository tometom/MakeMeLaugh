using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static beatManager;

public class kingScript : MonoBehaviour
{

    public Animator animator;
    public SpotLightScript highlight;

    //face materials
    public Material kingAngryFaceMaterial;
    public Material kingSuspiciousFaceMaterial;
    public Material kingNeutralFaceMaterial;
    public Material kingHappyFaceMaterial;
    public Material kingSmilingFaceMaterial;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void triggerChange(KingHappiness happiness)
    {
        highlight.ChangeLightColor(happiness);
        setAndResetTrigger(happiness);
        changeFaceMaterial(happiness);
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

    public void killKing()
    {
        animator.SetTrigger("GetDead");
        animator.ResetTrigger("GetHappy");
        animator.ResetTrigger("GetSus");
        animator.ResetTrigger("GetSmiling");
        animator.ResetTrigger("GetAngry");
        animator.ResetTrigger("GetIdle");
    }

    private void changeFaceMaterial(KingHappiness happiness)
    {
        GameObject head = this.transform.Find("Head/default").gameObject;
        MeshRenderer renderer = head.GetComponent<MeshRenderer>();
        Debug.Log("################################   CURRENT FACE IS " + head.name);
        Debug.Log("################################   CURRENT MATERIAL IS " + renderer.materials[2].name);
        Material[] materials = renderer.materials;
        //.GetComponent<Renderer>();
        switch (happiness)
        {
            case KingHappiness.FRUSTRATED:
                materials[2] = kingAngryFaceMaterial;
                Debug.Log("################################   CHANGED FACE TO FRUSTRATED");
                break;


            case KingHappiness.CONFUSED:
                materials[2] = kingSuspiciousFaceMaterial;
                Debug.Log("################################   CHANGED FACE TO SUSPICIOUS");
                break;

            case KingHappiness.NEUTRAL:
                materials[2] = kingNeutralFaceMaterial;
                Debug.Log("################################   CHANGED FACE TO NEUTRAL");
                break;

            case KingHappiness.HAPPY:
                materials[2] = kingHappyFaceMaterial;
                Debug.Log("################################   CHANGED FACE TO HAPPY");
                break;

            case KingHappiness.VERYHAPPY:
                materials[2] = kingSmilingFaceMaterial;
                Debug.Log("################################   CHANGED FACE TO SMILING");
                break;


            default:
                materials[2] = kingNeutralFaceMaterial;
                Debug.Log("################################   CHANGED FACE TO NEUTRAL");
                break;

        }

        renderer.materials = materials;
    }
}
