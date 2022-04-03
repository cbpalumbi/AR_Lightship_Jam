using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class grabObject : MonoBehaviour
{
    public Material highlightedMat;
    private  Material old;
    public GameObject closestObj;
    private bool isHolding = false;
    //bool canpickup; //a bool to see if you can or cant pick up the item
    //GameObject ObjectIwantToPickUp; // the gameobject onwhich you collided with
    //public GameObject myHands; 
    // Start is called before the first frame update
    void Start()
    {
        
        //canpickup = false; 
    }

    void OnTriggerEnter(Collider other)
    {
        old = other.gameObject.GetComponent<MeshRenderer>().material; //keeping track of original color
        Debug.Log(other.gameObject.name);
        other.gameObject.GetComponent<MeshRenderer>().material = highlightedMat; //changing to new color

        if (closestObj) //finding out which object in proximity is closer to pick up
        {
            float dist = Vector3.Distance(other.transform.position, gameObject.transform.position); //gameobject references thing object on camera that is at same position as camera
            float dist2 = Vector3.Distance(closestObj.transform.position, gameObject.transform.position);
            print("Distance to other: " + dist);
            if(dist2<dist){
                //canpickup = true;  //set the pick up bool to true
                closestObj = other.gameObject;
                //pickObj();
                // closestObj.GetComponent<Rigidbody>().isKinematic = true;   //makes the rigidbody not be acted upon by forces
                // closestObj.transform.position = gameObject.transform.position; // sets the position of the object to your hand position
                // closestObj.transform.parent = gameObject.transform; //makes the object become a child of the parent so that it moves with the hands
                //closestObj = other.gameObject;
                //isHolding = true;
                

            }

            // else{
            //     //canpickup = true;  //set the pick up bool to true
            
                // other.GetComponent<Rigidbody>().isKinematic = true;   //makes the rigidbody not be acted upon by forces
                // other.transform.position = gameObject.transform.position; // sets the position of the object to your hand position
                // other.transform.parent = gameObject.transform; //makes the object become a child of the parent so that it moves with the hands

            // }
        }
        else{
            closestObj = other.gameObject;
            //pickObj();
        }
        
    }

    public void pickObj(){
        if(closestObj && !isHolding){
            closestObj.GetComponent<Rigidbody>().isKinematic = true;   //makes the rigidbody not be acted upon by forces
            closestObj.transform.position = gameObject.transform.position + new Vector3(0.5f,-0.11f,0.3f); // sets the position of the object to your hand position
            closestObj.transform.parent = gameObject.transform; //makes the object become a child of the parent so that it moves with the hands}
            isHolding = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        
        Debug.Log(other.gameObject.name);
        other.gameObject.GetComponent<MeshRenderer>().material = old; //changing back to original color
        //canpickup = false; 
    }

    // void OnCollisionEnter(Collision collision)
    // {
    //     ContactPoint contact = collision.contacts[0];
    //     Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
    //     Vector3 position = contact.point;
    //     Instantiate(explosionPrefab, position, rotation);
    //     Destroy(gameObject);
    // }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        { 
            Debug.Log("touching the screen");
            pickObj();
        }

    }
}
