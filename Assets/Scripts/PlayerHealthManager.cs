using UnityEngine;
using System.Collections;

public class PlayerHealthManager : MonoBehaviour {

    public int playerMaxHealth;
    public int playerCurrentHealth;


	// Use this for initialization
	void Start () {

        playerCurrentHealth = playerMaxHealth;



	}
	
	// Update is called once per frame
	void Update () {
	
        if(playerCurrentHealth <= 0)
        {
            gameObject.SetActive(false); //kills the player
            PlayerController.isDead = true;
        }

	}

    public void HurtPlayer(int damageTaken)
    {
        //can be called by enemy OnCollisionEnter funcs to hurt player 
        playerCurrentHealth -= damageTaken;
    }

    public void SetMaxHealth()
    {
        playerCurrentHealth = playerMaxHealth;
    }
}
