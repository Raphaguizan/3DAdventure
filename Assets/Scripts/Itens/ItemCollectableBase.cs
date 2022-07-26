using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Item
{
    [RequireComponent(typeof(Collider))]
    public class ItemCollectableBase : MonoBehaviour
    {
        public ItemType type;
        [Header("Collectable"), NaughtyAttributes.Tag]
        public string playerTag = "Player";
        public float timeToDestroy = 3f;
        public GameObject Image;
        [Header("Effects")]
        public ParticleSystem particles;
        public AudioSource audioSource;

        protected GameObject playerGO;

        private Collider[] _collisionBox;
        private void Awake()
        {
            _collisionBox = GetComponents<Collider>();
            foreach (Collider coll in _collisionBox)
                coll.enabled = true;
            Initialize();
        }

        protected virtual void Initialize(){}

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag(playerTag))
            {
                playerGO = collision.gameObject;
                Collect();
            }
        }

        protected virtual void Collect()
        {
            OnCollet();
            if(audioSource) audioSource.Play();
            if(particles) particles.Play();
            DisableObj();
        }

        protected virtual void DisableObj()
        {
            Image.SetActive(false);

            foreach (Collider coll in _collisionBox)
                coll.enabled = false;

            Destroy(gameObject, timeToDestroy);
        }

        protected virtual void OnCollet() 
        {
            _ = ItensManager.AddItem(type);
            // Debug.Log("coletando moeda " + gameObject.name);
        }
    }
}