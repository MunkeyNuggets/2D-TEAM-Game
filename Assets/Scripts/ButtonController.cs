using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {

	public void quitButton()
    {
        Application.Quit();
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void startButton()
    {
        SceneManager.LoadScene(1);
    }

    public void creditsButton()
    {
        SceneManager.LoadScene(2);
    }
}
