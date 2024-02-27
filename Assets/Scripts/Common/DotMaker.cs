using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotMaker : MonoBehaviour
{
    public GameObject[] ghostObjectArray;

    public float moveSpeed = 3f;
    public float rangeAngle = 25f;
    public float rangeDistance = 4f;
    void Update()
    {
        PlayerMove();
        CheckGhost();
    }

    void PlayerMove()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        //GetAxis를 사용하면 조이스틱 컨트롤러 기능 구현
        //Horizontal : x축, Vertical : y축
        transform.Translate(new Vector3(x, y) * (moveSpeed * Time.deltaTime));
    }

    void CheckGhost()
    {
        int i = 0; //유령의 수 초기값은 0
        foreach (var ghost in ghostObjectArray) //foreach를 이용해서 각 오브젝트를 전부 체크
        {
            Vector3 distanceVec = ghost.transform.position - transform.position; //도착지 - 출발지
            if (distanceVec.magnitude < rangeDistance) //magnitude는 거리값
            {
                Vector3 dirVec = distanceVec.normalized; //내적을 하기 위해 방향벡터로 만듦

                if (Vector3.Dot(transform.up, dirVec) > Mathf.Cos(rangeAngle * Mathf.Deg2Rad))
                    i++;
                //transform.rigjt/up Vector2.right.left.up.down 전부 방향벡터(크기 1)
                //transform.up / dirVec 전부 방향 벡터
                //Vector3.Dot = 내적해줌
            }
        }

        Debug.Log("감지된 유령의 수: " + i);
    }
}
