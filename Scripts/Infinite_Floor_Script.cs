using UnityEngine;
using System.Collections;

public class Infinite_Floor_Script : MonoBehaviour
{

    public GameObject floor;


  

    private Vector3 floorPosition;
    private Vector3 newFloorPosition;

    private PlayerController thePlayer;
    private Vector3 thePlayerLocation;

    // Use this for initialization
    void Start()
    {

   
        thePlayer = FindObjectOfType<PlayerController>();
        floorPosition = new Vector3(floor.transform.position.x, floor.transform.position.y, floor.transform.position.z);


    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other) //physical box
    {
        if (other.gameObject.tag == "Player")
        {
            if(thePlayer.lastMove.x > 0)
            {
                floor.transform.position = new Vector3((floor.transform.position.x + 30f), -0.5f);
            }

            if(thePlayer.lastMove.x < 0)
            {
                floor.transform.position = new Vector3((floor.transform.position.x - 30f), -0.5f);
            }
        }
    }
}