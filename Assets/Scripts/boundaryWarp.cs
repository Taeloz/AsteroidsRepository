using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Function to warp the player when hitting the edges of the play area
public class boundaryWarp : MonoBehaviour
{

    private float limit = 17; // Placeholder to make sure the player is not infinitely bouncing between triggers
    private Vector3 oldPosition; // Keeps track of the position before warping so the shots will still travel their max distance after warping

    private void OnTriggerEnter(Collider other)
    {
        oldPosition = other.transform.position; // Get the position before modifying it

        // Warp the object based on which side of the play area was hit
        if (this.name == "rightGrid")
        {
            other.gameObject.transform.position = new Vector3(-limit, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
        }
        if (this.name == "leftGrid")
        {
            other.gameObject.transform.position = new Vector3(limit, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
        }
        if (this.name == "topGrid")
        {
            other.gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, -limit, other.gameObject.transform.position.z);
        }
        if (this.name == "bottomGrid")
        {
            other.gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, limit, other.gameObject.transform.position.z);
        }
        if (this.name == "frontGrid")
        {
            other.gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, limit);
        }
        if (this.name == "backGrid")
        {
            other.gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, -limit);
        }

        // If a shot was warped, make sure its distance left to travel is unmodified
        if (other.tag == "shot")
        {
            other.GetComponent<shotController>().resetDistance(oldPosition);
        }
    }
}