using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraMovement : MonoBehaviour ,IPlayerHit
{
    Vector2 startPos;
    float posX;
    float posY;

    public static GameObject firstObjext;
    [SerializeField]
    private Camera cam;
    public float maxX, maxY, minX, minY;
    public static bool shake;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"{gameObject.name}");
    }

    public IPlayerHit GetSubject()
    {
        return this;
    }

    void Update()
    {
        if (MouseController.onDrag)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("CamMovement Enter MouseButtonDown");
            this.startPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 endPos = Input.mousePosition;
            float swipeLength = (this.startPos.x - endPos.x);
            float swipeLengthY = (this.startPos.y - endPos.y);
            this.posX = swipeLength / 1000.0f;
            this.posY = swipeLengthY / 1000.0f;
            Debug.Log("CamMovement Enter MouseButton");
        }

        Camera.main.transform.Translate(this.posX, posY, 0);
        this.posX *= 0.98f;
        this.posY *= 0.98f;
    }

    private void LateUpdate()
    {
        if (cam.transform.position.x > maxX)
        {
            cam.transform.position =new Vector3( maxX, Camera.main.transform.position.y, -140);
        }
        if (cam.transform.position.y > maxY)
        {
            cam.transform.position = new Vector3(Camera.main.transform.position.x, maxY, -140);
        }
        if (cam.transform.position.x < minX)
        {
            cam.transform.position = new Vector3(minX, Camera.main.transform.position.y, -140);
        }
        if (cam.transform.position.y < minY)
        {
            cam.transform.position = new Vector3(Camera.main.transform.position.x, minY, -140);
        }
    }


    public IEnumerator Shake()
    {
        if (shake == true)
            yield break;


        shake = true;
        var tween = cam.DOShakePosition(0.3f, 1, 20, 30, false);
        yield return tween;
        shake = false;
    }

    public void HitPlayer()
    {
        Debug.Log("CamShake Call");
        StartCoroutine(Shake());
    }
}