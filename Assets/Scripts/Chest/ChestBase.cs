using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ChestBase : MonoBehaviour
{
    [SerializeField, Tag]
    private string _playerTag;

    [Space, SerializeField]
    private Animator _animator;
    [SerializeField]
    private string _playerCloseKey = "PlayerClose";
    [SerializeField]
    private string _openKey = "Open";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_playerTag))
        {
            _animator.SetBool(_playerCloseKey, true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_playerTag))
        {
            _animator.SetBool(_playerCloseKey, false);
        }
    }
}
