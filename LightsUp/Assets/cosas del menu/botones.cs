using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class botones : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI; 


    public void comenzar() // boton del menu "intro" deberia ser el nombre de la escena que tenga la animacion de introduccion
    {

        SceneManager.LoadScene("intro");
    
    }


    public void jugar() //este sirve para la siguiente escena en caso de que quieran saltear la into
    {

        SceneManager.LoadScene("nivel 1");

    }


    public void salir() //logicamente, boton de salir

    {

        Application.Quit();

    }



    public void Options() //este sirve para la siguiente escena en caso de que quieran saltear la into
    {

        pauseMenuUI.SetActive(true);

    }

    public void volver() //este sirve para la siguiente escena en caso de que quieran saltear la into
    {

        pauseMenuUI.SetActive(false);

    }


}
