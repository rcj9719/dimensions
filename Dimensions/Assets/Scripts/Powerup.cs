using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerType
{
    FREEZE,
    HIDE
}

public class Powerup : MonoBehaviour
{
    [SerializeField] float turnSpeed = 90f;   // means it will rotate 90 degrees every second
    [SerializeField] Material freezeMat;
    [SerializeField] Material invMat;

    PowerType powerType;

    
    // will work only if at least one of the object is a rigidbody, you can add it
    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.GetComponent<WallObstacle>() != null)  // an obstacle will have the component of type WallObstacle as we have added WallObstacle script to it
        //{
        //    Destroy(gameObject);
        //    return;
        //}

        // Check if the object we collided with is the player
        if (other.gameObject.name != "Player")
        {
            return;
        }


        // Add to the player's score
        GameManager.inst.IncrementPower(powerType);

        // Destroy this coin object
        Destroy(gameObject);
    }

    public void SetPowerupType(PowerType power)
    {
        powerType = power;
        if(powerType == PowerType.FREEZE)
        {
            transform.GetChild(0).GetComponent<Renderer>().material = freezeMat;
        }
        else if (powerType == PowerType.HIDE)
        {
            transform.GetChild(0).GetComponent<Renderer>().material = invMat;
        }
    }

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
    }
}
