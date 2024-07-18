using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTrigger : MonoBehaviour
{
    private bool playerInRange = false;
    private SpriteRenderer spriteRenderer;

    DuckControl currentDuck;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();

            if (boxCollider != null)
            {
                string colliderName = boxCollider.name;

                // Now you can check the name of the collider and perform actions accordingly
                switch (colliderName)
                {
                    case "Knife":
                        PickUpKnife();
                        break;
                    case "ColourBefore":
                        CutSeaweed();
                        break;

                    default:
                        Debug.LogWarning($"Out of Range: {colliderName}");
                        break;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            currentDuck = other.GetComponent<DuckControl>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }


    private void PickUpKnife()
    {
        if (currentDuck != null)
            if (playerInRange)
            {
                HideCurrentInteractable();
                currentDuck.ShowKnife();
            }
    }

    private void CutSeaweed()
    {
        if (currentDuck != null)
            if (currentDuck.HasKnife)
                HideCurrentInteractable();
    }

    private void HideCurrentInteractable()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }
    }

}
