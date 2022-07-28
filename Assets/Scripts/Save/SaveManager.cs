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

        [Space, SerializeField, NaughtyAttributes.ReadOnly]
        private bool _loaded;
        private string _path;
        
        public static SaveSetUp setUp;
        
        public static Action<SaveSetUp> Loaded;
        public static Action ToSave;

        public static bool IsLoaded => Instance ? Instance._loaded : false;

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
            if(setUp == null) setUp = new SaveSetUp();

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

        public static void LoadFeedBack()
        {
            Instance.StopAllCoroutines();
            Instance.StartCoroutine(Instance.LoadWaitTime());
        }
        IEnumerator LoadWaitTime()
        {
            yield return new WaitForSecondsRealtime(1f);
            Instance._loaded = false;
        }

        // salvar automaticamente quando o jogo perde o foco da janela
        //private void OnApplicationFocus(bool focus)
        //{
        //    if (!focus)
        //        Save();
        //}
    }
}