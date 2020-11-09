/*
using Photon.Pun;
using UnityEngine;

public class MP_FPCameraCheck : MonoBehaviour
{
    [Tooltip("The gameobject that holds the FP camera. Don't enable it if you're not the owner player.")]
    public GameObject FPCamera;

    protected virtual void Awake()
    {
        GetComponent<vFirstPersonCamera>().enabled = false;
        FPCamera.SetActive(false);
    }

    protected virtual void Start()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            GetComponent<vFirstPersonCamera>().enabled = true;
            FPCamera.SetActive(true);
        }
    }

}
*/
