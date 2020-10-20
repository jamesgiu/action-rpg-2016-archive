using UnityEngine;
using System.Collections;

public class EnemyFieldOfView : MonoBehaviour
{

    private EnemyController attachedEnemy;
    private PlayerController thePlayer;
    public bool playerSpotted;
    



    public TextAsset theText;

    public int startLine;
    public int endLine;


    private FleeingEnemyController thePerson;
  



    public Sprite portrait;

    public TextBoxManager textBox;

    public bool destroyWhenFinished;

    public bool requireButtonPress;
    private bool waitForPress;


    // Use this for initialization
    void Start()
    {
        playerSpotted = false;
        attachedEnemy = GetComponentInParent<EnemyController>();
      
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = attachedEnemy.transform.position; //keep collider following enemy

        if (playerSpotted && textBox.currentLine > endLine)
        {
            attachedEnemy.runningToPlayer = true;
            //once dialogue is over, begin chasing the player
            PlayerController.isTalking = false;

            if (destroyWhenFinished)
            {
                textBox.DisableTextBox();
                playerSpotted = false;
              
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D other) //field of view
    {
        if (!attachedEnemy.isDefeated)
        {
            if (other.gameObject.tag == "Player" && !attachedEnemy.runningToPlayer && !attachedEnemy.isFighting)
            {
                playerSpotted = true;

                //legs face the player
                attachedEnemy.GetComponentsInChildren<Animator>()[1].SetFloat("LastMoveX", other.transform.position.x - transform.position.x);
                attachedEnemy.GetComponentsInChildren<Animator>()[1].SetFloat("LastMoveY", other.transform.position.y - transform.position.y);
                //torso face the player
                attachedEnemy.GetComponent<Animator>().SetFloat("LastMoveX", other.transform.position.x - transform.position.x);
                attachedEnemy.GetComponent<Animator>().SetFloat("LastMoveY", other.transform.position.y - transform.position.y);
                //face the player

                PlayerController.isTalking = true;

                textBox.ReloadScript(theText, portrait);

                textBox.currentLine = startLine;
                textBox.endAtLine = endLine;

                textBox.EnableTextBox();

              



                attachedEnemy.BGMusic.clip = attachedEnemy.myMusic;
                attachedEnemy.BGMusic.Play();

                




                //death script below
                //other.gameObject.SetActive(false);
                //reloading = true;


            }

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //playerSpotted = false;
    }
}