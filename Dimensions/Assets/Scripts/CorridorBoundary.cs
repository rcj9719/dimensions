using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorBoundary : MonoBehaviour
{
    PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Wall Collision");
        if (collision.gameObject.name == "Player")
        {
            playerMovement.Die();
        }
    }
}
