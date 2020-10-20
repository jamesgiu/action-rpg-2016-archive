using UnityEngine;
using System.Collections;


public class ReflectionController : MonoBehaviour
{

    private Animator anim;
    public Animator parentAnimator;



    private bool playerMoving;
    private Vector2 lastMove;

    public float playerStartFaceY;
    public float playerStartFaceX;

  


    // Use this for initialization
    void Start()
    {

        anim = GetComponent<Animator>();
        //parentAnimator = GetComponentInParent<Animator>();




    }

    // Update is called once per frame
    void Update()
    {


        //playerMoving = false;

        //if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        //{
        //    //transform.Translate(new Vector3(Input.GetAxisRaw ("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));



        //    playerMoving = true;
        //    lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
        //}

        //if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
        //{
        //    //transform.Translate(new Vector3(0f, Input.GetAxisRaw ("Vertical") * moveSpeed * Time.deltaTime, 0f));



        //    playerMoving = true;


        //    lastMove = new Vector2(0f, (Input.GetAxisRaw("Vertical")));
        //}




        //sending info to the animator to play the right animation from the parent animator (reflection must imitate animation of
        //what it is reflecting)
        anim.SetBool("PlayerMoving", parentAnimator.GetBool("PlayerMoving"));
        anim.SetFloat("MoveX", parentAnimator.GetFloat("MoveX"));
        anim.SetFloat("MoveY", parentAnimator.GetFloat("MoveY"));
      
        anim.SetFloat("LastMoveX", parentAnimator.GetFloat("LastMoveX"));
        anim.SetFloat("LastMoveY", parentAnimator.GetFloat("LastMoveY"));
        anim.SetBool("Attack", parentAnimator.GetBool("Attack"));


    }
}
