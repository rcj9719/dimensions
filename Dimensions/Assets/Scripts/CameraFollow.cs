using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = player.position + offset;
        if(gameObject.name == "MinimapCamera")
        {
            targetPos.x = 0;
            targetPos.y = 5;
        }
        transform.position = targetPos;
    }
}
