using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private bool timerIsRunning = false;
    private float currentTime = 0;
    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
    }
    void Update()
    {
        if (timerIsRunning)
        {
            DisplayTime();
        }
    }
    void DisplayTime()
    {
        currentTime += Time.deltaTime;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        textMesh.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
    }
}
