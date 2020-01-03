using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    public string DefaltTargScene = "";
	

    //to load defalt scene
    public void ToChangeDefalt ( ) {

        SceneManager.LoadScene(DefaltTargScene);

    }

    //to load scene by name
    public void ToChange (string TargScene) {
		
        SceneManager.LoadScene(TargScene);

	}

    //Exit game
    public void ExitGame()
    {
        print("Quit!");
        Application.Quit();
    }
}
