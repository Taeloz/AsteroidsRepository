using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {

	// Update is called once per frame
	void Update ()
    {
        // Rotate the camera around the world
        float moveHorizontal = Input.GetAxis("Camera");
        this.transform.RotateAround(Vector3.zero, Vector3.up, moveHorizontal * 5);
	}
}
