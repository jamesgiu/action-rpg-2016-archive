using UnityEngine;
using System.Collections;

public class ActivateTextAtLine : MonoBehaviour {

    public TextAsset theText;

    public int startLine;
    public int endLine;

    public bool notAttachedToObject;

    private PlayerController thePlayer;
    private FleeingEnemyController thePerson;
    private Animator thePersonAnimator;

 
   
    public Sprite portrait;

    public TextBoxManager textBox;

    public bool destroyWhenFinished;
    
    public bool requireButtonPress;
    private bool waitForPress;

	// Use this for initialization
	void Start () {

        

        textBox = FindObjectOfType<TextBoxManager>();
        thePlayer = FindObjectOfType<PlayerController>();

        if(requireButtonPress) //if not a shout, then the script is on the person object
        {
            thePerson = GetComponent<FleeingEnemyController>();
            thePersonAnimator = GetComponent<Animator>();
        }

        waitForPress = false;
      

	}
	
	// Update is called once per frame
	void Update () {
	
        if(requireButtonPress)
        {

            //if (textBox.currentLine > endLine)
            //{
            //    waitForPress = true;
            //}


            if (waitForPress == true && Input.GetKeyDown(KeyCode.Z) && !PlayerController.isTalking)
            {
                //set so that the player has to be facing toward the dood to activate chat transform.position.x stuff
                waitForPress = false;
                PlayerController.isTalking = true;
                //cant hit z twice in a row now
                thePerson.canMove = false;
                thePerson.GetComponent<Rigidbody2D>().mass = 300;

                if (thePersonAnimator != null)
                {
                    thePersonAnimator.SetFloat("LastMoveX", thePlayer.transform.position.x - transform.position.x);
                    thePersonAnimator.SetFloat("LastMoveY", thePlayer.transform.position.y - transform.position.y);
                    //dont forget legs for teachers... i.e: if tag == teacher then get in child the legs
                }
             

                //the animator stuff makes the speaker "face" the player

                textBox.ReloadScript(theText, portrait);

                textBox.currentLine = startLine;
                
                textBox.endAtLine = endLine;

                textBox.EnableTextBox();

                if (destroyWhenFinished)
                {
                    Destroy(gameObject);
                }

            }

        }

        if (textBox.currentLine > endLine && !requireButtonPress) //for detecting when a shout has finished //REFACTORING: Could also use textbox.isactive??
        {
            PlayerController.isTalking = false; //in case the triggerExit doesn't run because a shout is destroyed

          
            if(!notAttachedToObject)
            {
                thePerson = GetComponentInParent<FleeingEnemyController>();
                thePerson.canMove = true;
            }
        

            if (destroyWhenFinished)
            {
                Destroy(gameObject);
            }

        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !PlayerController.isRunning && !PlayerController.isTalking)
        { 

            if(requireButtonPress == false && !notAttachedToObject) //if its a shout, then shout object is a child
            {
                
                GetComponentInParent<Animator>().SetFloat("LastMoveX", thePlayer.transform.position.x - transform.position.x);
                GetComponentInParent<Animator>().SetFloat("LastMoveY", thePlayer.transform.position.y - transform.position.y);
                //making them face player
                GetComponentInParent<FleeingEnemyController>().canMove = false;
                PlayerController.isTalking = true;
            }

        

            if (requireButtonPress == true)
            {
                waitForPress = true;
                return;
            }
            
            textBox.ReloadScript(theText, portrait);
            textBox.currentLine = startLine;

          

            textBox.endAtLine = endLine;
            
            textBox.EnableTextBox();

           
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            waitForPress = false;

            if(!notAttachedToObject)
            {
                thePerson.canMove = true;
            }
            
            if(textBox.currentLine > endLine)
            {
                PlayerController.isTalking = false;
            }
            
        }
    }
}
