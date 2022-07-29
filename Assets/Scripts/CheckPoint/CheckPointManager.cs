using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using Game.Save;

namespace Game.CheckPoint
{
    public class CheckPointManager : Singleton<CheckPointManager>, ISave
    {
        private readonly string _keyName = "CheckPoint";
        public SO_DiePos diePos;

        [SerializeField]
        private SO_CheckPoints _checkPoints;


        [Space, SerializeField]
        private List<CheckPointBase> _checks;



        protected override void Awake()
        {
            base.Awake();
            _checks = new List<CheckPointBase>();

            Load(SaveManager.setUp);

            SaveManager.Loaded += Load;
            SaveManager.ToSave += Save;
        }

        public static void RegisterCheckPoint(CheckPointBase c)
        {
            Instance._checks.Add(c);
        }

        public static void UnregisterCheckPoint(CheckPointBase c)
        {
            Instance._checks.Remove(c);
        }

       
        public static Vector3 GetRespawnClosiestPos()
        {
            if (Instance.diePos.pos == null)
                return Vector3.zero;

            CheckPointBase aux = null;
            float minDist = 9999999f;
            foreach (CheckPointBase c in Instance._checks)
            {
                float currentDist = Vector3.Distance(Instance.diePos.pos, c.transform.position);
                if (currentDist < minDist && c.Active)
                {
                    minDist = currentDist;
                    aux = c;
                }
            }
            if(aux == null)
                return Vector3.zero;

            return aux.transform.position;
        }

        public void Save()
        {
            foreach (int cpU in _checkPoints.unlokeds)
            {
                SaveManager.setUp.checkPoints.AddUnique(cpU);
            }
        }

        public static bool CheckIsUnloked(int key)
        {
            if(Instance._checkPoints.unlokeds == null) return false;
            return Instance._checkPoints.unlokeds.Exists(x => x == key);
        }

        public static void SaveCheckPoint(CheckPointBase check)
        {
            Instance._checkPoints.unlokeds.AddUnique(check.keyValue);
        }

        public void Load(SaveSetUp setup)
        {
            _checkPoints.unlokeds = setup.checkPoints;
        }

        private void OnDestroy()
        {
            SaveManager.Loaded -= Load;
            SaveManager.ToSave -= Save;
        }
    }
}