using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{

  
    public int currentLevel = 1;
    public TMP_Text levelText;   

    void Start()
    {
       
        UpdateLevelText();
    }

   
    void OnValidate()
    {
        UpdateLevelText(); 
    }

   
    void UpdateLevelText()
    {
        levelText.text = "Nivel: " + currentLevel.ToString();
    }
}