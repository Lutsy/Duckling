using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// will work
public class BreathBar : MonoBehaviour
{
    public DuckControl currentDuck;

    //breath control
    public Image breathBar;
    private float o2Amount = 1f;
    private float[] o2BarFill = { 0, 0.19f, 0.39f, 0.6f, 0.8f, 1f };

    // follow player
    public Transform playerTransform;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {

        FollowPlayer();

        UpdateBreath();

    }

    //not working
    public void FollowPlayer()
    {
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerTransform.position);

        rectTransform.position = playerScreenPoint;
    }

    private void UpdateBreath()
    {
        if (currentDuck != null)
        {
            if (currentDuck.IsUnderwater && o2Amount >= 0)
            {
                o2Amount -= 0.00005f;
               
                if (o2Amount < 0)
                {
                    o2Amount = 0;
                    breathBar.fillAmount = 0;
                    TriggerGameOver();
                    return;
                }
            }
            else if (o2Amount <= 1f)
            {
                o2Amount += 0.0003f;
                if (o2Amount > 1) o2Amount = 1;
            }

            breathBar.fillAmount = FillBar();
        }
    }

    //public GameOverScreen gameOverScreen;
    private void TriggerGameOver()
    {
        //Debug.Log("Game Over");
        SceneManager.LoadScene("GameOverScreen");

    }

    private float FillBar()
    {
        for (int i = 0; i < o2BarFill.Length - 1; i++)
        {
            if (o2Amount >= o2BarFill[i] && o2Amount <= o2BarFill[i + 1])
            {
                return o2BarFill[i+1];
            }
        }

        return 0f;
    }
}
