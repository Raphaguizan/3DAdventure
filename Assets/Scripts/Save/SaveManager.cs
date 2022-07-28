using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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
        private bool _loaded;
        
        public static SaveSetUp setUp;
        
        public static Action<SaveSetUp> Loaded;
        public static Action ToSave;

        public static bool IsLoaded => Instance._loaded;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
            _path = Application.streamingAssetsPath + _fileName;
            _loaded = false;
            // Auto Load
            //if(!_loaded)Load();
        }
        

        public void Save(SaveSetUp setup)
        {
            Debug.Log("Salvando...");
            string json = JsonUtility.ToJson(setup, true);
            File.WriteAllText(_path, json);
        }
        public static void Save()
        {
            ToSave?.Invoke();
            Instance.StartCoroutine(Instance.WaitToSave());
        }

        IEnumerator WaitToSave()
        {
            yield return new WaitForEndOfFrame();
            Save(setUp);
        }

        public static void Load()
        {
            Debug.Log("Carregando...");
            try
            {
                string json = File.ReadAllText(Instance._path);
                setUp = JsonUtility.FromJson<SaveSetUp>(json);

            }catch (FileNotFoundException e)
            {
                Debug.Log("criando novo arquivo");
                setUp = new SaveSetUp();
                Instance.Save(setUp);
            }

            Instance._loaded = true;
            Loaded?.Invoke(setUp);
        }

        // salvar automaticamente quando o jogo perde o foco da janela
        //private void OnApplicationFocus(bool focus)
        //{
        //    if (!focus)
        //        Save();
        //}
    }
}