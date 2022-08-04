using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

namespace Game.Sound
{
    public class MusicPooling : Singleton<MusicPooling>
    {
        [SerializeField]
        private List<AudioSource> _audiosSources;

        private GameObject _prefab;
        private AudioSource _sourceLooping;

        protected override void Awake()
        {
            base.Awake();
            _audiosSources = new List<AudioSource>();
            _prefab = transform.GetChild(0).gameObject;
            _audiosSources.Add(_prefab.GetComponent<AudioSource>());
        }

        public static void Play(AudioClip clip, bool loop = false)
        {
            AudioSource source = Instance.VerifyFreeSource();
            if (loop)
            {
                if(Instance._sourceLooping && Instance._sourceLooping.isPlaying) Instance._sourceLooping.Stop();
                Instance._sourceLooping = source;
            }
            source.loop = loop;
            source.clip = clip;
            source.Play();
        }

        private AudioSource VerifyFreeSource()
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