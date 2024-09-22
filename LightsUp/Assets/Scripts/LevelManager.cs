using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public string levelToLoad;
    public string nextSceneName;
    public string previousSceneName;

    public float waitToRespawn;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SceneManager.LoadScene(nextSceneName);
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene(previousSceneName);
        }
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCo());
    }

    IEnumerator RespawnCo()
    {
        PlayerMov.instance.gameObject.SetActive(false);
        
        yield return new WaitForSeconds(waitToRespawn - (1f / UIController.instance.fadeSpeed));

        UIController.instance.FadeToBlack();

        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) + .2f);

        UIController.instance.FadeFromBlack();

        PlayerMov.instance.gameObject.SetActive(true);

        PlayerMov.instance.transform.position = CheckpointController.instance.spawnPoint;
    }

    public void EndLevel()
    {
        StartCoroutine(EndLevelCo());
    }

    public IEnumerator EndLevelCo()
    {
        PlayerMov.instance.stopInput = true;

        yield return new WaitForSeconds(1f);

        UIController.instance.FadeToBlack();

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(levelToLoad);
    }
}
