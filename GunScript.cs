using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public float damage =10f;
    public float range = 200f;
    public float impactForce = 50f;
    public float fireRate = 10f;
  

    private float nextBullet =0f;
    public  int maxAmmo = 10;
    private int currentAmmo;
    private bool isReloading = false;
    public ParticleSystem muzzleFlash;
    public Camera fpsCam;
    AudioSource gunfire;
    public AudioSource reloading;
    public GameObject bloodEffect;

    Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
        currentAmmo = maxAmmo;
        gunfire = GetComponent<AudioSource>();

      

    }




    // Update is called once per frame
    void Update()
    {
        
        if(isReloading){
            return;
        }
        if(currentAmmo<=0){
           StartCoroutine( reload());
            return;
        }
        if (Input.GetButton("Fire1") && Time.time >= nextBullet)
        {
            gunfire.Play();

            nextBullet= Time.time + 1f/ fireRate;
            Shoot();
        }
        
        
         if (Input.GetKeyDown(KeyCode.Mouse1)){
            animator.SetBool("Scope", true);
         }
         if (Input.GetKeyUp(KeyCode.Mouse1)){
            animator.SetBool("Scope", false);
         }
       
    }
    IEnumerator reload(){
        isReloading = true;
        animator.SetBool("Reload", true);
        reloading.Play();
        yield return new WaitForSeconds(1.25f);
        animator.SetBool("Reload", false);
        yield return new WaitForSeconds(0.25f);
        currentAmmo = maxAmmo;
         isReloading = false;
        
    }
    void Shoot(){
        muzzleFlash.Play();
       
        currentAmmo--;
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position,fpsCam.transform.forward,out hit, range)){
            if(hit.rigidbody != null){
                hit.rigidbody.AddForce(-hit.normal *impactForce);
                Instantiate(bloodEffect,hit.point, Quaternion.identity);
            }

            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if(enemy != null){
                enemy.GetDamage(damage);
            }
        }
       
    }
   
}
