using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class Damage : UiBase
{
    enum eTMP_texts
    {
        DamageTMP = 0,
    }

    Transform tr;

    public void SetupTransform(Transform tr)
    {
        Bind<TMP_Text>(typeof(eTMP_texts));
        this.tr = tr;
    }

    private void Update()
    {
        if (tr != null)
            transform.position = tr.position;

    }

    public void Damaged(int damage)
    {
        if (damage <= 0)
            return;

        GetComponent<Order>().SetOrder(1000);
        GetTMP_Text().text = $"-{damage}";

        Sequence sequnce = DOTween.Sequence()
        .Append(transform.DOScale(Vector3.one * 1.8f, 0.5f).SetEase(Ease.InOutBack))
        .AppendInterval(1.2f)
        .Append(transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InOutBack))
        .OnComplete(() => Destroy(gameObject));
    }

    public TMP_Text GetTMP_Text()
    {
        return Get<TMP_Text>(0);
    }
}
