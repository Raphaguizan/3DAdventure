using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

namespace Game.CheckPoint
{
    public class CheckPointManager : Singleton<CheckPointManager>
    {
        private readonly string _keyName = "CheckPoint";

        [SerializeField]
        private CheckPointBase _lastCheckPoint = null;

        private bool _loadComplete = false;
        [SerializeField]
        private List<CheckPointBase> _checks;

        public static bool LoadComplete => Instance._loadComplete;
        public static int LastCheckValue
        {
            get
            {
                if (Instance._lastCheckPoint)
                    return Instance._lastCheckPoint.keyValue;
                return 0;
            }
        }

        protected override void Awake()
        {
            _checks = new List<CheckPointBase>();
            _loadComplete = false;
            base.Awake();
        }
        
        private void OnEnable()
        {
            StartCoroutine(LoadCheckPoint());
        }
        private IEnumerator LoadCheckPoint()
        {
            if (PlayerPrefs.HasKey(_keyName))
            {
                int lastValue = PlayerPrefs.GetInt(_keyName);
                yield return new WaitForEndOfFrame();

                _lastCheckPoint = _checks.Find(c => c.keyValue == lastValue);
            }
            _loadComplete = true;
        }

        public static void RegisterCheckPoint(CheckPointBase c)
        {
            Instance._checks.Add(c);
        }

        public static void UnregisterCheckPoint(CheckPointBase c)
        {
            Instance._checks.Remove(c);
        }

        public static void SaveCheckPoint(CheckPointBase check)
        {
            if(Instance._lastCheckPoint == null || check.keyValue > Instance._lastCheckPoint.keyValue)
            {
                PlayerPrefs.SetInt(Instance._keyName, check.keyValue);
                Instance._lastCheckPoint = check;
            }
        }

        public static Vector3 GetRespawnPos()
        {
            if(Instance._lastCheckPoint == null)
                return Vector3.zero;

            return Instance._lastCheckPoint.transform.position;
        }
    }
}