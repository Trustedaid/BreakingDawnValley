
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    [Header("Other Settings")] //----------------------------------- OTHER SETTINGS--------------------------//
    NavMeshAgent navmesh;
    Animator enemyanimator;
    GameObject Target;
    [Header("GENERAL Settings")] //----------------------------------- GENERAL SETTINGS----------------------//

    float ShootRadiusValue=7; // detection radius of patrol
        float DetectionRadiusValue=15;
        Vector3 spawnpoint;
        bool Suspicion = false;

    [Header("PATROL Settings")] //----------------------------------- PATROL SETTINGS------------------------//
    

        public GameObject[] PatrolPoint_1;
        public GameObject[] PatrolPoint_2;
        public GameObject[] PatrolPoint_3;



    GameObject[] ActivatedPointList;
    bool PatrolCheck;
    // bool ShootorNot=false;
    /*public */ bool PatrolLock;
    public bool PatrolCanDo;   
    Coroutine PatrolDo;
    Coroutine PatrolTime;
    void Start()
    {
    navmesh = GetComponent<NavMeshAgent>();
    enemyanimator = GetComponent<Animator>();
    spawnpoint = transform.position;
    StartCoroutine(PatrolTimeCheck());
     
    }
   GameObject[] PatrolContol()
    {
        int value = Random.Range(1, 3);
            switch(value)   // kaldığım yer 25x 6.58
        {
            case 1:
                ActivatedPointList = PatrolPoint_1;
                    break;
            case 2:
                ActivatedPointList = PatrolPoint_2;
                break;
            case 3:
                ActivatedPointList = PatrolPoint_3;
                break;

        }
        return ActivatedPointList;
     
    }
                IEnumerator PatrolTimeCheck()
    {
        while (true && !PatrolCheck && PatrolCanDo)
        {
           

                yield return new WaitForSeconds(5f);

                PatrolLock = true;
                StopCoroutine(PatrolTime);
            
        }
    }
                IEnumerator PatrolTechnical(GameObject[] objectInputs)
                {
                    navmesh.isStopped =false;
                    PatrolLock = false; // devriyeyi başlatan kilit
                    PatrolCheck = true; //sistemde devriye olup olmadığını kontrol eden
                    enemyanimator.SetBool("walk", true);
                    int sumofpoint = objectInputs.Length-1; // element ve en başta verilen sayı tutmadığı için 1 çıkarmak gerekiyo 8 ve 7
                    int startingvalue = 0;
                    navmesh.SetDestination(objectInputs[startingvalue].transform.position);
       
        
        while (true && PatrolCanDo) // while(true && !PatrolCheck)
        {
            if (Vector3.Distance(transform.position, objectInputs[startingvalue].transform.position ) <= 1f)  
            {
                if(sumofpoint>startingvalue)
                {
                    ++startingvalue;
                    navmesh.SetDestination(objectInputs[startingvalue].transform.position);   // listenin içindeki 1. hedefi kendine 1. hedef olarak belirliyo

                }
                else
                {
                    navmesh.stoppingDistance = 1;
                    navmesh.SetDestination(spawnpoint);
                   
                }
            }       
    
        else
          {

                if (sumofpoint > startingvalue)
                {
                   
                    navmesh.SetDestination(objectInputs[startingvalue].transform.position);   // listenin içindeki 1. hedefi kendine 1. hedef olarak belirliyo

                }

               
          }
            yield return null;
    
        }

}


    private void LateUpdate()
    {
        if (navmesh.stoppingDistance==1 && navmesh.remainingDistance <= 1)
        {
            enemyanimator.SetBool("walk", false);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            PatrolCheck = false;
            PatrolTime = StartCoroutine(PatrolTimeCheck());
            if (PatrolDo!=null)    
         StopCoroutine(PatrolDo);

            navmesh.stoppingDistance = 0;
            navmesh.isStopped = true;
        }
        
            if (PatrolLock && PatrolCanDo)
            {
                PatrolDo = StartCoroutine(PatrolTechnical(PatrolContol())); // defining startcoroutine to "patroldo"


            }
            DetectionRadius();
            // ShootRadius();

        }
        void ShootRadius() {

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, ShootRadiusValue);  // Detection Circle

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
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, DetectionRadiusValue);  // Detection Circle

            foreach (var objects in hitColliders)
            {
                if (objects.gameObject.CompareTag("Player"))
                {


                    enemyanimator.SetBool("walk", true);
                    Target = objects.gameObject;
                    navmesh.SetDestination(Target.transform.position);
                    Suspicion = true; //
                StopCoroutine(PatrolDo);
            }
                else
                {
                if (Suspicion)
                {
                    Target = null;
                    
                    if (transform.position != spawnpoint)
                    {
                        navmesh.stoppingDistance = 1;
                        navmesh.SetDestination(spawnpoint);
                        if (navmesh.remainingDistance <= 1)
                        {
                            enemyanimator.SetBool("walk", false);
                            transform.rotation = Quaternion.Euler(0, 180, 0);
                        }

                    }
                    Suspicion = false;
                    PatrolDo = StartCoroutine(PatrolTechnical(PatrolContol())) ;


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
   
