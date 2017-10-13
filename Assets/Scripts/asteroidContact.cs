using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidContact : MonoBehaviour {

    public GameObject explosion;
    public AudioClip asteroidDestroyed;
    public AudioClip asteroidCollision;
    public AudioClip asteroidPlayerCollision;

    private gameController GameController;

    private void Start()
    {
        // Find the gamecontroller to be able to call it's addScore script
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        GameController = gameControllerObject.GetComponent<gameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only track collisions between objects that can collide
        if (other.tag == "shot" || other.tag == "asteroid" || other.tag == "Player")
        {
            // Create the explosion
            GameObject instance = Instantiate(explosion, this.transform.position, this.transform.rotation);

            // Send a different audio clip to the instantiated explosion depending on what object was collided with
            if (other.tag == "shot")
            {
                instance.SendMessage("Awakening", asteroidDestroyed);

                // Add to the score if the player destroyed the asteroid with a shot
                GameController.addScore(10);
            }

            if (other.tag == "asteroid")
            {
                instance.SendMessage("Awakening", asteroidCollision);
            }

            if (other.tag == "Player")
            {
                instance.GetComponent<AudioSource>().spatialBlend = 0;
                instance.SendMessage("Awakening", asteroidPlayerCollision);

                GameController.gameOver();

                // If collision is with the player, change the audio listener to the main camera so
                // the collision sound can actually be heard
                FindObjectOfType<Camera>().GetComponent<AudioListener>().enabled = true;
            }

            // Destroy the game objects
            Destroy(other.gameObject);
            Destroy(instance, 3); // Destroy the explosion after 3 seconds to give it time to play
            Destroy(this.gameObject);
        }
    }
}
