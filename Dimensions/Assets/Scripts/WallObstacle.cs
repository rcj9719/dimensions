using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallObstacle : MonoBehaviour
{
    PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerExit(Collider collision)
    {
        if (!transform.GetChild(0).gameObject.activeSelf)
        {
            GameManager.inst.IncrementScore();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (transform.GetChild(0).gameObject.activeSelf)
        {
            playerMovement.Die();
        }
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
