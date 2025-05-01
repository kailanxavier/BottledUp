using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateEndTime : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI time;

    void Awake()
    {
        string endTime = StaticData.valueToKeep;
        time.text = endTime;
    }
}