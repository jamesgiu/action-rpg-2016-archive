using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player_Start_Point : MonoBehaviour {

    private PlayerController thePlayer;
    private CameraController theCamera;

    public string desiredCameFromSceneString;
    private static string currentSceneString; //every start point needs to inherit this value



    // Use this for initialization
    void Start () {
        
        
        thePlayer = FindObjectOfType<PlayerController>(); //finding an object
        theCamera = FindObjectOfType<CameraController>();

        //desired name of last scene to activate certain start point

        currentSceneString = LastSceneGrabber.lastSceneName; //this should only happen once per scene, despite number of startPoints


        if (currentSceneString == desiredCameFromSceneString) //goes through each of the start points, find which one is the context correct one
        {
            thePlayer.transform.position = transform.position; //equalling the position of the object this script is based on
            //the program thinks I am always in MAIN
            theCamera.transform.position = new Vector3(transform.position.x, transform.position.y, theCamera.transform.position.z);
        }


      
     
 
    
                
     
      
       

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
