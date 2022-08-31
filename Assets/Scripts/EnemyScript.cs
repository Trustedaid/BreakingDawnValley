using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    [Header("Other Settings")]
    NavMeshAgent navmesh;
    Animator enemyanimator;
    GameObject Target;
    [Header("GENERAL Settings")]

        float ShootRadiusValue=10; // detection radius of patrol
        float DetectionRadiusValue=15;
        Vector3 spawnpoint;
        bool Suspicion = false;

    [Header("PATROL Settings")]
    

        public GameObject[] PatrolPoint_1;
        public bool PatrolCheck;
       // bool ShootorNot=false;
        
        Coroutine PatrolDo;
    void Start()
    {
    navmesh = GetComponent<NavMeshAgent>();
    enemyanimator = GetComponent<Animator>();
    spawnpoint=transform.position;
     
    }

                IEnumerator PatrolTechnical()
                {
        enemyanimator.SetBool("walk", true);
        int sumofpoint = PatrolPoint_1.Length-1; // element ve en başta verilen sayı tutmadığı için 1 çıkarmak gerekiyo 8 ve 7
        int startingvalue = 0;
        Debug.Log(sumofpoint);
       
        
        while (true) // while(true && !PatrolCheck)
        {
        if(Vector3.Distance(transform.position, PatrolPoint_1[startingvalue].transform.position ) <= 1f)  
          {
                if(sumofpoint > startingvalue)
                {
                    ++startingvalue;
                    navmesh.SetDestination(PatrolPoint_1[startingvalue].transform.position);   // listenin içindeki 1. hedefi kendine 1. hedef olarak belirliyo

                }
                else
                {
                    navmesh.stoppingDistance = 1;
                    navmesh.SetDestination(spawnpoint);
                    if (navmesh.remainingDistance <= 1)
                    {
                        enemyanimator.SetBool("walk", false);
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                        PatrolCheck = false;
                        StopCoroutine(PatrolDo);
                    }
                }
            }       
    
        else
          {

                if (sumofpoint > startingvalue)
                {
                   
                    navmesh.SetDestination(PatrolPoint_1[startingvalue].transform.position);   // listenin içindeki 1. hedefi kendine 1. hedef olarak belirliyo

                }

               
          }
            yield return null;
    
        }
}


    private void LateUpdate()
    {
        if(PatrolCheck)
         {
            PatrolDo=StartCoroutine(PatrolTechnical()); // defining startcoroutine to patroldo
         }
         DetectionRadius();
         ShootRadius();
        
    }
    void ShootRadius() {
    
         Collider[] hitColliders =  Physics.OverlapSphere(transform.position, ShootRadiusValue);  // Detection Circle
      
                foreach (var objects in hitColliders)
                {
                    if (objects.gameObject.CompareTag("Player"))
                    {
                          enemyanimator.SetBool("walk", false);
                          navmesh.isStopped = true; //Stopping the setdestination func
                         
                          enemyanimator.SetBool("standingshoot", true);
                        
                        }
                    else
                     {
                       enemyanimator.SetBool("standingshoot", false);
                     
                    }
                    
                }


    }
      void DetectionRadius() {
         Collider[] hitColliders =  Physics.OverlapSphere(transform.position, DetectionRadiusValue);  // Detection Circle
        
                foreach (var objects in hitColliders)
                {
                    if (objects.gameObject.CompareTag("Player"))
                    {
                        Suspicion = true; //
                        
                          enemyanimator.SetBool("walk",true);
                          Target = objects.gameObject;
                          navmesh.SetDestination(Target.transform.position);
                        }
                    else
                     {
                          Target = null;
                          Suspicion = false;
                          if (transform.position!=spawnpoint) 
                          {
                             navmesh.stoppingDistance = 1; 
                             navmesh.SetDestination(spawnpoint);
                          if (navmesh.remainingDistance <=1)
                             {
                                enemyanimator.SetBool("walk",false);
                                transform.rotation = Quaternion.Euler(0, 180, 0);
                             }

                          }
                         
                    }
                    
                }


    }
      private void OnDrawGizmos()
     {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, ShootRadiusValue);
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position, DetectionRadiusValue);
     }

    // Update is called once per frame
    void Update()
    {
        
    }
}
