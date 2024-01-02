using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace CrazyLabsHubs.Editor
{

    public class EvenQuickerCLIKEditor : EditorWindow
    {
        [MenuItem("Even Quicker CLIK/Set Up iOS Build Settings")]
        public static void SetUpIOSBuildSettings()
        {
            PlayerSettings.iOS.targetOSVersionString = "12.0";
        }

        [MenuItem("Even Quicker CLIK/Set Up Android Build Settings")]
        public static void SetUpAndroidBuildSettings()
        {
            //Other settings
            PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel23;
            PlayerSettings.Android.targetSdkVersion = (AndroidSdkVersions)31;


            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
            PlayerSettings.SetManagedStrippingLevel(BuildTargetGroup.Android, 0);
            PlayerSettings.stripEngineCode = false;
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;

#if UNITY_2020_1_OR_NEWER
            PlayerSettings.Android.minifyWithR8 = false;
            PlayerSettings.Android.minifyDebug = false;
#endif
            //Publishing Settings
            //If needed try to set from code Custom Main Gradle Template, Custom Launcher Gradle Template, Custom Base Gradle Template flags
            //and if the CLIK version is less than 3.8.0.0 Custom Main Manifest needs to be checked also
        }

        [MenuItem("Even Quicker CLIK/Add CLIK Init object to this scene")]
        public static void AddCLIKInitObjectToThisScene()
        {
            GameObject go = new GameObject();
            go.name = "QuickerCLIKInit";
            var quickCLICK = go.AddComponent<QuickerCLIKInit>();

            var scene = EditorSceneManager.GetActiveScene();
            EditorSceneManager.SaveScene(scene, scene.path);
        }

        [MenuItem("Even Quicker CLIK/Copy BundleID to clipboard")]
        static void CopyBundleIDToClipboard()
        {
            var bundleId = PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.Android);
            GUIUtility.systemCopyBuffer = bundleId;
        }
    }
}