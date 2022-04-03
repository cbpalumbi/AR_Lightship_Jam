using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
using Niantic.ARDK.AR;
using Niantic.ARDK.AR.ARSessionEventArgs;
using Niantic.ARDK.AR.Configuration;
 
using Unity.Collections;
using Niantic.ARDK.AR.Mesh;
using Niantic.ARDK.Utilities;

using UnityEngine.iOS;
 
public class PickUpThrow : MonoBehaviour
{
    private GameObject heldObject = null;
    public GameObject player;
    public GameObject hands;
    public float vertForce = 20f;
    public float horForce = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
 
    // Update is called once per frame
    void Update()
    {
        //if (PlatformAgnosticInput.touchCount <= 0) { return; }
 
        //if the user touches the screen then pick up nearest object in range
        //var touch = PlatformAgnosticInput.GetTouch(0);
        Debug.Log(Input.touchCount);
        if (Input.touchCount > 0)
        {
            Debug.Log("Touch Detected");
            if (heldObject == null) {
                GetClosestObject();
            }
            else {
                Debug.Log("Drop");
                ThrowHeldObject();
            }
        }
    }
   
    private bool ValidTag(string tag) {

        switch(tag) {
            case "pea" :
                return true; 
            case "cheese" :
                return true;
            case "flour" :
                return true;
            case "rice" :
                return true;
            case "seaweed" :
                return true;
            case "fish" :
                return true;
            case "potato" :
                return true;
            default: 
                return false; 

        }
        return false;
        
    }

    private void GetClosestObject() {
        Vector3 center = player.transform.position;
        float radius = 1.5f;
        Collider[] colliders = Physics.OverlapSphere(center, radius);
        Collider nearestCollider = null;
        float minSqrDistance = Mathf.Infinity;

        // for (int i = 0; i < colliders.Length; i++) {
        //     if (!ValidTag(colliders[i].gameObject.tag)) {
        //         colliders = colliders.RemoveAt(i);
        //         //i++;
        //     }
        // }

        for (int i = 0; i < colliders.Length; i++) {  
            float sqrDistanceToCenter = (center - colliders[i].transform.position).sqrMagnitude;
            if (sqrDistanceToCenter < minSqrDistance)
            {
                minSqrDistance = sqrDistanceToCenter;
                nearestCollider = colliders[i];
            }
        }
        if (nearestCollider != null) {
            Debug.Log("Pick Up " + nearestCollider.gameObject);
            StartCoroutine(PickUp(nearestCollider.gameObject));
        }
    }

    IEnumerator PickUp(GameObject obj) {
        heldObject = obj;

        if (ValidTag(heldObject.tag)) {
        
            obj.GetComponent<Collider>().enabled = false;
            obj.GetComponent<Rigidbody>().isKinematic = true;
            obj.transform.position = hands.transform.position;
            obj.transform.rotation = hands.transform.rotation;
            obj.transform.parent = hands.transform;
            yield return new WaitForSeconds(2);
        }
        
    }

    private void ThrowHeldObject() {
        heldObject.transform.parent = null;
        heldObject.GetComponent<Collider>().enabled = true;
        heldObject.GetComponent<Rigidbody>().isKinematic = false;
        heldObject.GetComponent<Rigidbody>().AddForce(heldObject.transform.forward * horForce);
        heldObject = null;
    }
}