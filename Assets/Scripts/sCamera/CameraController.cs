using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float camLeft = 0.0f;
    public float camRight = 0.0f;
    public float camTop = 0.0f;
    public float camBottom = 0.0f;

    GameObject player;
    Player playercontroller;

    public GameObject subBack1;
    public GameObject subBack2;
    public float subBackScrollSpeed =0.005f;
    public float subBackWidth = 19.1f;

    public bool isForceScrollX=false;
    public bool isForceScrollY=false;
    public float forceScrollSpeedX = 0.5f;
    public float forceScrollSpeedY = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playercontroller = player.GetComponent<Player>();
        this.transform.position = new Vector3(0, 0, -10);

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }

        float x,y;

        if (isForceScrollX)
        {
            x = transform.position.x + (forceScrollSpeedX * Time.deltaTime);
            x = Mathf.Clamp(x, camLeft, camRight);
        }
        else
        {
            x = Mathf.Clamp(player.transform.position.x, camLeft, camRight);
        }

        if (isForceScrollY)
        {
            y = transform.position.y + ( forceScrollSpeedY * Time.deltaTime);
            y = Mathf.Clamp( y, camLeft, camRight);
        }
        else
        {
            y = Mathf.Clamp(player.transform.position.y, camBottom, camTop);
        }

        //Debug.Log("x>> " + x);
        //Debug.Log("y>> " + y);

        Vector3 camPos = new Vector3(x, y, -10);
        Camera.main.transform.position = camPos;


        if (x > camLeft && x < camRight)
        {
            if (isForceScrollX == true)
            {
                Debug.Log("サブ背景：強制スクロール：X");

                subBack1.transform.localPosition -= new Vector3(forceScrollSpeedX * subBackScrollSpeed, 0, 0);
                subBack2.transform.localPosition -= new Vector3(forceScrollSpeedX * subBackScrollSpeed, 0, 0);

                SubBackPositionChange("right");

            }
            else
            {
                Debug.Log("サブ背景：通常スクロール：X");
                if (playercontroller.GetAxisH() != 0)
                {
                    subBack1.transform.localPosition -= new Vector3(playercontroller.GetAxisH() * subBackScrollSpeed, 0, 0);
                    subBack2.transform.localPosition -= new Vector3(playercontroller.GetAxisH() * subBackScrollSpeed, 0, 0);
                    if (playercontroller.GetAxisH() > 0)
                    {
                        SubBackPositionChange("right");
                    }

                    if (playercontroller.GetAxisH() < 0)
                    {
                        SubBackPositionChange("left");
                    }
                }
            }
        }
    }

    void SubBackPositionChange(string vector)
    {
        if (vector == "right")
        {
            if (subBack1.transform.localPosition.x <= -(subBackWidth))
            {
                float diff = -(subBackWidth) - subBack1.transform.localPosition.x;
                
                subBack1.transform.localPosition = new Vector3(
                    subBackWidth + diff,
                    subBack1.transform.localPosition.y,
                    subBack1.transform.localPosition.z);
            }

            if (subBack2.transform.localPosition.x <= -(subBackWidth))
            {
                float diff = -(subBackWidth) - subBack2.transform.localPosition.x;
                
                subBack2.transform.localPosition = new Vector3(
                    19.2f + diff,
                    subBack2.transform.localPosition.y,
                    subBack2.transform.localPosition.z);
            }
        }
        else if (vector == "left")
        {
            if (subBack1.transform.localPosition.x >= (subBackWidth))
            {
                float diff = subBack1.transform.localPosition.x - subBackWidth;
                subBack1.transform.localPosition = new Vector3(
                    -(subBackWidth) + diff,
                    subBack1.transform.localPosition.y,
                    subBack1.transform.localPosition.z);
            }
            if (subBack2.transform.localPosition.x >= (subBackWidth))
            {
                float diff = subBack2.transform.localPosition.x - subBackWidth;
                subBack2.transform.localPosition = new Vector3(
                    -(subBackWidth) + diff,
                    subBack2.transform.localPosition.y,
                    subBack2.transform.localPosition.z);
            }
        }
    }
  
}
