using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Load_New_Area : MonoBehaviour {

    public string levelToLoad;


	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag.Equals("Player") && !PlayerController.isFighting && !PlayerController.isRunning && !PlayerController.isTalking) //or whoever player is at time, maybe use variable
        {
            SceneManager.LoadScene(levelToLoad);
        }
    }
}
