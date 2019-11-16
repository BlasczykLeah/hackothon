using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonStuff : MonoBehaviour
{
    audioMan AM;

    public void Start()
    {
        AM = audioMan.instance;
    }

    public void MainMenu()
    {
        AM.Press();
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        AM.Press();
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
