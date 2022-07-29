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
        [SerializeField, NaughtyAttributes.ReadOnly]
        private bool _loadRequired = false;
        
        private string _path;

        public static SaveSetUp setUp = new SaveSetUp();

        public static bool LoadRequired
        {
            get => Instance._loadRequired;
            set => Instance._loadRequired = value;
        }

        public static Action<SaveSetUp> Loaded;
        public static Action ToSave;


        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
            _path = Application.streamingAssetsPath + _fileName;
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
                Instance.Save(setUp);
            }
            Instance._loadRequired = true;
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