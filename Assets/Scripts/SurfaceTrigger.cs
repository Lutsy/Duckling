using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D waterSurface)
    {
        if (waterSurface.CompareTag("Player"))
        {
            DuckControl playerController = waterSurface.GetComponent<DuckControl>();
            if (playerController != null)
            {
                playerController.ToggleSwim(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D waterSurface)
    {
        if (waterSurface.CompareTag("Player"))
        {
            DuckControl playerController = waterSurface.GetComponent<DuckControl>();
            if (playerController != null)
            {
                playerController.ToggleSwim(false);
            }
        }
    }
}

