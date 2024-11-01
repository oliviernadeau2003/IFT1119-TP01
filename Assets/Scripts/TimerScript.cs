using TMPro;
using System;
using UnityEngine;

public class TimerScript : MonoBehaviour
{

    float chrono = 0f;

    public TMP_Text timerText;

    void Update()
    {
        chrono += Time.deltaTime;

        int minutes = Mathf.FloorToInt(chrono / 60F);
        int seconds = Mathf.FloorToInt(chrono - minutes * 60);

        string timer = string.Format("{0:0}:{1:00}", minutes, seconds);

        this.gameObject.GetComponent<TextMeshPro>().text = timer;
    }
}
