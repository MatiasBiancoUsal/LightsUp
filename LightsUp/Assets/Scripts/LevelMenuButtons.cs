using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenuButtons : MonoBehaviour
{
    public Button[] buttons;

    //Niveles Bosque
    public void Bosque1() 
    {

        SceneManager.LoadScene("Bosque 1");

    }
    public void Bosque2()
    {

        SceneManager.LoadScene("Bosque 2");

    }
    public void Bosque3()
    {

        SceneManager.LoadScene("Bosque 3");

    }
    public void Bosque4()
    {

        SceneManager.LoadScene("Bosque 4");

    }
    public void Boss1()
    {

        //SceneManager.LoadScene("");

    }

    //Niveles Ciudad
    public void Ciudad1()
    {

        SceneManager.LoadScene("Ciudad 1");

    }
    public void Ciudad2()
    {

        SceneManager.LoadScene("Ciudad 2");

    }

    public void Ciudad3()
    {

        SceneManager.LoadScene("Ciudad 3");

    }
    public void Ciudad4()
    {

        SceneManager.LoadScene("Ciudad 4");

    }

    public void Boss2()
    {

        SceneManager.LoadScene("BossCodicia");

    }

    //Niveles Caverna
    public void Caverna1()
    {

        SceneManager.LoadScene("Caverna 1");

    }
    public void Caverna2()
    {

        SceneManager.LoadScene("Caverna 2");

    }

    public void Caverna3()
    {

        SceneManager.LoadScene("Caverna 3.2");

    }
    public void Caverna4()
    {

        SceneManager.LoadScene("Caverna 4");

    }

    public void Boss3()
    {

        SceneManager.LoadScene("BossGula");

    }

    //Niveles Torre
    public void Torre1()
    {

        SceneManager.LoadScene("Torre 1");

    }
    public void Torre2()
    {

        SceneManager.LoadScene("Torre 2");

    }

    public void Torre3()
    {

        SceneManager.LoadScene("Torre 3");

    }
    public void Torre4()
    {

        SceneManager.LoadScene("Torre 4");

    }

    public void Boss4()
    {

        //SceneManager.LoadScene("");

    }
}
