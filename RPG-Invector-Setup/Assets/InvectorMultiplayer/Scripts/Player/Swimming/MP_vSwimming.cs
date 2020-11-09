/*
using Invector.vCharacterController;
using Invector.vCharacterController.vActions;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CBGames.Player
{
    [AddComponentMenu("CB GAMES/Player/MP Components/MP vSwimming")]
    [RequireComponent(typeof(SyncPlayer))]
    public class MP_vSwimming : vSwimming
    {
        [Header("UnityEvent. Called when you enter the water. This event is called over the network.")]
        public UnityEvent NetworkOnEnterWater;
        [Header("UnityEvent. Called when you exit the water. This event is called over the network.")]
        public UnityEvent NetworkOnExitWater;
        [Header("UnityEvent. Called when you are above the water. This event is called over the network.")]
        public UnityEvent NetworkOnAboveWater;
        [Header("UnityEvent. Called when you are under the water. This event is called over the network.")]
        public UnityEvent NetworkOnUnderWater;
     
        vThirdPersonInput mp_tpInput = null;
        protected float mp_timer;
        protected float mp_waterRingSpawnFrequency;
        protected float mp_waterHeightLevel;
        protected bool mp_currentlyInWater = false;
        protected bool mp_triggerSwimState;
        protected bool mp_triggerUnderWater;
        protected bool mp_triggerAboveWater;
        protected bool org_Kinematic = false;
        protected bool org_Gravity = false;
        protected bool waterRingMoving = false;
        protected bool lastSendUnderWaterState = false;
        protected bool lastInWaterState = false;
        protected PhotonView pv = null;

        protected bool currentlyUnderWater
        {
            get
            {
                if (mp_tpInput.cc._capsuleCollider.bounds.max.y >= mp_waterHeightLevel + 0.25f)
                {
                    if (pv.IsMine && lastSendUnderWaterState == true)
                    {
                        lastSendUnderWaterState = false;
                        pv.RPC("SetUnderWaterState", RpcTarget.Others, false);
                    }
                    return false;
                }
                else
                {
                    if (pv.IsMine && lastSendUnderWaterState == false)
                    {
                        lastSendUnderWaterState = true;
                        pv.RPC("SetUnderWaterState", RpcTarget.Others, true);
                    }
                    return true;
                }
            }
            private set{}
        }

        protected override void Start()
        {
            pv = GetComponent<PhotonView>();
            if (pv.IsMine)
            {
                mp_tpInput = GetComponentInParent<vThirdPersonInput>();
            }
            base.Start();
        }

        /// <summary>
        /// Will send the `WaterImpactEffect` over the network via the `NetworkWaterImpactEffect` RPC.
        /// </summary>
        /// <param name="other"></param>
        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(waterTag) && pv.IsMine == true)
            {
                mp_currentlyInWater = true;
                mp_waterHeightLevel = other.transform.position.y;
                if (mp_tpInput.cc.verticalVelocity <= velocityToImpact)
                {
                    var newPos = new Vector3(transform.position.x, other.transform.position.y, transform.position.z);
                    pv.RPC(
                        "NetworkWaterImpactEffect", 
                        RpcTarget.Others, 
                        JsonUtility.ToJson(newPos), 
                        JsonUtility.ToJson(mp_tpInput.transform.rotation)
                    );
                }
            }
        }

        /// <summary>
        /// Will send the `WaterDropsEffect` over the network via the `NetworkWaterDropsEffect` RPC.
        /// </summary>
        /// <param name="other"></param>
        protected virtual void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(waterTag) && pv.IsMine == true)
            {
                if (debugMode) Debug.Log("Player left the Water");

                mp_currentlyInWater = false;
                //MP_ExitSwimState();
                if (waterDrops)
                {
                    var newPos = new Vector3(transform.position.x, transform.position.y + waterDropsYOffset, transform.position.z);
                    pv.RPC(
                        "NetworkWaterDropsEffect",
                        RpcTarget.Others,
                        JsonUtility.ToJson(newPos),
                        JsonUtility.ToJson(mp_tpInput.transform.rotation)
                    );
                }
            }
        }

        /// <summary>
        /// Keeps the same logic as the based invector code but also calls the `MP_UnderWaterBehaviour`
        /// and `MP_SwimmingBehaviour` functions.
        /// </summary>
        protected override void UpdateSwimmingBehavior()
        {
            if (pv.IsMine)
            {
                base.UpdateSwimmingBehavior();
                if (lastInWaterState != inTheWater)
                {
                    lastInWaterState = inTheWater;
                    pv.RPC("SetInWaterState", RpcTarget.Others, inTheWater);
                }
                MP_UnderWaterBehaviour();
                MP_SwimmingBehaviour();
            }
        }

        protected virtual void Update()
        {
            if (pv.IsMine) return;
            if (lastInWaterState)
            {
                PlayWaterRings();
            }
        }

        protected virtual void PlayWaterRings()
        {
            if (lastSendUnderWaterState || !mp_triggerAboveWater) return;

            mp_timer += Time.deltaTime;
            if (mp_timer >= mp_waterRingSpawnFrequency)
            {
                Instantiate(
                    waterRingEffect,
                    new Vector3(transform.position.x, mp_waterHeightLevel, transform.position.z),
                    transform.rotation
                ).transform.SetParent(vObjectContainer.root, true);
                mp_timer = 0f;
            }
        }

        /// <summary>
        /// Calls the `MP_EnterSwimState` or `MP_ExitSwimState` based on your position in the water.
        /// </summary>
        protected virtual void MP_SwimmingBehaviour()
        {
            if (mp_tpInput.cc._capsuleCollider.bounds.center.y + heightOffset < mp_waterHeightLevel)
            {
                if (mp_tpInput.cc.currentHealth > 0)
                {
                    if (!mp_triggerSwimState) MP_EnterSwimState();
                }
                else
                    MP_EnterSwimState();
            }
            else
                MP_ExitSwimState();
        }

        /// <summary>
        /// Calls the `NetworkOnEnterWater` UnityEvent for everyone in the photon room. Also
        /// plays the `Swimming` animation for everyone in the photon room.
        /// </summary>
        protected virtual void MP_EnterSwimState()
        {
            mp_triggerSwimState = true;
            pv.RPC("InvokeOnEnterWater", RpcTarget.Others);
            pv.RPC("CrossFadeInFixedTime", RpcTarget.Others, "Swimming", 0.25f);
        }

        /// <summary>
        /// Calls the `NetworkOnExitWater` UnityEvent for everyone in the photon room.
        /// </summary>
        protected virtual void MP_ExitSwimState()
        {
            if (!mp_triggerSwimState) return;
            mp_triggerSwimState = false;
            pv.RPC("InvokeOnExitWater", RpcTarget.Others);
        }

        /// <summary>
        /// Calls the `NetworkOnUnderWater` UnityEvent for everyone in the photon room.
        /// </summary>
        protected virtual void MP_UnderWaterBehaviour()
        {
            if (currentlyUnderWater)
            {
                if (!mp_triggerUnderWater)
                {
                    mp_triggerUnderWater = true;
                    mp_triggerAboveWater = false;
                    pv.RPC("InvokeOnUnderWater", RpcTarget.Others);
                }
            }
            else
            {
                MP_WaterRingEffect();
                if (!mp_triggerAboveWater && mp_triggerSwimState)
                {
                    mp_triggerUnderWater = false;
                    mp_triggerAboveWater = true;
                    pv.RPC("InvokeOnAboveWater", RpcTarget.Others);
                }
            }
        }

        /// <summary>
        /// Plays the `WaterRingEffect` for all networked versions in the photon room.
        /// </summary>
        protected virtual void MP_WaterRingEffect()
        {
            if (mp_tpInput.cc.input != Vector3.zero)
            {
                mp_waterRingSpawnFrequency = waterRingFrequencySwim;
                if (pv.IsMine && waterRingMoving == false)
                {
                    waterRingMoving = true;
                    pv.RPC("SetWaterRingSettings", RpcTarget.Others, true);
                }
            }
            else
            {
                mp_waterRingSpawnFrequency = waterRingFrequencyIdle;
                if (pv.IsMine && waterRingMoving == true)
                {
                    waterRingMoving = false;
                    pv.RPC("SetWaterRingSettings", RpcTarget.Others, false);
                }
            }

            //mp_timer += Time.deltaTime;
            //if (mp_timer >= mp_waterRingSpawnFrequency)
            //{
            //    Instantiate(
            //        GetComponent<MP_vSwimming>().waterRingEffect,
            //        new Vector3(transform.position.x, mp_waterHeightLevel, transform.position.z),
            //        mp_tpInput.transform.rotation
            //    ).transform.SetParent(vObjectContainer.root, true);
            //    mp_timer = 0f;
            //}
        }

        #region RPCs
        [PunRPC]
        protected virtual void SetInWaterState(bool inWaterState)
        {
            lastInWaterState = inWaterState;
        }
        [PunRPC]
        protected virtual void SetUnderWaterState(bool underWater)
        {
            currentlyUnderWater = underWater;
        }
        [PunRPC]
        protected virtual void SetWaterRingSettings(bool moving)
        {
            waterRingMoving = moving;
            if (moving)
            {
                mp_waterRingSpawnFrequency = waterRingFrequencySwim;
            }
            else
            {
                mp_waterRingSpawnFrequency = waterRingFrequencyIdle;
            }
        }
        [PunRPC]
        protected virtual void InvokeOnAboveWater()
        {
            mp_triggerAboveWater = true;
            GetComponent<MP_vSwimming>().NetworkOnAboveWater.Invoke();
        }
        [PunRPC]
        protected virtual void InvokeOnUnderWater()
        {
            mp_triggerAboveWater = false;
            GetComponent<MP_vSwimming>().NetworkOnUnderWater.Invoke();
        }
        [PunRPC]
        protected virtual void NetworkWaterImpactEffect(string position, string rotation)
        {
            Instantiate(
                GetComponent<MP_vSwimming>().impactEffect,
                JsonUtility.FromJson<Vector3>(position),
                JsonUtility.FromJson<Quaternion>(rotation)
            );
        }
        [PunRPC]
        protected virtual void NetworkWaterDropsEffect(string position, string rotation)
        {
            GameObject myWaterDrops = Instantiate(
                GetComponent<MP_vSwimming>().waterDrops,
                JsonUtility.FromJson<Vector3>(position),
                JsonUtility.FromJson<Quaternion>(rotation)
            ) as GameObject;
            myWaterDrops.transform.parent = transform;
        }
        [PunRPC]
        protected virtual void InvokeOnEnterWater()
        {
            if (GetComponent<Rigidbody>())
            {
                org_Kinematic = GetComponent<Rigidbody>().isKinematic;
                org_Gravity = GetComponent<Rigidbody>().useGravity;
                GetComponent<Rigidbody>().isKinematic = true;
                GetComponent<Rigidbody>().useGravity = false;
            }
            GetComponent<MP_vSwimming>().NetworkOnEnterWater.Invoke();
        }
        [PunRPC]
        protected virtual void InvokeOnExitWater()
        {
            if (GetComponent<Rigidbody>())
            {
                GetComponent<Rigidbody>().isKinematic = org_Kinematic;
                GetComponent<Rigidbody>().useGravity = org_Gravity;
            }
            GetComponent<MP_vSwimming>().NetworkOnExitWater.Invoke();
        }
        #endregion
    }
}
*/
