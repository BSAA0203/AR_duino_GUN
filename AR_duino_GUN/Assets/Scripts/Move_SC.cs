using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move_SC : MonoBehaviour {

	// Use this for initialization
	
    public void Search()
    {
        SceneManager.LoadScene("Search"); // go to Search Scene
    }

    public void Game()
    {
        SceneManager.LoadScene("Game"); //go to Game Scene
    }
}
