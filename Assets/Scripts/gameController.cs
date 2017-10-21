using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour {

    public Text scoreText;
    public Text gameOverText;
    public GameObject asteroid;

    public float asteroidSpawnRadius;
    public float asteroidMinSpeed;
    public float asteroidMaxSpeed;
    public float asteroidTumble;
    [Tooltip("Time before starting a wave, in seconds")] // Experimenting with tooltips
    public float startWait;
    public float asteroidWait;
    public float waveWait;
    public float waveCount;

    private int score;
    private bool gameOverFlag;

	// Use this for initialization
	void Start ()
    {
        // Start the asteroid spawn coroutine
        StartCoroutine("spawnAsteroids");

        // Initialize the score
        score = 0;
        updateScore();

        gameOverFlag = false;
        gameOverText.enabled = false;
	}

    private void Update()
    {
        if (gameOverFlag)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    // Add to the score
    public void addScore(int value)
    {
        score += value;
        updateScore();        
    }

    // Update the displayed score
    private void updateScore()
    {
        scoreText.text = "SCORE: " + score;
    }

    // Game over
    public void gameOver()
    {
        gameOverFlag = true;
        gameOverText.enabled = true;
        StopCoroutine("spawnAsteroids");
        StartCoroutine("gameOverTextFlash");
    }

    // Asteroid spawning coroutine
    IEnumerator spawnAsteroids()
    {
        while (true)
        {
            yield return new WaitForSeconds(startWait);
            for (int i = 0; i < waveCount; i++)
            {
                // Random initial asteroid speed
                float asteroidSpeed = Random.Range(asteroidMinSpeed, asteroidMaxSpeed);

                // Random initial spawn position within sphere of radius "asteroidSpawnRadius"
                Vector3 spawnPosition = Random.onUnitSphere * asteroidSpawnRadius;

                // Initial rotation using identity quaternion
                Quaternion spawnRotation = Quaternion.identity;
          
                // Instantiate a new asteroid
                GameObject newAsteroid = Instantiate(asteroid, spawnPosition, spawnRotation);

                // Give asteroid random initial rotation and velocity vectors
                newAsteroid.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * asteroidTumble;
                newAsteroid.GetComponent<Rigidbody>().velocity = Random.insideUnitSphere * asteroidSpeed;

                yield return new WaitForSeconds(asteroidWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }

    // Flash the game over text
    IEnumerator gameOverTextFlash()
    {
        while (true)
        {
            if (gameOverText.enabled)
            {
                gameOverText.enabled = false;
            }
            else
            {
                gameOverText.enabled = true;
            }

            yield return new WaitForSeconds(1);
        }
    }
}
