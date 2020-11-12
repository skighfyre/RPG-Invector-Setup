using CBGames.Core;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace CBGames.UI
{
    [AddComponentMenu("CB GAMES/UI/Menus/Player List/Player List Object")]
    public class SetScoreText : MonoBehaviour
    {
        [Tooltip("Hidden variable. The user id of this player.")]
        [HideInInspector] public string userId = null;
        [Tooltip("The Text component that is responsible for displaying the player's name.")]
        [SerializeField] protected Text playerNameText = null;
        [Tooltip("The Text component that is responsible for displaying if this player is the MasterClient or not.")]
        [SerializeField] protected Text ownerText = null;
        [Tooltip("The Text component that is responsible for displaying if the player is marked as ready or not.")]
   
        protected bool isReady = false;
        protected UICoreLogic logic = null;


        protected virtual void Start()
        {
            logic = NetworkManager.networkManager.GetComponentInChildren<UICoreLogic>();
            if (GetComponent<PhotonView>())
            {
                object[] data = GetComponent<PhotonView>().InstantiationData;
                if (data != null)
                {
                    foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
                    {
                        if (player.UserId == (string)data[0])
                        {
                            SetPlayerContents(player);
                         
                        }
                    }
                    Transform parentToSet = StaticMethods.FindTargetChild((int[])data[1], logic.transform);
                   // transform.SetParent(parentToSet);
                  //  transform.localScale = new Vector3(1, 1, 1);
                  //  transform.position = Vector3.zero;
                }
            }
        }

   

        /// <summary>
        /// Sets all the Text and Image values based on the input `info`
        /// </summary>
        /// <param name="info">PlayerListInfo type, all the data about this player</param>
        public virtual void SetContents(PlayerListInfo info)
        {
            userId = info.userId;
            playerNameText.text = userId.Split(':')[0];
          /*  if (location != null)
            {
                sceneName = "Unknown Location";
                if (info.sceneIndex > 9999 && hideLocationIfNotSet == true)
                {
                    location.gameObject.SetActive(false);
                }
                else
                {
                    location.gameObject.SetActive(true);
                    if (info.sceneIndex == -1)
                    {
                        sceneName = "Lobby";
                    }
                    else if (info.sceneIndex < NetworkManager.networkManager.database.storedScenesData.Count)
                    {
                        sceneName = NetworkManager.networkManager.database.storedScenesData.Find(x => x.index == info.sceneIndex).sceneName;
                    }
                }
                location.text = sceneName;
            }*/
        }

        /// <summary>
        /// Sets the Text and Images based on the input values.
        /// </summary>
        /// <param name="player">Photon.Realtime.Player type, the </param>
        /// <param name="isOwnerText">string type, the string value to display if this is a MasterClient</param>
        /// <param name="nonOwnerText">string type, the string value to display if this is NOT a MasterClient</param>
        public virtual void SetPlayerContents(Photon.Realtime.Player player, string isOwnerText = "Title", string nonOwnerText = "")
        {
            userId = player.UserId;
            if (playerNameText != null)
            {
                playerNameText.text = player.NickName;
            }
            if (ownerText != null)
            {
                ownerText.text = (player.IsMasterClient == true) ? isOwnerText : nonOwnerText;
            }
        
        }

    }
}