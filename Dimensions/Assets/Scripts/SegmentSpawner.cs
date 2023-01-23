using UnityEngine;

public class SegmentSpawner : MonoBehaviour
{
    [SerializeField] GameObject corridorSegment;
    Vector3 nextSpawnPoint;
    bool wallCnt = false;
    public void SpawnTile(bool spawnWall)
    {
        GameObject temp = Instantiate(corridorSegment, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = temp.transform.GetChild(1).transform.position;     // get the child at 0th index

        if (spawnWall)
        {
            wallCnt = !wallCnt;
            temp.GetComponent<CorridorSegment>().SpawnWall(wallCnt);
        }
        //if (spawnPowerups)
        //{
        //    //temp.GetComponent<CorridorSegment>().SpawnPowerUps();
        //}

    }

    //// Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i < 1) SpawnTile(false);    // dont spawn obstacles and coins for first few tiles
            else SpawnTile(true);
            //SpawnTile(true);
        }
    }

}
