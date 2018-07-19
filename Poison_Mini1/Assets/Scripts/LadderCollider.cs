using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderCollider : MonoBehaviour {

    private BoxCollider2D boxCollider;
        
	// Use this for initialization
	void Start () {
        boxCollider = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(boxCollider.isTrigger);
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "GroundCheck" && Input.GetKey("down"))
        {
            boxCollider.isTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "GroundCheck" &&Input.GetKey("up"))
        {
            boxCollider.isTrigger = false;
        }
    }

}
