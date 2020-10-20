using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FleeingEnemyController : MonoBehaviour {

    public float moveSpeed;

    private Rigidbody2D myRigidbody;
    private Animator myAnim;

    public bool moving;

    public float timeBetweenMove;
    private float timeBetweenMoveCounter;

    private float mass;

    public float timeToMove;
    private float timeToMoveCounter;

    //public float waitToReload;
    private float runtimer = 2f;
    //private bool reloading;

    private bool runningFromPlayer;

    public bool canFlee;
    public bool canMove;

    private PlayerController thePlayer;
    private FleeingEnemyController colliderEntity;

    private Vector3 moveDirection;


	// Use this for initialization
	void Start () {

        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();

        mass = myRigidbody.mass;
        //timeBetweenMoveCounter = timeBetweenMove;
        //timeToMoveCounter = timeToMove;

        timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
        timeBetweenMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);

    }
	
	// Update is called once per frame
	void Update () {

        if(!canMove)
        {
            moving = false;
            myRigidbody.velocity = Vector2.zero;
           
            return;
        }

        myRigidbody.mass = mass; //in case activateTextAtLine changed this

        if (moving && !runningFromPlayer)
        {
            timeToMoveCounter -= Time.deltaTime;

            myRigidbody.velocity = moveDirection;

            if(timeToMoveCounter < 0f)
            {
                moving = false;
                //timeBetweenMoveCounter = timeBetweenMove;
                timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
            }
        }
        else if(!runningFromPlayer)
        {
            timeBetweenMoveCounter -= Time.deltaTime; //counting down
            myRigidbody.velocity = Vector2.zero; //an empty vector 2

            if(timeBetweenMoveCounter < 0f)
            {
                moving = true;

                //timeToMoveCounter = timeToMove;
                timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);

                moveDirection = new Vector3(Random.Range(-1f, 1f) * moveSpeed, 0f, 0f);

              
            }
        }

        //if(reloading)
        //{
        //    waitToReload -= Time.deltaTime;
        //    if(waitToReload < 0f)
        //    {
        //        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //        thePlayer.gameObject.SetActive(true);
        //    }
        //}

        if(runningFromPlayer)
        {
            moving = true;

            if(runtimer == 2f)
            {
                moveDirection = new Vector3(Random.Range(-1f, 1f) * moveSpeed * 7, 0f, 0f);
                
            }

            myRigidbody.mass = 0.01f;
            myRigidbody.velocity = moveDirection;
           
            runtimer -= Time.deltaTime;



            if (runtimer < 0f)
            {
                myRigidbody.velocity = Vector2.zero;

                runtimer = 2f;
                myRigidbody.mass = mass;

                runningFromPlayer = false;
                moving = false;
                
            }
        }

        myAnim.SetBool("PlayerMoving", moving);

        //animations
        //myAnim.SetBool("MoveX", );

    }

    void OnCollisionEnter2D (Collision2D other)
    {
        if (!canFlee)
        {
            return;
        }

        if (other.gameObject.tag == "Player")
        {

           

            thePlayer = other.gameObject.GetComponent<PlayerController>();

          


            runningFromPlayer = true;

            


            //death script below
            //other.gameObject.SetActive(false);
            //reloading = true;


        }


    }
}
