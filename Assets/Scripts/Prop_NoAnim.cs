using UnityEngine;
using System.Collections;

public class Prop_NoAnim : MonoBehaviour
{

    public float moveSpeed;

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

    }

    // Update is called once per frame
    void Update()
    {

        //if moving to diff position than start of frame, animate the movement (i.e: when getting pushed around)
        if (startPos != transform.position)
        {

            playerMoving = true;

         

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





          


        }
        else
        {
            playerMoving = false;
        }

        //sending info to the animator to play the right animation



   

        if (playerMoving == false)
        {
            direction.x = 0f;
            direction.y = 0f;

        }

        startPos = transform.position;
        myRigidbody.velocity = new Vector2(0f, 0f);

    }
}
