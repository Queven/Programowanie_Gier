using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meta : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name != "Sphere") return;

        FindObjectOfType<GameController>().CheckIfEndOfGame();
    }
}
