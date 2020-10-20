using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{

    public float moveSpeed;

    private Rigidbody2D myRigidbody;

    private bool moving;

    public int damageDealt;

    public float timeBetweenMove;
    private float timeBetweenMoveCounter;

    public float timeToMove;
    private float timeToMoveCounter;

    public float timeBetweenAttack;
    private float timeBetweenAttackCounter;

    //public float waitToReload;
    private float runtimer = 10f;
    //private bool reloading;

    public bool isRecoiling;
    public float recoilTime;
    private float recoilTimeCounter;

    public bool runningToPlayer;
  

    private PlayerController thePlayer;

    private FleeingEnemyController colliderEntity;

    public bool isFighting;
    public bool isDefeated;



    private Vector3 moveDirection;

    //maybe should create a huge controller variable that watches for if statements and changes music based on that
    public AudioSource BGMusic;
    public AudioClip BGMusicOrig;
    public AudioClip myMusic;

    private Animator myAnim;
    public Animator myLegsAnim;
 

    // Use this for initialization
    void Start()
    {
 

        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();

        thePlayer = FindObjectOfType<PlayerController>();

        //timeBetweenMoveCounter = timeBetweenMove;
        //timeToMoveCounter = timeToMove;

        timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
        timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);
        timeBetweenAttackCounter = timeBetweenAttack;

        recoilTimeCounter = recoilTime;

        BGMusic = FindObjectOfType<AudioSource>();
        BGMusicOrig = BGMusic.clip;


    }

    // Update is called once per frame
    void Update()
    {

        if (isRecoiling)
        {
            myAnim.SetBool("Staggering", isRecoiling);
            myLegsAnim.SetBool("Staggering", isRecoiling);

            myAnim.SetBool("PlayerMoving", false);
            myLegsAnim.SetBool("PlayerMoving", false);

            if (recoilTimeCounter == recoilTime)
            {
                myRigidbody.velocity = new Vector3((transform.position.x - myAnim.GetFloat("LastMoveX")) * 0.2f, (transform.position.y - myAnim.GetFloat("LastMoveY")) * 0.2f, 0f);
                //might need based on lastmoveX and lastmoveY
            }





            //moving = true;
            recoilTimeCounter -= Time.deltaTime;

            //myRigidbody.mass = 300;

            if (recoilTimeCounter < 0f)
            {
                recoilTimeCounter = recoilTime;
                isRecoiling = false;
                //moving = false;
                myRigidbody.velocity = Vector2.zero;

                myAnim.SetBool("Staggering", isRecoiling);
                myLegsAnim.SetBool("Staggering", isRecoiling);

                myAnim.SetBool("PlayerMoving", true);
                myLegsAnim.SetBool("PlayerMoving", true);

            }


           
           

            return;

        }

        if (moving && !runningToPlayer)
        {
            timeToMoveCounter -= Time.deltaTime;

            myRigidbody.velocity = moveDirection;

            if (timeToMoveCounter < 0f)
            {
                moving = false;
                //timeBetweenMoveCounter = timeBetweenMove;
                timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
            }
        }
        else if (!runningToPlayer && !isFighting)
        {
            timeBetweenMoveCounter -= Time.deltaTime; //counting down
            myRigidbody.velocity = Vector2.zero; //an empty vector 2

            if (timeBetweenMoveCounter < 0f)
            {
                moving = true;

                //timeToMoveCounter = timeToMove;
                timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);

                moveDirection = new Vector3(Random.Range(-1f, 1f) * moveSpeed, Random.Range(-1f, 1f) * moveSpeed, 0f);
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

        if (runningToPlayer && !isFighting)
        {
            Debug.Log("I am running to player");
            PlayerController.isRunning = true;
            
            Vector3 targetPos = new Vector3(thePlayer.transform.position.x, thePlayer.transform.position.y, thePlayer.transform.position.z);

            transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
            moving = true;
            runtimer -= Time.deltaTime;

            //myRigidbody.mass = 300;

            if (runtimer < 0f)
            {
                runningToPlayer = false;
                PlayerController.isRunning = false;
                BGMusic.clip = BGMusicOrig;
                BGMusic.Play();
                runtimer = 10f;
                moving = false;
            }
        }

        //if(isDefeated && !isDefeatedMusicChange)
        //{
        //    BGMusic.clip = myDefeatMusic;
        //    BGMusic.Play();
        //    isDefeatedMusicChange = true;
        //}

        if(isFighting)
        {
            Vector3 targetPos = new Vector3(thePlayer.transform.position.x, thePlayer.transform.position.y, thePlayer.transform.position.z);
            moving = true;
            transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);

            if(PlayerController.isDead)
            {
                isFighting = false;
            }

        }

        myAnim.SetBool("PlayerMoving", moving);
        myLegsAnim.SetBool("PlayerMoving", moving);

    }

    void OnCollisionEnter2D(Collision2D other) //physical box
    {
        if(!isDefeated)
        {
            if (other.gameObject.tag == "Player" && !isFighting)
            {
                if (runningToPlayer == true)
                {
                    runningToPlayer = false;
                    Debug.Log("I have begun fighting player");
                    isFighting = true;
                    PlayerController.isFighting = true;
                }

                return;
            }

           else if (other.gameObject.tag == "Player" && isFighting)
            {
                other.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damageDealt);

                other.gameObject.GetComponent<PlayerController>().isRecoiling = true;

               
                //instead of using LERP, lets use VELOCITY.. no out of bounds, plus maybe smoOTH!!
                //getting there, just need smoother
                //other.gameObject.GetComponent<Rigidbody2D>().gravityScale = 300;

                //other.gameObject.GetComponent<SpriteRenderer>().color = new Color32(233, 27, 27, 255);
            }
        }
        
    }

   

}
