using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour {

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void loadNextLevel(int vorherigeScence)
    {
        if (vorherigeScence != 2)
            SceneManager.LoadScene(vorherigeScence + 1);
        else
            SceneManager.LoadScene(0);
    }
}
