using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{

    [SerializeField]
    private float duration = 5f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pacman"))
        {
            StartCoroutine(Pickup(other));
        }
    }

    IEnumerator Pickup(Collider player)
    {
        PacmanController pcInstance = player.GetComponent<PacmanController>();
        pcInstance.isPowerUp = true;

        yield return new WaitForSeconds(duration);

        pcInstance.isPowerUp = false;

        Destroy(gameObject);
    }
}
