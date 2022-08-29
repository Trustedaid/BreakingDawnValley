using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour


{
    Animator animator;
    bool isMainCamera = true;  // deflinition of state

     void Start()
    {
        animator = GetComponent<Animator>(); // gets the animator component inside
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C)) // takes the C button as onepress button
        {
          cameraChange();  
        }
        
 
    }
    void cameraChange()
    {
        if(isMainCamera){
            animator.Play("TPScam"); // "TPScam" defined in Animator
        }
        else {
        animator.Play("FPScam"); // "FPScam" defined in Animator
        }
        isMainCamera = !isMainCamera; // true goes false if state is false it goes true
    }
}
