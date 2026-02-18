using Unity.VisualScripting;
using UnityEngine;

public class WorldCameraController : MonoBehaviour
{
    GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = new Vector3(0, 0, -10);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) {

            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
            //Debug.Log(transform.position);
        }
        else
        {
            //Debug.Log("not [player]");
        }
    }
}
