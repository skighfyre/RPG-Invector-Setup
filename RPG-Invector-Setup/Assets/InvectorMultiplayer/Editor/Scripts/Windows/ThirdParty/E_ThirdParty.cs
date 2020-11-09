using Invector.vCharacterController;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace CBGames.Editors
{
    public class E_ThirdParty : EditorWindow
    {
        public static IEnumerator TP_FPC_DownloadFile()
        {
            if (File.Exists(Application.dataPath + "/ThirdParty/FPCameraAddOn_2.51.unitypackage"))
            {
                Debug.Log("File already exists, skipping download.");
                yield return null;
            }
            else
            {
                Debug.Log("Downloading from: https://www.dropbox.com/s/nfygzliyeezjmfr/FPCameraAddOn_2.51.unitypackage?dl=1");
                using (UnityWebRequest www = UnityWebRequest.Get("https://www.dropbox.com/s/nfygzliyeezjmfr/FPCameraAddOn_2.51.unitypackage?dl=1"))
                {
                    yield return www.SendWebRequest();
                    if (www.isNetworkError || www.isHttpError)
                    {
                        Debug.Log(www.error);
                    }
                    else
                    {
                        Debug.Log("Done, saving To: " + Application.dataPath + "/ThirdParty/FPCameraAddOn_2.51.unitypackage");
                        if (!Directory.Exists(Application.dataPath + "/ThirdParty"))
                        {
                            Directory.CreateDirectory(Application.dataPath + "/ThirdParty");
                        }
                        File.WriteAllBytes(Application.dataPath + "/ThirdParty/FPCameraAddOn_2.51.unitypackage", www.downloadHandler.data);
                    }
                }
            }
        }

        [MenuItem("CB Games/Third Party/First Person Camera Add-On/Open Forum Link", false, 0)]
        public static void CB_MENU_TPFPAddOn_ForumLink()
        {
            Application.OpenURL("https://invector.proboards.com/thread/41/free-first-person-camera-add");
        }

        [MenuItem("CB Games/Third Party/First Person Camera Add-On/Download v2.51", false, 1)]
        public static void CB_MENU_TPFPAddOn_DOWNLOAD()
        {
            if (EditorUtility.DisplayDialog("Before importing the \"First Person Camera\" addon...",
                "WARNING: This is a third party add-on not maintained by myself.\n\n" +
                "It is possible that the link could have changed or even the version has been updated. " +
                "For the most accurate up to date package it is recommended to look at the forum links " +
                "to get the most recent download of the package.\n\n" +
                "Would you like to continue and download this .unitypackage?",
                        "Yes", "No"))
            {
                StaticCoroutine.Start(TP_FPC_DownloadFile());
            }
        }

        [MenuItem("CB Games/Third Party/First Person Camera Add-On/Enable FP Add-On v2.51", false, 2)]
        public static void CB_MENU_TPFPAddOn_ENABLE()
        {
            string results = E_Helpers.CommentOutFile("InvectorMultiplayer/Editor/Scripts/Windows/ThirdParty/E_FPAddon.cs", false);
            Debug.Log(results);
            results = E_Helpers.CommentOutFile("InvectorMultiplayer/Scripts/Player/ThirdParty/MP_FPCameraCheck.cs", false);
            Debug.Log(results);
            if (EditorUtility.DisplayDialog("FP Add-On Is ready to be setup!",
                "This has just enabled the setup code, it hasn't done any setup. Click out of " +
                "unity and back into to let the code compile.",
                        "Okay"))
            {}
        }
        [MenuItem("Window/Reset/Disable FP Add-On v2.51", false, 200)]
        public static void CB_MENU_TPFPAddOn_DISABLE()
        {
            string results = E_Helpers.CommentOutFile("InvectorMultiplayer/Editor/Scripts/Windows/ThirdParty/E_FPAddon.cs", true);
            Debug.Log(results);
            results = E_Helpers.CommentOutFile("InvectorMultiplayer/Scripts/Player/ThirdParty/MP_FPCameraCheck.cs", true);
            Debug.Log(results);
        }
    }
}