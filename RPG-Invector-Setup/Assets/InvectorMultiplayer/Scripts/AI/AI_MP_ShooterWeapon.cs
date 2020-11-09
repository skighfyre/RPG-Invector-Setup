/*
using CBGames.Objects;
using Invector.vCharacterController.AI;
using Invector.vShooter;
using Photon.Pun;
using UnityEngine;

namespace CBGames.AI
{
    [AddComponentMenu("CB GAMES/AI/MP Components/MP Shooter Weapon")]
    public class AI_MP_ShooterWeapon : MP_BaseShooterWeapon
    {
        protected MP_vAIShooterManager ai_shooterManager = null;

        /// <summary>
        /// Find the parent MP_vAIShooterManager component. Also makes sure
        /// this weapon has a valid vShooterWeapon component. Finally, makes 
        /// sure the root transform has a photon view.
        /// </summary>
        protected override void Start()
        {
            ai_shooterManager = transform.GetComponentInParent<MP_vAIShooterManager>();
            base.Start();
        }

        #region Sends
        /// <summary>
        /// Send the 'Shoot' trigger over the network and tells the other network
        /// versions of this object to play their weapon fire function.
        /// </summary>
        public virtual void SendNetworkShot()
        {
            if (transform.GetComponentInParent<PhotonView>().IsMine == false) return;
            if (weapon.ammo > 0)
            {
                if (weapon.transform.GetComponent<vBowControl>())
                {
                    view.RPC("SetTriggers", RpcTarget.Others, new string[1] { "Shoot" });
                }

                view.RPC("vAIShooterManager_Shoot", RpcTarget.Others, (object)childTree, ai_shooterManager.lastAimPos);
            }
        }

        public override void SendNetworkReload()
        {
            ViewCheck();
            ChildTreeCheck();
            if (view.IsMine == false) return;
            view.RPC("vAIShooterManager_ShooterWeaponReload", RpcTarget.Others, childTree);
        }
        public override void SendNetworkOnFinishReload()
        {
            ViewCheck();
            ChildTreeCheck();
            if (view.IsMine == false) return;
            view.RPC("vAIShooterManager_ShooterWeaponOnFinishReload", RpcTarget.Others, childTree);
        }
        public override void SendNetworkOnFullPower()
        {
            ViewCheck();
            ChildTreeCheck();
            if (view.IsMine == false) return;
            view.RPC("vAIShooterManager_ShooterWeaponOnFullPower", RpcTarget.Others, childTree);
        }
        public override void SendNetworkOnFinishAmmo()
        {
            ViewCheck();
            ChildTreeCheck();
            if (view.IsMine == false) return;
            if (weapon.onFinishAmmo.GetPersistentEventCount() > 0)
            {
                view.RPC("vAIShooterManager_ShooterWeaponOnFinishAmmo", RpcTarget.Others, childTree);
            }
        }
        public override void SendOnEnableAim()
        {
            ViewCheck();
            ChildTreeCheck();
            if (view.IsMine == false) return;
            view.RPC("vAIShooterManager_ShooterWeaponOnEnableAim", RpcTarget.Others, childTree);
        }
        public override void SendOnDisableAim()
        {
            ViewCheck();
            ChildTreeCheck();
            if (view.IsMine == false) return;
            view.RPC("vAIShooterManager_ShooterWeaponOnDisableAim", RpcTarget.Others, childTree);
        }
        public override void SendOnChangerPowerCharger(float amount)
        {
            ViewCheck();
            ChildTreeCheck();
            if (view.IsMine == false) return;
            view.RPC("vAIShooterManager_ShooterWeaponOnChangerPowerCharger", RpcTarget.Others, childTree, amount);
        }
        #endregion

        #region Receives
        public override void RecieveNetworkShot(Vector3 aimPos)
        {
            ViewCheck();
            if (view.IsMine == true) return;
            WeaponCheck();
            weapon.isInfinityAmmo = true;
            weapon.AddAmmo(1);
            weapon.Shoot(aimPos, transform);
        }
        public override void RecieveNetworkShot()
        {
            ViewCheck();
            if (view.IsMine == true) return;
            WeaponCheck();
            weapon.isInfinityAmmo = true;
            weapon.AddAmmo(1);
            weapon.Shoot(transform);
        }
        #endregion
    }
}
*/
