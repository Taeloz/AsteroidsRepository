using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotController : MonoBehaviour
{
    public float speed;
    public float maxDistance; // Maximum distance the shot will travel

    private Vector3 startPosition;

	
	void Start ()
    {
        // Place the shot at the nose of the ship and give it a velocity
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        startPosition = GetComponent<Rigidbody>().transform.position;
	}

	void Update ()
    {
        // Destroy the shot if it reaches the max distance
        if (Vector3.Distance(GetComponent<Rigidbody>().transform.position, startPosition) >= maxDistance)
        {
            Destroy(gameObject);
        }

	}

    // Reset the distance the shot has left to travel if the shot warped across the play area
    public void resetDistance(Vector3 oldPosition)
    {
        maxDistance = maxDistance - Vector3.Distance(oldPosition, startPosition);
        startPosition = GetComponent<Rigidbody>().transform.position;
    }
}
