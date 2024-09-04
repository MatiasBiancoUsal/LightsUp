using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour

{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    
        public void Options() //este sirve para la siguiente escena en caso de que quieran saltear la into
        {

            pauseMenuUI.SetActive(true);

        }

    public void Back() //este sirve para la siguiente escena en caso de que quieran saltear la into
    {

        pauseMenuUI.SetActive(false);

    }


}