using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!(other.CompareTag("Ingredient") || other.CompareTag("Plate"))) return;
        Destroy(other.gameObject);
    }
}
