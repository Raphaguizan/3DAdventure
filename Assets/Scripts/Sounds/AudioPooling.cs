using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

namespace Game.Sound
{
    public class AudioPooling : Singleton<AudioPooling>
    {
        [SerializeField]
        private List<AudioSource> _audiosSources;

        private GameObject _prefab;

        protected override void Awake()
        {
            base.Awake();
            _audiosSources = new List<AudioSource>();
            _prefab = transform.GetChild(0).gameObject;
            _audiosSources.Add(_prefab.GetComponent<AudioSource>());
        }

        public static void Play(AudioClip clip, Vector3 pos)
        {
            var source = Instance.VerifyFreeSource();
            source.transform.position = pos;
            source.clip = clip;
            source.Play();
        }
        public static void Play(AudioClip clip)
        {
            var source = Instance.VerifyFreeSource();
            source.clip = clip;
            source.Play();
        }

        private  AudioSource VerifyFreeSource()
        {
            foreach (var source in _audiosSources)
                if (!source.isPlaying)
                    return source;

            return CreateNewSource();
        }

        private AudioSource CreateNewSource()
        {
            GameObject newSourceGO = Instantiate(_prefab, transform);
            AudioSource newSource = newSourceGO.GetComponent<AudioSource>();
            _audiosSources.Add(newSource);
            return newSource;
        }
    }
}