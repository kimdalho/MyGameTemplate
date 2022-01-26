using UnityEngine.UI;

namespace Card_GamePackage
{
    public class PlayButton : UiBase
    {
        public enum eTexts
        {
            //GameEdit Object Name
            PlayButtonText = 0,
        }

        public enum eButtons
        {
            PlayButton = 0,
        }

        public override void Setup()
        {
            Bind<Button>(typeof(eButtons));
            Bind<Text>(typeof(eTexts));
        }

        public Button GetStartButton()
        {
            return Get<Button>((int)eButtons.PlayButton);
        }

    }
}