using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SceneChanger : MonoBehaviour
{

    public string sceneName;
    
    public void PlayTheGame()
    {
        SceneManager.LoadScene("Level_0_Intro");
    }

   
    // Coordinates where the player will respawn
    public Vector3 respawnPosition;

    // Function to retry the level
    public void RetryLevel()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // Set the player's position to the specified respawn position
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = respawnPosition;
        }
        else
        {
            Debug.LogWarning("Player GameObject not found in the scene.");
        }
    }

public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitTheGame()
    {
        Debug.Log("QUIT THE GAME");
        Application.Quit();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider belongs to the player (you can tag the player as "Player")
        if (other.gameObject.tag == "Player")
        {
            // Load the specified scene
            SceneManager.LoadScene(sceneName);
        }
    }
}

