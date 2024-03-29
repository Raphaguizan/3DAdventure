using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;
using Game.Player;
using Game.Item;
using Game.Sound;

public class ChestBase : MonoBehaviour
{
    [SerializeField, ReadOnly]
    private bool _isOpen = false;

    [Space, SerializeField, Tag]
    private string _playerTag;

    [Space, SerializeField]
    private Animator _animator;
    [SerializeField]
    private string _playerCloseKey = "PlayerClose";
    [SerializeField]
    private string _openKey = "Open";

    [Space, SerializeField]
    private ParticleSystem _particle;
    [SerializeField]
    private AudioClip _sound;

    [Space, SerializeField]
    private ItemType _itemType;
    [SerializeField]
    private int itemNum;

    [Space]
    public UnityEvent OnOpenCallBack;

    public bool IsOpen => _isOpen;

    private void OnTriggerEnter(Collider other)
    {
        if (IsOpen) return;
        if (other.CompareTag(_playerTag))
        {
            PlayerInputController.OnInteractCallBack += OpenChest;

            _animator.SetBool(_playerCloseKey, true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (IsOpen) return;
        if (other.CompareTag(_playerTag))
        {
            PlayerInputController.OnInteractCallBack -= OpenChest;

            _animator.SetBool(_playerCloseKey, false);
        }
    }

    private void OpenChest()
    {
        _isOpen = true;
        _animator.SetBool(_openKey, IsOpen);

        PlayerInputController.OnInteractCallBack -= OpenChest;
        if (_sound) AudioPooling.Play(_sound, transform.position);
        ItemCollect();
        OnOpenCallBack.Invoke();
    }

    private void ItemCollect()
    {
        if(_particle)_particle.Play();
        ItensManager.AddItem(_itemType, itemNum);
    }
}
