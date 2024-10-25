using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject Controlesmenu;
    public GameObject Levelmenu;


    public void StartIntro() // boton del menu "intro" deberia ser el nombre de la escena que tenga la animacion de introduccion
    {

        SceneManager.LoadScene("Intro");
    
    }


    public void StartGame() //este sirve para la siguiente escena en caso de que quieran saltear la into
    {

        SceneManager.LoadScene("Bosque 1");

    }


    public void QuitGame() //logicamente, boton de salir

    {

        Application.Quit();

    }



    public void Options() //este sirve para abrir el panel de  options
    {

        pauseMenuUI.SetActive(true);

    }

    public void Back() //este sirve para la siguiente voler
    {

        pauseMenuUI.SetActive(false);

    }



    public void controles() //este sirve para abrir el panel de  controles
    {

        Controlesmenu.SetActive(true);

    }

    public void vovler() //este sirve para la siguiente voler
    {

        Controlesmenu.SetActive(false);

    }

    public void LevelMenuActive()
    {

        Levelmenu.SetActive(true);

    }

    public void LevelMenuDeactive()
    {

        Levelmenu.SetActive(false);

    }

}
