using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

    bool itemCollected = false;

    private void Update()
    {
        TimeCounter();
    }

    void TimeCounter()
    {
        if (itemCollected)
        {
            //NextScene()
            return;
        }
        else if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            //GameOver()
            timerText.color = Color.red;
            Debug.Log("Se quemó la arepa :c");
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void SetItemCollected() // Método para marcar la recogida
    {
        Debug.Log("SetItemCollected() fue llamado, deteniendo el tiempo");

        itemCollected = true;
    }


}
