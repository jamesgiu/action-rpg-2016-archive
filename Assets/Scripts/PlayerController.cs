using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;

    private Animator anim;
    private Rigidbody2D myRigidbody;
    private float defaultMass;

    private bool playerMoving;
    public Vector2 lastMove;

    public static bool isDead;

    public static bool isFighting;
    public static bool isRunning;
    public static bool isTalking;

    public float playerStartFaceY;
    public float playerStartFaceX;

    public bool canMove;

    private bool attacking;
    public float attackTime;
    private float attackTimeCounter;

    public bool isRecoiling;
    public float recoilTime;
    private float recoilTimeCounter;

    private static bool playerExists; //all instantiations of this object will have the same value


    // Use this for initialization
    void Start () {

        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject); //destroy extra gameobject that is created on scene load
            
        }

        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        defaultMass = myRigidbody.mass;
        recoilTimeCounter = recoilTime;

        anim.SetFloat("StartFaceY", playerStartFaceY);
        anim.SetFloat("StartFaceX", playerStartFaceX);

    }
	
	// Update is called once per frame
	void Update () {

       if(!canMove)
        {
            playerMoving = false;
            myRigidbody.mass = 300;
            myRigidbody.velocity = Vector2.zero;

            anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
            anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
            anim.SetBool("PlayerMoving", playerMoving);
            anim.SetFloat("LastMoveX", lastMove.x);
            anim.SetFloat("LastMoveY", lastMove.y);

            return; //if they can't move, don't run the move code
        }

        if (isRecoiling)
        {
           
            if (recoilTimeCounter == recoilTime)
            {
                myRigidbody.velocity = new Vector3((transform.position.x - anim.GetFloat("LastMoveX")) * 0.3f, (transform.position.y - anim.GetFloat("LastMoveY")) * 0.3f, 0f);
                //need based on lastmoveX and lastmoveY
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
            }

            return;

        }

        myRigidbody.mass = defaultMass;

        playerMoving = false;

       if(!attacking)
        {
            if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
            {
                //transform.Translate(new Vector3(Input.GetAxisRaw ("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
                // myRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, myRigidbody.velocity.y);

                myRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, 0f);

                playerMoving = true;
                lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
            }

          /*  if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
            {
                //transform.Translate(new Vector3(0f, Input.GetAxisRaw ("Vertical") * moveSpeed * Time.deltaTime, 0f));
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, Input.GetAxisRaw("Vertical") * moveSpeed);


                playerMoving = true;


                lastMove = new Vector2(0f, (Input.GetAxisRaw("Vertical")));
            }
            */

            if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f) //if not moving horizontal
            {
                myRigidbody.velocity = new Vector2(0f, myRigidbody.velocity.y);
            }

            /*
            if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f) //if not moving vertical
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0f);
            }
            */
        }
		

        if(Input.GetKeyDown(KeyCode.X))
        {
            attackTimeCounter = attackTime;
            attacking = true;
            myRigidbody.velocity = Vector2.zero;
        }

        if(attackTimeCounter > 0f)
        {
            attackTimeCounter -= Time.deltaTime;
        }

        if(attackTimeCounter <= 0f)
        {
            attacking = false;
            
        }

        //sending info to the animator to play the right animation
        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
       // anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
        anim.SetBool("Attack", attacking);



    }
}
