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
        Dictionary<string, object> eventData = new Dictionary<string, object>
        {
            { "levelName", SceneManager.GetActiveScene().name },
            { "completionTime", Time.timeSinceLevelLoad }
        };

        AnalyticsService.Instance.CustomData("level_complete", eventData);
        Debug.Log("Evento level_complete enviado a Unity Analytics");
    }
}
