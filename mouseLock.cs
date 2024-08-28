using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLock : MonoBehaviour
{
   

     void Update()
    {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        
       
        
    }
}
