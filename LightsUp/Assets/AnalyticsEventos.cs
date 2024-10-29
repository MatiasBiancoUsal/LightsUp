using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;
using UnityEngine.SceneManagement;


public class AnalyticsEventos : MonoBehaviour
{




    public void EventoLevelStart()
    {


        CustomEvent nombreVariable = new CustomEvent("level_start")
        {
            { "level_index", SceneManager.GetActiveScene().buildIndex }
        };

        AnalyticsService.Instance.RecordEvent(nombreVariable);



    }





    // Start is called before the first frame update
    void Start()
    {
        EventoLevelStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
