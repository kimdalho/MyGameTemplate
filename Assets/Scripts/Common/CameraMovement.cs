using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Vector2 startPos;
    float posX;
    float posY;
    public bool onDrag;
    public static GameObject firstObjext;


    public float maxX, maxY, minX, minY;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Agent.onDrag == true)
            return;



        if (Input.GetMouseButtonDown(0))
        { this.startPos = Input.mousePosition; }
        else if
         (Input.GetMouseButton(0))
        { Vector2 endPos = Input.mousePosition;
            float swipeLength = (this.startPos.x - endPos.x);
            float swipeLengthY = (this.startPos.y - endPos.y);
            this.posX = swipeLength / 1000.0f;
            this.posY = swipeLengthY / 1000.0f;
        }

        Camera.main.transform.Translate(this.posX, posY, 0);
        this.posX *= 0.98f;
        this.posY *= 0.98f;
    }

    private void LateUpdate()
    {
        if (Camera.main.transform.position.x > maxX)
        {
            Camera.main.transform.position =new Vector3( maxX, Camera.main.transform.position.y, -140);
        }
        if (Camera.main.transform.position.y > maxY)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, maxY, -140);
        }
        if (Camera.main.transform.position.x < minX)
        {
            Camera.main.transform.position = new Vector3(minX, Camera.main.transform.position.y, -140);
        }
        if (Camera.main.transform.position.y < minY)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, minY, -140);
        }
    }





}