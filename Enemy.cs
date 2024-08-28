using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   public float health = 50f;
   
   void Start() {
       GetComponent<Animator>().enabled = true;
       GetComponent<EnemyBehaviour>().enabled = true;
       GetComponent<BoxCollider>().enabled = true;
      
   }

   public void  GetDamage(float damage){
    health -= damage;
    if(health <= 0){
          
          StartCoroutine(death());
        
    }
   }
   IEnumerator death(){
           GetComponent<Animator>().enabled = false;
           GetComponent<EnemyBehaviour>().enabled = false;
           GetComponent<BoxCollider>().enabled = false;
           yield return new WaitForSeconds(4f);
           Destroy(this.gameObject);

   }
  

}
