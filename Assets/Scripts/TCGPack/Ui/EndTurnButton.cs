using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Card_GamePackage
{
    public class EndTurnButton : UiBase
    {
        public Sprite activeSprite;
        public Sprite inactiveSprite;


        enum eTMPs
        {
            EndTurnTMP = 0,
        }
        public override void Setup()
        {
            Bind<TextMeshProUGUI>(typeof(eTMPs));
            TurnManager.OnTurnStarted += SetButtonInteractable;

            SetButtonInteractable(false);
        }

        private void OnDestroy()
        {
            TurnManager.OnTurnStarted -= SetButtonInteractable;
        }
        public override void SetRenderSprite()
        {
            GetEndTurnButton().GetComponent<Image>().sprite = SpriteManager.Instance.GetButtonSprite();
        }
        public void SetButtonInteractable(bool isActive)
        {
            GetComponent<Image>().sprite = isActive ? activeSprite : inactiveSprite;
            GetEndTurnButton().interactable = isActive;
            Get<TextMeshProUGUI>(0).color = isActive ? new Color32(255, 195, 90, 255) : new Color32(55, 55, 55, 255);
        }

        public Button GetEndTurnButton()
        {
            return GetComponent<Button>();
        }
    }
}