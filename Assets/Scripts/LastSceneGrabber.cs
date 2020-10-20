using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LastSceneGrabber : MonoBehaviour {

    public static string lastSceneName;

	// Use this for initialization
	void Start () {


       lastSceneName = SceneManager.GetActiveScene().name;


    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
