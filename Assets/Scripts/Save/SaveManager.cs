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

        private string _path;
        private bool _loaded = false;
        
        public static SaveSetUp setUp;
        
        public static Action<SaveSetUp> Loaded;
        public static Action ToSave;

        public static bool IsLoaded => Instance._loaded;
        protected override void Awake()
        {
            base.Awake();
            _path = Application.streamingAssetsPath + _fileName;
            Load();
            DontDestroyOnLoad(this);
        }

        public void Save(SaveSetUp setup)
        {
            Debug.Log("Salvando...");
            string json = JsonUtility.ToJson(setup, true);
            File.WriteAllText(_path, json);
        }
        public void Save()
        {
            ToSave?.Invoke();
            StartCoroutine(WaitToSave());
        }

        IEnumerator WaitToSave()
        {
            yield return new WaitForEndOfFrame();
            Save(setUp);
        }

        public void Load()
        {
            Debug.Log("Carregando...");
            try
            {
                string json = File.ReadAllText(_path);
                setUp = JsonUtility.FromJson<SaveSetUp>(json);

            }catch (FileNotFoundException e)
            {
                Debug.Log("criando novo arquivo");
                setUp = new SaveSetUp();
                Save(setUp);
            }

            _loaded = true;
            Loaded?.Invoke(setUp);
        }

        private void OnApplicationFocus(bool focus)
        {
            if (!focus)
                Save();
        }
    }
}