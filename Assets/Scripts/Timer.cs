using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;

public class Timer : MonoBehaviour
{
    public GameObject EndGame;
    public GameObject InGame;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _endTime;

    public bool gameEnded;
    float elapsedTime;

    void Update()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        _timerText.text = string.Format("{00:00}:{01:00}", minutes, seconds);

        if (gameEnded)
        {
            PauseGame();
            _endTime.enabled = true;
            _endTime.text = string.Format("{00:00}:{01:00}", minutes, seconds);

            EndGame.SetActive(true);
            InGame.SetActive(false);
        }
    }

    //Stop Timer
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    //Resume Timer
    public void UnPauseGame()
    {
        Time.timeScale = 1;

    }
}

public class StaticData : MonoBehaviour
{
    public static string valueToKeep;
}
