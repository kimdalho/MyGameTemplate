using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;

namespace Card_GamePackage
{
    public class ResultPanel : UiBase
    {
        enum eImages
        {
            TitlePanel = 0,
            FramePanel = 1,
        }

        enum eButtons
        {
            ResultButton = 0,
        }
        enum eTMP_Texts
        {
            TitlePanelTMP = 0,
            ResultButtonTMP = 1,
        }

        public override void Setup()
        {
            Bind<Image>(typeof(eImages));
            Bind<Button>(typeof(eButtons));
            Bind<TextMeshProUGUI>(typeof(eTMP_Texts));

            GetReStartButton().onClick.AddListener(ReStart);
            ScaleZero();
            Hide();
        }

        public void Show(string message)
        {
            gameObject.SetActive(true);
            gameObject.transform.localScale = Vector3.one;
            GetTitlePanelTMP().text = message;
            GetFramePanel().transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutQuad);
        }

        [ContextMenu("Blind")]
        public void Blind()
        {
            gameObject.transform.localScale = Vector3.zero;
        }
        public void ScaleZero() => GetFramePanel().transform.localScale = Vector3.zero;

        public void ReStart()
        {
            SceneManager.LoadScene(0);
        }

        //###########Getter########################

        public Image GetFramePanel()
        {
            return Get<Image>((int)eImages.FramePanel);
        }

        public Button GetReStartButton()
        {
            return Get<Button>((int)eButtons.ResultButton);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public TextMeshProUGUI GetResultTMP()
        {
            return Get<TextMeshProUGUI>((int)eTMP_Texts.ResultButtonTMP);
        }

        public TextMeshProUGUI GetTitlePanelTMP()
        {
            return Get<TextMeshProUGUI>((int)eTMP_Texts.TitlePanelTMP);
        }

        //###########Getter########################

    }
}