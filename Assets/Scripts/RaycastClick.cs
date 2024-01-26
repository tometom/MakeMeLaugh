using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray =Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 500f) && Input.GetMouseButtonDown(0)){
            var btnReport = hit.transform.GetComponent<buttonReport>();
            if(btnReport){
                btnReport.report();
            }
        }
    }
}
