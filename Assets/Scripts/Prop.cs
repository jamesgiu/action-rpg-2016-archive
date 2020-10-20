using UnityEngine;
using System.Collections;

public class Prop : MonoBehaviour
{
    //SOMETHING TO DO WITH ME IS STOPPING MOVEMENT FROM OTHER SCENE SOMEHOW, SOMETHING IS LOST FROM SCENES WHEN COPIED
    public float moveSpeed;

    private Animator anim;

    private bool playerMoving;
    private Vector3 lastMove;
    public Vector3 startPos;
    public Vector2 direction;
    public Rigidbody2D myRigidbody;



    // Use this for initialization
    void Start()
    {
        startPos = transform.position;

        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        // NEW ENDLESS RUNNER BUG: ONLY Y POSITION GETS SENT TO ANIMATOR???

        //if moving to diff position than start of frame, animate the movement (i.e: when getting pushed around)
        if (startPos != transform.position)
        {

            playerMoving = true;

            //anim.SetBool("PlayerMoving", playerMoving);

            if (transform.position.x < startPos.x)
            {
                //going left
                direction.x = -1f;
            }

            if (transform.position.x > startPos.x)
            {
                //going right
                direction.x = 1f;
            }

            if (transform.position.y > startPos.y)
            {
                //going up
                direction.y = 1f;
            }

            if (transform.position.y < startPos.y)
            {
                //going down
                direction.y = -1f;
            }



            //print("I am moving");

            anim.SetFloat("LastMoveX", direction.x);
            anim.SetFloat("LastMoveY", direction.y);



        }
        else
        {
            playerMoving = false;
        }

        //sending info to the animator to play the right animation



        anim.SetFloat("MoveX", direction.x);
        anim.SetFloat("MoveY", direction.y);

        if (playerMoving == false)
        {
            direction.x = 0f;
            direction.y = 0f;
            
        }

        anim.SetBool("PlayerMoving", playerMoving);

        startPos = transform.position;
        //myRigidbody.velocity = new Vector2(0f, 0f);

    }
}
