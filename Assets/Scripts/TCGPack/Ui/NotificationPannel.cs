using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace Card_GamePackage
{
    public class NotificationPannel : UiBase
    {
        enum eImages
        {
            NotificationPanel = 0
        }
        enum eTexts
        {
            NotificationTMP = 0
        }


        public override void Setup()
        {
            Bind<Image>(typeof(eImages));
            Bind<TextMeshProUGUI>(typeof(eTexts));
        }

        public TextMeshProUGUI GetTMP()
        {
            return Get<TextMeshProUGUI>((int)eTexts.NotificationTMP);
        }




        public void Show(string message)
        {
            GetTMP().text = message;
            Sequence sequence = DOTween.Sequence()
                //스케일이 커짐 SetEase -> dotween ease로 종류 확인가능
                .Append(transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InOutQuad))
                //딜레이 발생
                .AppendInterval(0.9f)
                //스케일 작아짐
                .Append(transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InOutQuad));
        }

        private void Start() => ScaleZero();

        [ContextMenu("ScaleOne")]
        public void ScaleOne() => transform.localScale = Vector3.one;
        [ContextMenu("ScaleZero")]
        public void ScaleZero() => transform.localScale = Vector3.zero;
    }
}