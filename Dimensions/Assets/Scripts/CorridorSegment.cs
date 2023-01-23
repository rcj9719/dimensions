using System;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using UnityEngine.Profiling;
using Random = UnityEngine.Random;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CorridorSegment : MonoBehaviour
{
    SegmentSpawner groundSpawner;

    [SerializeField] GameObject powerUpPrefab;

    [SerializeField] GameObject obstaclePrefabEven;
    [SerializeField] GameObject obstaclePrefabOdd;

    List<List<GameObject>> wall = new List<List<GameObject>>();

    private void Awake()
    {
        wall.Clear();
        wall.Add(new List<GameObject>());
    }

    //// Start is called before the first frame update
    void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<SegmentSpawner>();
    }

    private void OnTriggerExit(Collider other)
    {
        //// called when player collider moves out of the ground collider
        groundSpawner.SpawnTile(true);
        Destroy(gameObject, 2); // destroys gameobject after 2 seconds
    }

    public void SpawnWall(bool wallCnt)
    {
        Transform spawnPoint = transform.GetChild(0).transform; // wall obstacle is at 0th index

        GameObject obstacle = wallCnt ? obstaclePrefabOdd : obstaclePrefabEven;

        for (int i = -10; i < 10; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                Vector3 position = new Vector3(i/2.0f, j/2.0f, 0);
                position.z = spawnPoint.position.z;
                GameObject tempVoxel = Instantiate(obstacle, position, Quaternion.identity, transform);   // transform here means "this" object's transform, instantiating it like this will parent this object
                tempVoxel.name = "v" + (i+10).ToString() + j.ToString();
                wall[i + 10].Add(tempVoxel.transform.GetChild(0).gameObject);
            }
            wall.Add(new List<GameObject>());
        }
        wall.Remove(wall[20]);
        GenerateWindows();

    }

    void GenerateWindows()
    {
        int noOfWindows = Random.Range(2, 5);    // number of windows 2 to 3

        //Debug.Log("[CorridorSegment][GenerateWindows] noOfWindows: " + noOfWindows);

        int row;
        int column;
        int winScaleX;
        int winScaleY;

        //place windows
        for (int i = 0; i < noOfWindows; i++)
        {
            row = Random.Range(1, 20);
            column = Random.Range(1, 20);
            winScaleX = Random.Range(2, 6);
            winScaleY = Random.Range(2, 6);

            while (!okToPlaceWindowAt(row, column, winScaleX, winScaleY))
            {
                row = Random.Range(1, 20); ;
                column = Random.Range(1, 20);
                winScaleX = Random.Range(2, 6);
                winScaleY = Random.Range(2, 6);
            }

            placeWindowAt(row, column, winScaleX, winScaleY, i);
        }
    }

    bool okToPlaceWindowAt(int row, int column, int winScaleX, int winScaleY)
    {
        int leftbound = column - 1;
        int rightbound = column + winScaleX + 1;
        
        int lowerbound = row - 1;
        int upperbound = row + winScaleY + 1;

        bool isOkToPlaceWindow = true;

        if (leftbound >= 0 && rightbound < 20 && lowerbound >= 0 && upperbound < 20)
        {
            for (int i = leftbound; i <= rightbound; i++)
            {
                for (int j = lowerbound; j <= upperbound; j++)
                {
                    if (!wall[i][j].activeSelf)
                    {
                        isOkToPlaceWindow = false;
                        break;
                    }
                }
            }
        }
        else
        {
            isOkToPlaceWindow = false;
        }

        return isOkToPlaceWindow;
    }

    void placeWindowAt(int row, int column, int winScaleX, int winScaleY, int winNo)
    {
        int rightbound = column + winScaleX;
        int upperbound = row + winScaleY;

        int powerUpType = -1;
        int powerUpCol = -1;
        int powerUpRow = -1;

        //if (winNo == 0)
        //{
        //    int isPowerUpPresent = Random.Range(0, 2);
        //    powerUpType = isPowerUpPresent == 1 ? Random.Range(0, 2) : -1;

        //    powerUpCol = Random.Range(column, rightbound);
        //    powerUpRow = Random.Range(row, upperbound);
        //}

        powerUpType = 1;

        powerUpCol = Random.Range(column, rightbound);
        powerUpRow = Random.Range(row, upperbound);

        for (int i = column; i < rightbound; i++)
        {
            for (int j = row; j < upperbound; j++)
            {
                if (i < 20 && j < 20 && i >= 0 && j >= 0)
                {
                    GameObject temp = wall[i][j];
                    temp.SetActive(false);

                    if (winNo == 0 && i == powerUpCol && j == powerUpRow)
                    {
                        if (powerUpType == (int)PowerType.FREEZE)
                        {
                            Instantiate<GameObject>(powerUpPrefab, temp.transform.position, Quaternion.identity).GetComponent<Powerup>().SetPowerupType(PowerType.FREEZE);
                        }
                        else if (powerUpType == (int)PowerType.HIDE)
                        {
                            Instantiate<GameObject>(powerUpPrefab, temp.transform.position, Quaternion.identity).GetComponent<Powerup>().SetPowerupType(PowerType.HIDE);
                        }
                    }
                }
            }
        }
    }

}
