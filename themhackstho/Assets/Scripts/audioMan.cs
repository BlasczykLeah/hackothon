using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioMan : MonoBehaviour
{
    public static audioMan instance;
    public AudioSource button, win, lose, end, touch, clank, yes;

    public void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void Press()
    {
        button.Play();
    }
    public void Victory()
    {
        win.Play();
    }
    public void Defeat()
    {
        lose.Play();
    }
    public void Over()
    {
        end.Play();
    }
    public void hold()
    {
        touch.Play();
    }
    public void tie()
    {
        clank.Play();
    }
    public void draw()
    {
        yes.Play();
    }
}
