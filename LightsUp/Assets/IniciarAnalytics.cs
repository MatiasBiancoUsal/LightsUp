using System;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;

public class IniciarAnalytics : MonoBehaviour
{
    async void Awake()
    {
        try
        {
            await UnityServices.InitializeAsync(); // Esto inicia los servicios de Unity


            // Con esto inicializamos la recolección de datos:
            AnalyticsService.Instance.StartDataCollection();

        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

}
