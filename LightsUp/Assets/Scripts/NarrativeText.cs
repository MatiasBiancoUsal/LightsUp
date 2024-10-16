using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NarrativeText : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public List<string> TextLines;
    public float Duration;
    public float Speed;

    private int Index = 0;
    void Start()
    {
        StartCoroutine(Narrative());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            NextScene();
        }
    }

    IEnumerator Narrative()
    {
        while (Index < TextLines.Count)
        {
            yield return StartCoroutine(Typing(TextLines[Index]));
            Index++;
            yield return new WaitForSeconds(Duration);
        }

        Text.text = "";
        yield return null;
    }

    IEnumerator Typing(string line)
    {
        Text.text = "";
        foreach (char letter in line.ToCharArray())
        {
            Text.text += letter;
            yield return new WaitForSeconds(Speed);
        }
    }
    void NextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
