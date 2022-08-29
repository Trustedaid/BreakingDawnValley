using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BowEffects : MonoBehaviour
{
    [Header("SETTINGS")]
    float ShootingRate_1;
    public float ShootingRate_2; //  Defines one shoot to another shooting-time in seconds
    public float range;
    int TotalAmmo=10;
    int CapacityAmmoClip=5; // public function added to add other weapons' damage
    int RemaniningAmmo;
    float DamageAmmo=25; 
    public TextMeshProUGUI TotalAmmo_text;
    public TextMeshProUGUI RemaniningAmmo_text;

    [Header("SOUNDS")]
    public AudioClip[] ShootingSound; // same style of sources can be list as a array
    public AudioSource AuSo;
     [Header("EFFECTS")]
     public ParticleSystem[] ShootingEffects;          /*public ParticleSystem ShootDamageEffect;   public ParticleSystem ShootBloodEffect;*/ // lazım olucak
    [Header("GENERAL")]
    public Camera RayCasting; // Benim kameram
    public Animator CharAnim;
    

    
    void Start()
    {
      RemaniningAmmo = CapacityAmmoClip; // kalan
      TotalAmmo_text.text = TotalAmmo.ToString();
      RemaniningAmmo_text.text= CapacityAmmoClip.ToString();
        
    }

    void Update()
    {
      if(Input.GetKey(KeyCode.R))
      {
        if(RemaniningAmmo < CapacityAmmoClip && TotalAmmo !=0)
        {
          CharAnim.Play("Reload");
          if(RemaniningAmmo==0)
          {
            // ---- EXAMPLE -----
            // 0 
            TotalAmmo -= CapacityAmmoClip;
            //170
            RemaniningAmmo = CapacityAmmoClip;
            //30
            TotalAmmo_text.text = TotalAmmo.ToString();
            RemaniningAmmo_text.text = RemaniningAmmo.ToString();

          }
        }
        
      }
    

    if (Input.GetKeyDown(KeyCode.Mouse1 ))
       {
        if(Time.time > ShootingRate_1 && RemaniningAmmo!=0) // If time value bigger than Shootingrate_1 let it shoot in shooting function
        {

          Shoot();
          ShootingRate_1 = Time.time + ShootingRate_2;

        }
        /*if (RemaniningAmmo==0);
           {
            SOUNDS[2].Play();
           }
           */

       }
     }
     void Shoot()
    {
    
      RemaniningAmmo--;
      RemaniningAmmo_text.text = RemaniningAmmo.ToString();

      
        RaycastHit hit;
        if(Physics.Raycast(RayCasting.transform.position,RayCasting.transform.forward, out hit, range))
        {
          AuSo.PlayOneShot(ShootingSound[0]);
           Instantiate(ShootingEffects[1], hit.point, Quaternion.LookRotation(hit.normal)); 
          
            Debug.Log(hit.transform.gameObject.name);
            
            
        }
    }
    
    
}
