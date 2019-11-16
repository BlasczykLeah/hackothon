using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tit : MonoBehaviour
{
    public GameObject howToPlay;
    audioMan AM;

    void Start()
    {
        howToPlay.SetActive(false);
        AM = audioMan.instance;
    }

    public void openTit()
    {
        AM.Press();
        howToPlay.SetActive(true);
    }
}
