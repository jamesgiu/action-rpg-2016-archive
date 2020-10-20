using UnityEngine;
using System.Collections;

public class HurtEnemy : MonoBehaviour {

    private EnemyController theEnemy;
    private Animator playerAnimator;
    private PlayerController thePlayer;
    private float timeUntilNextKnockback;
    private float timeUntilNextKnockbackCounter;
    private bool canBeKnockedBack;
    private Vector3 knockBackPos = new Vector3();

    public GameObject damageBurst;

    public int damageToGive;
    public Transform hitPoint;

    // Use this for initialization
    void Start () {

        playerAnimator = GetComponentInParent<Animator>();
        thePlayer = GetComponentInParent<PlayerController>();
        timeUntilNextKnockback = 1f;
        canBeKnockedBack = true;
        timeUntilNextKnockbackCounter = timeUntilNextKnockback;
    }
	
	// Update is called once per frame
	void Update () {

     
        //put LERP stuff in here to make it smood
        //if lastmovex, then dont move y and if lastmovey then dont move x

        if(!canBeKnockedBack)
        {
            timeUntilNextKnockbackCounter -= Time.deltaTime;

         

            if (timeUntilNextKnockbackCounter <= 0f)
            {
                canBeKnockedBack = true;
                timeUntilNextKnockbackCounter = timeUntilNextKnockback;
            }
        }

    }

    void OnTriggerEnter2D (Collider2D other)
    {

        if (other.gameObject.tag == "Enemy" && playerAnimator.GetBool("Attack")) //since name could be something stupid like williams (2) so tags are better
        {
            //CAN SWING THROUGH WALL
            //IDEA OF CHEN: IF RULER COLLIDE WITH COLLISION BOX FOR WALL, SEND PLAYER BACK!!! ARGHH SO GOOD
           
            theEnemy = other.gameObject.GetComponent<EnemyController>();

            EnemyHealthManager enemyHealthManager = other.gameObject.GetComponent<EnemyHealthManager>();
            enemyHealthManager.HurtEnemy(damageToGive);

            Instantiate(damageBurst, hitPoint.position, hitPoint.rotation);
            theEnemy.isRecoiling = true;

        }

        if (other.gameObject.tag == "Wall" && playerAnimator.GetBool("Attack") && canBeKnockedBack)
        {
           thePlayer.isRecoiling = true;
           canBeKnockedBack = false;
        }

    }
}

