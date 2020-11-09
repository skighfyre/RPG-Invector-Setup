using Photon.Pun;
using UnityEngine;


namespace Invector.vCharacterController
{
    /// <summary>
    /// This makes this component not responde if you're not the owner of this component.
    /// This component will class with the original vThirdPersonInput component. So you 
    /// need to completely remove that component from the character
    /// </summary>
    [DisallowMultipleComponent]
    public class MP_vThirdPersonInput : vThirdPersonInput
    {
        PhotonView pv = null;
        protected virtual void Awake()
        {
            pv = GetComponent<PhotonView>();
        }

        protected override void Start()
        {
            if (pv.IsMine)
            {
                base.Start();
            }
        }

        protected override void LateUpdate()
        {
            if (pv.IsMine)
            {
                base.LateUpdate();
            }
        }

        protected override void FixedUpdate()
        {
            if (pv.IsMine)
            {
                base.FixedUpdate();
            }
        }

        protected override void Update()
        {
            if (pv.IsMine)
            {
                base.Update();
            }
        }

        //public override void OnAnimatorMove()
        //{
        //    if (pv.IsMine)
        //    {
        //        base.OnAnimatorMove();
        //    }
        //}
    }
}