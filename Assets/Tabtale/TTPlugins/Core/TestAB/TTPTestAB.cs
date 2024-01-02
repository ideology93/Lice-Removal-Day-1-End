using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;
using Object = UnityEngine.Object;

namespace Tabtale.TTPlugins
{
    public class TTPTestAB
    {
        private static TTPTestAB _testAb;
        private static TTPTestABViewController _controller;
        private static GameObject _testAbConsole;

        // It's not a real url and should be changed 
        private const string CONFIG_URL =
            "http://com.tabtale.psdk.services.test.s3.amazonaws.com/psdkJsonLinks/mbk/abtest.json";
        private const string STORED_FILENAME = "testAb.json";
        
        private static string _storedFilePath;
        private static Dictionary<string, object> _configDictionary = new Dictionary<string, object>();

        public static Action OnHideConsole;
        
        public static void Create()
        {
            _testAb = new TTPTestAB();
        }

        private TTPTestAB()
        {
            _storedFilePath = Path.Combine(Application.persistentDataPath, STORED_FILENAME);
            SetupConsole();
            LoadConfigFromFileAndApply();
            LoadConfigFromWeb();
        }

        public static void Show()
        {
            if (_controller != null)
            {
                _controller.Show();
            }
        }

        public static void ApplyExperimentWithVariant(string experimentName, string variantName)
        {
            var variantsDict = _configDictionary[experimentName] as Dictionary<string, object>;
            if (variantsDict != null)
            {
                var jsonData = TTPJson.Serialize(variantsDict[variantName]);
                SendOnRemoteConfigUpdate(jsonData);
                SaveConfigToFile(experimentName, variantName);
            }
        }

        public static string[] GetExperiments()
        {
            return _configDictionary.Keys.ToArray();
        }

        public static string[] GetVariantNamesForExperiment(String name)
        {
            var variants = _configDictionary[name] as Dictionary<String, object>;
            return variants.Keys.ToArray();
        }
        
        /// Private
        
        private static void LoadConfigFromFileAndApply()
        {
            if (File.Exists(_storedFilePath))
            {
                Debug.Log("TTPTestAB::LoadConfigFromFileAndApply: path - " + _storedFilePath);
                var sw = File.OpenText(_storedFilePath);
                var jsonData = sw.ReadToEnd();
                SendOnRemoteConfigUpdate(jsonData);
            }
            else
            {
                Debug.Log("TTPTestAB::LoadConfigFromFileAndApply: no stored configuration");
            }
        }
        
        private static void SetupConsole()
        {
            if (!_testAbConsole)
            {
                _testAbConsole = Resources.Load<GameObject>("TestABCanvas");
                _testAbConsole = Object.Instantiate(_testAbConsole);
                Object.DontDestroyOnLoad(_testAbConsole);
                
                var rectTransform = _testAbConsole.GetComponent<RectTransform>();
                rectTransform.offsetMin = Vector2.zero;
                rectTransform.offsetMax = Vector2.zero;
                
                var loggerParent = _testAbConsole.transform.Find("TestABParent").gameObject;
                _controller = loggerParent.AddComponent<TTPTestABViewController>();
            }
        }

        private static void LoadConfigFromWeb()
        {
            Debug.Log("TTPTestAB::LoadConfigFromWeb:");
            
            var bindingAttr = BindingFlags.NonPublic | BindingFlags.Static;
            var method = typeof(TTPCore).GetMethod("GetTTPGameObject", bindingAttr);
            if (method != null)
            {
                var gameObject = method.Invoke(null, null) as GameObject;
                if (gameObject != null)
                {
                    var mono = gameObject.GetComponent<MonoBehaviour>();
                    mono.StartCoroutine(LoadJsonFromUrl());
                }
            }
        }
        
        private static IEnumerator LoadJsonFromUrl()
        {
            Debug.Log("TTPTestAB::LoadJsonFromUrl: start");
            
            var loaded = new UnityWebRequest(CONFIG_URL);
            loaded.downloadHandler = new DownloadHandlerBuffer();
            yield return loaded.SendWebRequest();

            var loadedJson = loaded.downloadHandler.text;
            Debug.Log("TTPTestAB::LoadJsonFromUrl: loadedJson - " + loadedJson);
            
            _configDictionary = TTPJson.Deserialize(loadedJson) as Dictionary<string, object>;
            if (_controller)
            {
                _controller.ReloadData();
            }
        }

        private static void SaveConfigToFile(string experimentName, string variantName)
        {
            Debug.Log("TTPTestAB::SaveConfigToFile: experimentName - " + experimentName + ", variantName - " + variantName);
            
            var variantsDict = _configDictionary[experimentName] as Dictionary<string, object>;
            if (variantsDict != null)
            {
                var sw = File.CreateText(_storedFilePath);
                var jsonData = TTPJson.Serialize(variantsDict[variantName]);
                sw.Write(jsonData);
            }
        }

        private static void SendOnRemoteConfigUpdate(string jsonData)
        {
            Debug.Log("TTPTestAB::SendOnRemoteConfigUpdate: jsonData - " + jsonData);
            
            var bindingAttr = BindingFlags.NonPublic | BindingFlags.Static;
            var method = typeof(TTPCore).GetMethod("GetTTPGameObject", bindingAttr);
            if (method != null)
            {
                var gameObject = method.Invoke(null, null) as GameObject;
                if (gameObject != null)
                {
                    var coreDelegate = gameObject.GetComponent<TTPCore.CoreDelegate>(); 
                    coreDelegate.OnRemoteConfigUpdate(jsonData);
                }
            }
        }
    }
}
