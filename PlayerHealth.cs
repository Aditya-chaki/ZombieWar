using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
   
    private float health;
    private float lerpTimer;
    public float maxHealth = 100;
    public float chipSpeed =2f;
    public Image frontHealthBar;
    public Image backHealthBar;
    public AudioSource hurt;
    public AudioSource heal;




    public Image overlay;
    public float duration;
    public float fadeSpeed;
    private float durationTimer;
    CapsuleCollider myCapsuleCollider;


   
    
    void Start()
    {
        myCapsuleCollider = GetComponent<CapsuleCollider>();
        health= maxHealth;
         overlay.color = new Color(overlay.color.r,overlay.color.g,overlay.color.b,0);
    }

  
    void Update()
    {
        health = Mathf.Clamp(health ,0 , maxHealth);
        UpdateHealthUI();
        if(health==0){
            SceneManager.LoadScene(0);
        }
       
       
        if(overlay.color.a>0){
            if(health<30){
                return;
            }
            durationTimer += Time.deltaTime;
            if(durationTimer >duration){
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r,overlay.color.g,overlay.color.b,tempAlpha);
            }
        }
        
        
    }
    public void  UpdateHealthUI(){
        float fillF =frontHealthBar.fillAmount;
        float fillB =backHealthBar.fillAmount;
        float hFraction = health/maxHealth;
        if(fillB> hFraction){
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer/ chipSpeed;
            percentComplete = percentComplete* percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB,hFraction,percentComplete);
        }
        if(fillF<hFraction){
             backHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.green;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer/ chipSpeed;
            percentComplete = percentComplete* percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount,percentComplete);
        }

    }
    public void  TakeDamage(float damage){
        hurt.Play();
        health -= damage;
        lerpTimer =0f;
        durationTimer =0;

         overlay.color = new Color(overlay.color.r,overlay.color.g,overlay.color.b,1);


    }
     public void RestoreHealth (float healAmount){
        health += healAmount;
        lerpTimer = 0f;

     }
      void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Enemy" ){
            TakeDamage(10);
        }
         
     }
     void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == "Health"){
            heal.Play();
            RestoreHealth(100);
            
            
        }
        if(other.gameObject.tag == "next"){
             SceneManager.LoadScene(3);
        }
        if(other.gameObject.tag == "end"){
             SceneManager.LoadScene(4);
        }
     }
}
