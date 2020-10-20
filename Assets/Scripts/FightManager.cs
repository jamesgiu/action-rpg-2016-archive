using UnityEngine;
using UnityEngine.SceneManagement;

public class FightManager : MonoBehaviour {

    private new CircleCollider2D collider;
    private PlayerController thePlayer;
    private EnemyController[] enemiesList;
    private EnemyController theEnemy;
    private string sceneIstartedIn;
    
	// Use this for initialization
	void Start () {

        collider = GetComponent<CircleCollider2D>();
        thePlayer = FindObjectOfType<PlayerController>();
        enemiesList = FindObjectsOfType<EnemyController>();
        sceneIstartedIn = SceneManager.GetActiveScene().name;
        
      

        Physics2D.IgnoreCollision(collider, thePlayer.GetComponent<BoxCollider2D>());
        
    }
	
	// Update is called once per frame
	void Update () {

        if(sceneIstartedIn != SceneManager.GetActiveScene().name)
        {
            Start(); 
            //the fight area never gets destroyed on scene change as it is child of camera
            //therefore start() never reruns and the level is never rescanned for enemies
            //therefore we must scan again to find new enemies if scene has changed
        }

        if (PlayerController.isFighting == true)
        {
            collider.enabled = true;

            foreach (EnemyController enemy in enemiesList)
            {
                if (enemy.isFighting == true)
                {
                    theEnemy = enemy;
                    Physics2D.IgnoreCollision(collider, theEnemy.GetComponent<BoxCollider2D>());
                }
            }

        }
        else
        {
            collider.enabled = false;
        }
        //got to set isFighting to false as well in enemycontroller
        //enemy not immune to collider if player switches scene and comes back before starting any fight
        //for some reason..

        

	}
}