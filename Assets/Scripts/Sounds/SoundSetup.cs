using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace Game.Sound
{
    public class SoundSetup : MonoBehaviour
    {
        [SerializeField]
        private AudioMixerGroup _mixerGroup;
        [SerializeField]
        private Slider _slider;


        private readonly string _sfxParameter = "SFXVolume";
        private readonly string _musicParameter = "MusicVolume";

        private string _volumeParameter;

        private void OnEnable()
        {
            _slider.value = GetVolume();
        }

        public void OnSlideChange(float val)
        {
            _mixerGroup.audioMixer.SetFloat(_volumeParameter, val);
        }

        private float GetVolume()
        {
            if (_volumeParameter == null) _volumeParameter = GetVolumeParameter();
            _mixerGroup.audioMixer.GetFloat(_volumeParameter, out float vol);
            return vol;
        }

        private string GetVolumeParameter()
        {
            if (_mixerGroup.name.Equals("SFX"))
                return _sfxParameter;
            else
                return _musicParameter;
        }
    }
}