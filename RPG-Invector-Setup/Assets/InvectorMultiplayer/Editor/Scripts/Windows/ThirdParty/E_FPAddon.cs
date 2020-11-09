/*
using Invector.vCamera;
using Invector.vCharacterController;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace CBGames.Editors
{
    public class E_FPAddon : Editor
    {
        [MenuItem("CB Games/Third Party/First Person Camera Add-On/Setup FP Add-On v2.51", false, 3)]
        public static void CB_MENU_TPFPAddOn_SETUP()
        {
            if (EditorUtility.DisplayDialog("Before setting up the \"First Person Camera\" addon...",
                "Make sure to actually install the previously downloaded .unitypackage otherwise this will not work. " +
                "This will attempt to auto setup the FP add-on for you based on the documentation that I read in v2.51. \n\n" +
                "Would you like to continue?",
                        "Yes", "No"))
            {
                if (!Directory.Exists(Application.dataPath + "/FPCameraAddOn"))
                {
                    if (EditorUtility.DisplayDialog("ERROR: Package not installed!",
                    "Looks like you haven't installed that package yet. First download then run the downloaded " +
                    "unitypackage to install it into your project.",
                        "Okay, maybe I should actually read instructions next time..."))
                    { }
                }
                else
                {
                    if (Selection.activeGameObject.GetComponent<vThirdPersonInput>() || Selection.activeGameObject.GetComponent<vThirdPersonController>())
                    {
                        if (!Selection.activeGameObject.GetComponent<MP_HeadTrack>())
                        {
                            Debug.Log("Adding MP_HeadTrack to: " + Selection.activeGameObject);
                            Selection.activeGameObject.AddComponent<MP_HeadTrack>();
                        }
                        
                        if (!Selection.activeGameObject.GetComponent<vFirstPersonCamera>())
                        {
                            Debug.Log("Adding vFirstPersonCamera to: " + Selection.activeGameObject);
                            Selection.activeGameObject.AddComponent<vFirstPersonCamera>();
                        }

                        if (!Selection.activeGameObject.GetComponent<MP_FPCameraCheck>())
                        {
                            Debug.Log("Adding MP_FPCameraCheck to: " + Selection.activeGameObject);
                            Selection.activeGameObject.AddComponent<MP_FPCameraCheck>();
                        }

                        GameObject fp_cam = null;
                        if (!Selection.activeGameObject.GetComponentInChildren<vThirdPersonCamera>(true))
                        {
                            fp_cam = E_Helpers.CreatePrefabFromPath("FPCameraAddon/Prefabs/Cameras/ThirdPersonCamera.prefab");
                        }
                        else
                        {
                            fp_cam = Selection.activeGameObject.GetComponentInChildren<vThirdPersonCamera>(true).gameObject;
                        }
                        
                        Selection.activeGameObject.GetComponent<vFirstPersonCamera>().fpCamera = fp_cam.GetComponentInChildren<Camera>();
                        Selection.activeGameObject.GetComponent<vFirstPersonCamera>().cameraRotationSpeed = 2.4f;
                        Selection.activeGameObject.GetComponent<vFirstPersonCamera>().downAngleLimit = 77.1f;

                        Transform head = Selection.activeGameObject.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head);
                        fp_cam.transform.SetParent(Selection.activeGameObject.transform);
                        fp_cam.transform.position = head.position;
                        fp_cam.transform.position += fp_cam.transform.forward * 0.1f;

                        Selection.activeGameObject.GetComponent<MP_FPCameraCheck>().FPCamera = fp_cam;

                        if (EditorUtility.DisplayDialog("Successfully setup FP Camera",
                            "A new camera prefab has been added to this player with the FP camera. There are othe components " +
                            "and additional options that can be done. Have a look at the documentation by selecting Okay.\n\n" +
                            "IMPORTANT: Remember to save your prefab and disable the scene camera that isn't on your player.",
                                "Okay"))
                        {
                            Selection.activeObject = AssetDatabase.LoadMainAssetAtPath("Assets/FPCameraAddon/Documentation/Doc.txt");
                        }
                    }
                    else
                    {
                        if (EditorUtility.DisplayDialog("ERROR: Invalid Selected Object",
                            "You currently have \"" + Selection.activeGameObject + "\" gameObject selected. " +
                            "This gameobject doesn't appear to be a player controller. Please select a player " +
                            "controller and try again.",
                                "Okay"))
                        { }
                    }
                }
            }
        }
    }
}
*/
