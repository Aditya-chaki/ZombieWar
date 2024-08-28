using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopUp : MonoBehaviour
{
   
    

 
   
     public void QuitMessage(){
          SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
