using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /*
     중심에서 각 모서리까지의 거리인 내부 반경도 있습니다. 이 메트릭은 각 이웃의 중심까지의 거리가 이 값의 두 배와 같기 때문에 중요합니다. 내부 반경은 다음과 같습니다.
    √
    삼
    2
     외부 반지름을 곱하면 
    5
    √
    삼
    우리의 경우. 쉽게 액세스할 수 있도록 이러한 메트릭을 정적 클래스에 넣습니다.*/

    public static class HexMetrics
    {

        public const float outerRadius = 10f;

        public const float innerRadius = outerRadius * 0.866025404f;

        /*
         *  육각 셀을 방향을 지정하는 방식은 두가지가 있다
         *  1. 모서리가 위를 바라보는 방법
         *  2. 평평한면이 위를 바라보는 방법
         *	3. 지금 진행할 배치는 위가 모서리를 바라보는 방식
         */

        public static Vector3[] corners = {
        new Vector3(0f, 0f, outerRadius),
        new Vector3(innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(0f, 0f, -outerRadius),
        new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(-innerRadius, 0f, 0.5f * outerRadius)
        };

    }
