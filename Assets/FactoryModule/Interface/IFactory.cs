using UnityEngine;
/// <summary>
/// �ν��Ͻ��� �����ϴ� ������Ʈ
/// TInput : �����Ǿ��� ������ Ŭ����
/// TOutput : ��ȯ�Ǵ� Ŭ����
/// </summary>
public interface IFactory<TInput, TOutput>
{
    TOutput Create(TInput data, Transform parent);
}