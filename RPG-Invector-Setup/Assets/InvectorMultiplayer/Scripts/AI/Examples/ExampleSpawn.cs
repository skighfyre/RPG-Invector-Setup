using CBGames.Core;
using Photon.Pun;
using System.Collections;
using UnityEngine;

public class ExampleSpawn : MonoBehaviour
{
    [Tooltip("The prefab that exist in your `Resources` folder that you want to spawn.")]
    [SerializeField] protected GameObject prefab;

    [Tooltip("The location you want to spawn this prefab.")]
    [SerializeField] protected Transform atLocation;

    void Start()
    {
        StartCoroutine(SpawnPrefab(prefab.name));
    }

    // This will wait until you're successfully connected to photon and in a room.
    // Then IF you're that owner of the room you will initiate a network spawn of the 
    // desired prefab across the network at the specified `atLocation`.
    IEnumerator SpawnPrefab(string prefabName)
    {
        yield return new WaitUntil(() => PhotonNetwork.IsConnected && PhotonNetwork.InRoom);
        if (PhotonNetwork.IsMasterClient)
        {
            NetworkManager.networkManager.NetworkInstantiatePersistantPrefab(prefab.name, atLocation.position, atLocation.rotation, 0);
        }
    }
}
