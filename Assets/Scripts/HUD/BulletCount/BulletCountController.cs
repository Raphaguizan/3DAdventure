using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Bullet
{
    public class BulletCountController : GunUIBase
    {
        [SerializeField]
        private Image _fillImage;

        public override void UpdateUI(float current, int max)
        {
            UpdateUI(1-(current/max));

        }
        public override void UpdateUI(float value)
        {
            if (_fillImage) _fillImage.fillAmount = value;
        }
    }
}

