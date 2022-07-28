using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using Game.Util;

namespace Game.Save
{
    public class SaveManager : Singleton<SaveManager>
    {
        [SerializeField]
        private string _fileName = "/Save.txt";

        public static Action<SaveSetUp> Loaded;

        private string _path;
        private SaveSetUp _setUp;

        public static SaveSetUp GetSetup => Instance._setUp;
        protected override void Awake()
        {
            base.Awake();
            _path = Application.streamingAssetsPath + _fileName;
            DontDestroyOnLoad(this);
        }
        public void Save(SaveSetUp setUp)
        {
            string json = JsonUtility.ToJson(setUp);
            _setUp = setUp;
            File.WriteAllText(_path, json);
        }

        public void Load()
        {
            string json = File.ReadAllText(_path);
            _setUp = JsonUtility.FromJson<SaveSetUp>(json);
            
            if (_setUp == null)
            {
                _setUp = new SaveSetUp();
                Save(_setUp);
            }

            Loaded?.Invoke(_setUp);
        }
    }
}