using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tit : MonoBehaviour
{
    public GameObject howToPlay;

    void Start()
    {
        howToPlay.SetActive(false);
    }

    public void openTit()
    {
        howToPlay.SetActive(true);
    }
}
