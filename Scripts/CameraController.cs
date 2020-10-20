using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject followTarget;
    private Vector3 targetPos;
    public float moveSpeed;

    private static bool cameraExists;

	// Use this for initialization
	void Start () {


        if (!cameraExists)
        {
            cameraExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject); //destroy extra gameobject that is created on scene load
        }

        DontDestroyOnLoad(transform.gameObject);

    }
	
	// Update is called once per frame
	void Update () {

        targetPos = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
       
        //for LERP: first value = where we are, second value where we want to be, float how fast. like a tween in flash
        //time.deltaTime = consistency through framerates
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);

	}
}
