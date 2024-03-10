using UnityEngine;
using Hex_Package;
/// <summary>
/// 기능의 설명
/// 1. 중요한 기능은 단 하나다
/// 2. A Active 대상, B event 
/// 3. B 특정 이벤트의 발생 여부에따라 Active가 일어난다
/// 4. 마우스가 OnEnter -> True 시 CardArea 객체는 올라간다
/// 4. 마우스가 OnEnter -> Faild 시 CardArea 객체는 내려간다
/// </summary>
/// 

public abstract class BaseShowArea : MonoBehaviour
{
    public string targetLayer_Name;
    public  Transform targetTranform;
    public  bool onShowArea;


    private void Update()
    {

        DetectShowArea();

        if (onShowArea)
            Show();
        else
            Hide();
    }

    protected virtual void DetectShowArea() { }

    protected virtual void Show() { }

    protected virtual void Hide() { }

}
