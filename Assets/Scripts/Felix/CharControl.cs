using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MMORPG;

public class CharControl : MonoBehaviour
{
    float inputX;
    //float inputY;    
    Animator Anim;
    Vector3 currentdirection;
    Camera MainCam;
    float maxlength=1; 
    float rotationSpeed=8;
    float MaxSpeed;
    AnimationLibrary animations = new AnimationLibrary(); // includes class 
   
    void Start()
    {
        Anim = GetComponent<Animator>();
        MainCam = Camera.main;
       
    }

    void LateUpdate()
    {
        InputMove();
        InputRotation();
    
        animations.Forward_Movement();
        
        MaxSpeed = animations.HiziDisariCikar(); 
        inputX = animations.YonuDisariCikar();
        
       
     
        // ----------------------------------------------- A -------------------------------------------------------------
         if (Input.GetKey(KeyCode.A))
        {   
            Anim.SetBool("Left_isitactive", true);

            if (Input.GetKey(KeyCode.LeftShift)) // If you press shift + a provide running left
            {
                Anim.SetFloat("leftmovement", 0.34f);  
            }
            else if (Input.GetKey(KeyCode.W)) // a + w  button left front walking
            {
                Anim.SetFloat("leftmovement", 0.63f);
            }
            else if (Input.GetKey(KeyCode.S)) //   
            { 
                Anim.SetFloat("leftmovement", 0.92f);

            }        
            else 
            {
                Anim.SetFloat("leftmovement", 0.12f);
            }
            
        }
        
         if (Input.GetKeyUp(KeyCode.A))
        {
            Anim.SetFloat("leftmovement", 0f);
            Anim.SetBool("Left_isitactive", false);
        }


// ---------------------------------------------------- D --------------------------------------------------------------------
        
         if (Input.GetKey(KeyCode.D)) // RIGHT
       {
         Anim.SetBool("Right_isitactive", true);

            if (Input.GetKey(KeyCode.LeftShift)) // If you press shift + D provide running Right
            {
                Anim.SetFloat("rightmovement", 0.34f);  
            }
            else if (Input.GetKey(KeyCode.W)) // a + w  button left front walking
            {
                Anim.SetFloat("rightmovement", 0.63f);
            }
            else if (Input.GetKey(KeyCode.S)) //   
            { 
                Anim.SetFloat("rightmovement", 0.92f);

            }        
            else 
            {
                Anim.SetFloat("rightmovement", 0.12f);
            }
            
        }
        
         if (Input.GetKeyUp(KeyCode.D))
        {
            Anim.SetFloat("rightmovement", 0f);
             Anim.SetBool("Right_isitactive", false);
        }

        // ------------------------------------------------------- S ----------------------------------------------------------------

          if (Input.GetKeyDown(KeyCode.S)) // BACK
        {
            Anim.SetBool("walkback", true);
        }
         if (Input.GetKeyUp(KeyCode.S))
        {
            Anim.SetBool("walkback", false);
        }

          if (Input.GetKeyDown(KeyCode.Mouse1))  // ----- Aim //
        {
            Anim.SetBool("aim", true);
        }
         if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            Anim.SetBool("aim", false);
        }

         /* --------------------------------------------------------------------- A KEY--------------------------------------------
         if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.LeftShift)) // If you press shift + a provide running left
            {
                Anim.SetFloat("leftmovement", 0.70f);  
            }

            else if (Input.GetKey(KeyCode.Mouse1)) // a + right mouse button aim walking
            {
                Anim.SetFloat("eftmovement", 0.37f);
            }

            else if (Input.GetKey(KeyCode.F)) //   
            { 
                Anim.SetFloat("leftmovement", 1f);

            }        
            else 
            {
                Anim.SetFloat("leftmovement", 0.20f);
            }
            
        }
        
         if (Input.GetKeyUp(KeyCode.A))
        {
            Anim.SetFloat("leftmovement", 0f);
        }
// ------------------------------------------------------------------------------ Over A key Script------------------------------- */



        currentdirection = new Vector3(inputX, 0, 0);

        /*InputMove();
        InputRotation();      
*/

    }

    void InputMove()
    {
        Anim.SetFloat("speed", Vector3.ClampMagnitude(currentdirection,MaxSpeed).magnitude,maxlength, Time.deltaTime * 10);

    }
    void InputRotation()
    {
        // Vector3 CamOfset = MainCam.transform.TransformDirection(currentdirection);

         Vector3 CamOfset = MainCam.transform.forward;
        CamOfset.y = 0;
        transform.forward = Vector3.Slerp(transform.forward,CamOfset,Time.deltaTime * rotationSpeed);

    }
}