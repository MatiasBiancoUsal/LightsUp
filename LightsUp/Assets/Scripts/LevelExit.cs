using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Services.Analytics;
using Unity.Services.Core;

public class LevelExit : MonoBehaviour
{
    private async void Start()
    {
        await UnityServices.InitializeAsync();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            SendLevelCompleteEvent();

            LevelManager.instance.EndLevel();
        }
    }

    private void SendLevelCompleteEvent()
    {
        var levelCompleteEvent = new CustomEvent("level_complete")
        {
            { "levelName", SceneManager.GetActiveScene().name },
            { "completionTime", Time.timeSinceLevelLoad }
        };

        AnalyticsService.Instance.RecordEvent(levelCompleteEvent);

        Debug.Log("Event 'level_complete' sent to Unity Analytics");
    }
}
