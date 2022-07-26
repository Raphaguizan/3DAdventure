using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Cloth
{
    public class ClothChanger : MonoBehaviour
    {
        [SerializeField]
        private Material material;
        [SerializeField]
        private string _textureKey = "_EmissionMap";
        [Header("TESTE"), SerializeField]
        private ClothType _testType;

        [NaughtyAttributes.Button]
        public void ChangeCloth()
        {
            ChangeCloth(_testType);
        }

        public void ChangeCloth(ClothSetup cloth)
        {
            material.SetTexture(_textureKey, cloth.texture);
        }

        public void ChangeCloth(ClothType type)
        {
            ChangeCloth(ClothManager.GetClothByType(type));
        }

        public void ChangeCloth(ClothType type, float duration)
        {
            StartCoroutine(ChangeClothTime(type, duration));
        }

        IEnumerator ChangeClothTime(ClothType type, float duration)
        {
            ChangeCloth(type);
            yield return new WaitForSeconds(duration);
            ResetCloth();
        }

        public void ResetCloth()
        {
            ChangeCloth(ClothType.DEFAULT);
        }

        private void OnDestroy()
        {
            ResetCloth();
        }
    }
}