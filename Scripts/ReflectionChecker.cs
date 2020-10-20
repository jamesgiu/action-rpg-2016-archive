using UnityEngine;
using System.Collections;

public class ReflectionChecker : MonoBehaviour {

 
	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

       

	}

    void OnTriggerEnter2D(Collider2D other)
    {
        //check if chen or player as well, it activated on williams' line of sight
        //use 'other' to activate specific reflection

        ReflectionController[] reflections = FindObjectsOfType<ReflectionController>();

        foreach(ReflectionController reflection in reflections)
        {
            if(reflection.GetComponentInParent<Rigidbody2D>() == other.GetComponent<Rigidbody2D>() )
            {
                SpriteRenderer spriteRenderer = reflection.GetComponent<SpriteRenderer>();
                spriteRenderer.color = new Color(255f, 255f, 255f, 0.43f);
            }

        }
        //need to use other for context else everythin become reflect

    }

    void OnTriggerExit2D(Collider2D other)
    {

        ReflectionController[] reflections = FindObjectsOfType<ReflectionController>();

        foreach (ReflectionController reflection in reflections)
        {

            if (reflection.GetComponentInParent<Rigidbody2D>() == other.GetComponent<Rigidbody2D>())
            {
                SpriteRenderer spriteRenderer = reflection.GetComponent<SpriteRenderer>();
                spriteRenderer.color = new Color();
            }

      

        }

    }
}
