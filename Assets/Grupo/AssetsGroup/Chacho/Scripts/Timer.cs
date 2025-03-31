using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    [SerializeField] GameObject ArepaImg1;
    [SerializeField] GameObject ArepaImg2;
    [SerializeField] GameObject ArepaImg3;

    bool itemCollected = false;
    private void Start()
    {
        ArepaImg2.SetActive(false);
        ArepaImg3.SetActive(false);
    }
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

            if (remainingTime > 240)
            {
                ArepaImg1.SetActive(true);
           
            }
            if(remainingTime <= 240)
            {
                ArepaImg1.SetActive(false);
                ArepaImg2.SetActive(true);
            }
            if (remainingTime <= 120)
            {
                ArepaImg2.SetActive(false);
                ArepaImg3.SetActive(true);
            }

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
