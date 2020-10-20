using UnityEngine;
using System.Collections;

public class EnemyHealthManager : MonoBehaviour {

    public int MaxHealth;
    public int CurrentHealth;
    private EnemyController theEnemy;

    // Use this for initialization
    void Start()
    {

       CurrentHealth = MaxHealth;
       theEnemy = GetComponent<EnemyController>();


    }

    // Update is called once per frame
    void Update()
    {

        if (CurrentHealth <= 0)
        {

            theEnemy.runningToPlayer = false;
            theEnemy.isFighting = false;
            theEnemy.isDefeated = true;


            theEnemy.gameObject.GetComponent<SpriteRenderer>().color = new Color32(233, 27, 27, 255);

            Debug.Log("I have finished fighting the player");

            PlayerController.isFighting = false; //this only works if all battles are 1v1s

        }

    }

    public void HurtEnemy(int damageTaken)
    {
        //can be called by enemy OnCollisionEnter funcs to hurt player 
        CurrentHealth -= damageTaken;
    }

    public void SetMaxHealth()
    {
        CurrentHealth = MaxHealth;
    }
}
