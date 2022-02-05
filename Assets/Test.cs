using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{


    public Transform Left;
    public Transform Right;

    public GameObject pointPrfab;
    public float value;

    public float circleX;

    //포인트들의 X좌표를 모두 가지고있다
    public Queue<Vector2> XList = new Queue<Vector2>();

    //반지름 값이다
    public float r;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {

            StartCoroutine(COTester());
           
        }
    }

    private IEnumerator COTester()
    {
        float count = 0.1f;

        while(count < 1)
        {
            //t = 0~1만을 갔는다 0보다 작으면 0으로 기준이되고 1보다 크면 1만을 기준으로한다
            var result = Vector3.Lerp(Left.position, Right.position, count);


            if(count == 0.5f)
            {
                circleX = result.x;
            }
            
            XList.Enqueue(result);
            Util.Log(string.Format("result = {0}", result));
        
            count += 0.1f;
        }

        while(XList.Count > 0)
        {
           

            //원의 중심의 x좌표값은 circleX이다
            //var point = Instantiate(pointPrfab);
            //point.transform.position = result;
            //원의 방정식 공식
            Vector2 vec = XList.Dequeue();
            //float Y = Mathf.Sqrt(Mathf.Pow(r, 2) - Mathf.Pow((vec.x - circleX), 2));
            float Y = Mathf.Sqrt(Mathf.Pow(r, 2) - Mathf.Pow((vec.x - circleX), 2));
            Y = r >= 0 ? Y : -Y;
            var targetRot = Util.QI;
            targetRot = Quaternion.Slerp(Left.rotation, Right.rotation, vec.x);
            var point = Instantiate(pointPrfab);
            point.transform.position = new Vector2(vec.x, vec.y + Y);
            point.transform.rotation = targetRot;
            yield return new WaitForSeconds(0.1f);
        }




   
    }

    



}
