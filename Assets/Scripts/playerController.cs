using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    public float speed;
    public float fireRate;
    public GameObject shot;
    public Transform shotSpawn;

    private float nextFire;

    private void Update()
    {
        // Let the player fire at a fixed fire rate
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            // Randomize the pitch
            float randomPitch = Random.Range(0.3f, 1.0f);

            Instantiate(shot, shotSpawn.position, shotSpawn.rotation).GetComponent<AudioSource>().pitch = randomPitch;
        }
    }

    void FixedUpdate ()
    {
        // Grab input to use for movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float thrust = Input.GetAxis("Thrust");

        // Play engine animation when thrust is applied
        if (thrust > 0)
        {
            GetComponent<ParticleSystem>().Play();
        } else
        {
            GetComponent<ParticleSystem>().Stop();
        }

        // Rotate ship according to input.
        GetComponent<Rigidbody>().transform.Rotate(0.0f, -moveVertical * speed, 0.0f, Space.Self);
        GetComponent<Rigidbody>().transform.Rotate(0.0f, moveHorizontal * speed, 0.0f, Space.World);
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * thrust * speed * 0.5f);
    }
}
