using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCounter : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerMovement playerMovement;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerExit(Collider other)
    {
        GameManager.inst.IncrementWallCount();
        if (GameManager.inst.getWallCount() != 0)
        {
            audioSource.Play();
        }
    }
}
