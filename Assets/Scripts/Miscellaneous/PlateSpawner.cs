using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateSpawner : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject platePrefab;
    [SerializeField] private Transform plateSpawnPoint;
    [SerializeField] private Animator btnAnimator;

    [Header("Settings")]
    [SerializeField] private float waitTime;

    private bool playerOnTrigger;
    private bool ableToSpawn = true;

    private void Update()
    {
        if (!playerOnTrigger) return;
        if (Input.GetKeyDown(KeyCode.E) && ableToSpawn) OrderPlate();
    }

    private void OrderPlate()
    {
        Instantiate(platePrefab, plateSpawnPoint.position, Quaternion.identity);
        btnAnimator.Play("Press");
        ableToSpawn = false;
        Invoke("RestoreSpawn", waitTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        UIManager.Instance.uiBottomMessage.SetText("Press [E] To Order A Plate");
        playerOnTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        UIManager.Instance.uiBottomMessage.SetText("");
        playerOnTrigger = false;
    }

    private void RestoreSpawn()
    {
        ableToSpawn = true;
    }
}
