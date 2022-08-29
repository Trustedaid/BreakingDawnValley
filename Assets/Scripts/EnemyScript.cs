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
    [Header("PATROL Settings")]

        public GameObject[] PatrolPoint_1;
        public bool PatrolCheck;
        bool ShootorNot=false;
        bool Suspicion=false;
        IEnumerator PatrolDo;
    void Start()
    {
    navmesh = GetComponent<NavMeshAgent>();
    enemyanimator = GetComponent<Animator>();
    spawnpoint=transform.position;
    }

IEnumerator PatrolTechnical(){

    int sumofpoint = PatrolPoint_1.Length;
    int startingvalue = 0;
while(true && !PatrolCheck)
{
    if(transform.position != PatrolPoint_1[startingvalue].transform.position)  
    {
    navmesh.SetDestination(Target.transform.position);   // listenin içindeki 1. hedefi kendine 1. hedef olarak belirliyo

    }
    
    else
    {
            startingvalue++;

    }
    yield return null;
    
}
}


    private void LateUpdate()
    {
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
                         enemyanimator.SetBool("walk", false);
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
