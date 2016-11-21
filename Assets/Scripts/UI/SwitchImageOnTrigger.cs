using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Controllers.Cannons;

namespace UI
{
    public class SwitchImageOnTrigger : MonoBehaviour
    {
        public Sprite Sprite1 = null;
        public Sprite Sprite2 = null;
        private Image UiImage = null;
        private bool FirstSpriteActive = true;

        void Awake()
        {
            UiImage = GetComponent<Image>();
        }

        public void SwitchImage()
        {
            if (FirstSpriteActive)
            {
                UiImage.sprite = Sprite2;
                FirstSpriteActive = false;
            }
            else
            {
                UiImage.sprite = Sprite1;
                FirstSpriteActive = true;
            }
        }

        public void SwitchImage(bool activateFirstSprite)
        {
            if (!activateFirstSprite)
            {
                if (!FirstSpriteActive)
                {
                    return;
                }

                UiImage.sprite = Sprite2;
                FirstSpriteActive = false;
            }
            else
            {
                if (FirstSpriteActive)
                {
                    return;
                }

                UiImage.sprite = Sprite1;
                FirstSpriteActive = true;
            }
        }
    }
}
