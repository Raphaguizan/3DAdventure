using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

namespace Game.Enemy
{
    public class EnemyMove : EnemyBase
    {
        [Header("Movement")]
        [SerializeField]
        private float _speed;
        [SerializeField]
        private Vector2 _randomTime;
        
        [Space, SerializeField, NaughtyAttributes.ReadOnly]
        private Vector2 _randomDirection;

        private void Awake()
        {
            StartCoroutine(CheckRandomTime());
            PlayAnimation(Animations.AnimationType.RUN);
        }
        private void Update()
        {
            if(isAlive)
                Move();
        }

        IEnumerator CheckRandomTime()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(_randomTime.x, _randomTime.y));
                ChangeDirection();
            }
        }
        public void Move()
        {
            transform.Translate(new Vector3(_randomDirection.x, 0, _randomDirection.y) * _speed * Time.deltaTime);
            
            RaycastHit hit;
            Vector3 posAux = transform.position;
            posAux.y += 10;

            if (Physics.Raycast(posAux, transform.TransformDirection(-Vector3.up), out hit, Mathf.Infinity))// && hit.transform.CompareTag("Ground"))
            {
                Debug.DrawLine(posAux, hit.point, Color.green);
                transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
            }
        }

        private void ChangeDirection() 
        {
            _randomDirection = new Vector2().Randomize();
        }
    }
}
