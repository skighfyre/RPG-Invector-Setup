/*
using Photon.Pun;

namespace Invector.vCharacterController.vActions
{
    public class MP_vLadderAction : vLadderAction
    {
        protected override void ExitLadderInput()
        {
            if (!isUsingLadder) return;
            if (tpInput.cc.baseLayerInfo.IsName("EnterLadderTop") || tpInput.cc.baseLayerInfo.IsName("EnterLadderBottom")) return;

            if (ladderAction == null)
            {
                if (tpInput.cc.IsAnimatorTag("ClimbLadder"))
                {
                    if (slideDownInput.GetButtonDown() && !isExitingLadder)
                    {
                        GetComponent<PhotonView>().RPC("UseRigidBodyGravity", RpcTarget.Others, true);
                        GetComponent<PhotonView>().RPC("SetRigidbodyKinematic", RpcTarget.Others, false);
                        GetComponent<PhotonView>().RPC("CrossFadeInFixedTime", RpcTarget.Others, "Ladder_SlideDown", 0.2f);
                    }

                    if (exitInput.GetButtonDown())
                    {
                        //tpInput.cc.animator.speed = 1; // TO DO IMPLENMET
                        GetComponent<PhotonView>().RPC("UseRigidBodyGravity", RpcTarget.Others, true);
                        GetComponent<PhotonView>().RPC("SetRigidbodyKinematic", RpcTarget.Others, false);
                        GetComponent<PhotonView>().RPC("CrossFadeInFixedTime", RpcTarget.Others, "QuickExitLadder", 0.1f);
                        Invoke("ResetPlayerSettings", .5f);
                    }
                }
            }
            else
            {
                var animationClip = ladderAction.exitAnimation;
                if (animationClip == "ExitLadderBottom")
                {
                    if (exitInput.GetButtonDown() && !triggerExitOnce || (speed <= -0.05f && !triggerExitOnce) || (tpInput.cc.IsAnimatorTag("LadderSlideDown") && ladderAction != null && !triggerExitOnce))
                    {
                        GetComponent<PhotonView>().RPC("UseRigidBodyGravity", RpcTarget.Others, true);
                        GetComponent<PhotonView>().RPC("SetRigidbodyKinematic", RpcTarget.Others, false);
                        GetComponent<PhotonView>().RPC("CrossFadeInFixedTime", RpcTarget.Others, ladderAction.exitAnimation, 0.1f);
                    }
                }
                else if (animationClip == "ExitLadderTop" && tpInput.cc.IsAnimatorTag("ClimbLadder"))
                {
                    if ((speed >= 0.05f) && !triggerExitOnce && !tpInput.cc.animator.IsInTransition(0))
                    {
                        GetComponent<PhotonView>().RPC("UseRigidBodyGravity", RpcTarget.Others, true);
                        GetComponent<PhotonView>().RPC("SetRigidbodyKinematic", RpcTarget.Others, false);
                        GetComponent<PhotonView>().RPC("CrossFadeInFixedTime", RpcTarget.Others, ladderAction.exitAnimation, 0.1f);
                    }
                }
            }
            base.ExitLadderInput();
        }

        protected override void TriggerEnterLadder()
        {
            if (!string.IsNullOrEmpty(ladderAction.playAnimation))
            {
                GetComponent<PhotonView>().RPC("UseRigidBodyGravity", RpcTarget.Others, false);
                GetComponent<PhotonView>().RPC("SetRigidbodyKinematic", RpcTarget.Others, true);
                GetComponent<PhotonView>().RPC("CrossFadeInFixedTime", RpcTarget.Others, ladderAction.playAnimation, 0.25f);
            }
            base.TriggerEnterLadder();
        }

    }
}
*/
