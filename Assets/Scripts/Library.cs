using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MMORPG
{
    public class AnimationLibrary 
 {
    private float  MaXSpeedClass;
    private float  MaXinputXClass;
    

    public void Forward_Movement()    {             // GENERAL FORWARD MOVEMENT OF LIBRARY 
    
           if (Input.GetKey(KeyCode.LeftShift))                               // If you press left shift you get speed 1 (on)
        {
            MaXSpeedClass = 1;                      // Gets the value from Library
                                                                            // inputX = MaxSpeed *  Input.GetAxis("Horizontal");
                                                                            //inputY = MaxSpeed * Input.GetAxis("Vertical");
         }
    //                                 ----------------------------------------------------- W ----------------------------------
    
        else if (Input.GetKey(KeyCode.W))
        {
            MaXSpeedClass = 0.2f;                   // Gets the value from Library
            MaXinputXClass = 1;
                                                                               // inputX = MaxSpeed * Input.GetAxis("Horizontal");
                                                                               //inputY = MaxSpeed * Input.GetAxis("Vertical");
        }
        
        else 
        {
            MaXSpeedClass = 0f;
            MaXinputXClass = 0;
                                                                               //  inputX = MaxSpeed * Input.GetAxis("Horizontal");
                                                                                // inputY = MaxSpeed * Input.GetAxis("Vertical");
        }
      
    }
    
   public float HiziDisariCikar() {
        return MaXSpeedClass;

    }

    public float YonuDisariCikar() {   
       return MaXinputXClass;

    }
    
}


}

