using UnityEngine;
using Valve.VR;

namespace MayBeKnuckles {
	/*
	 * An example class that combines SteamVR actions
	 * and KnucklePoses.
	 */
	public class KnuckleMagic : MonoBehaviour {
		public KnucklePoses poses;
		private SteamVR_Input_Sources hand;

		[SteamVR_DefaultActionSet("magic")]
        public SteamVR_ActionSet actionSet;

        [SteamVR_DefaultAction("Squeeze")]
        public SteamVR_Action_Single cast;

		public GameObject fingerTipEffectPrefab;
		private GameObject fingerTipEffect;
		private SpiritGunGlow fingerTipGlow;
		public float fingerTipRotationYOffset;

		private Transform fingerTip;

		private float castForce;
		private float spiritGunCooldown;


		// Use this for initialization
		void Start () {
			actionSet.ActivateSecondary();
			hand = GetComponent<SteamVR_Behaviour_Skeleton>().inputSource;
			fingerTip = GetComponent<SteamVR_Behaviour_Skeleton>().indexTip;
			if ( fingerTipEffectPrefab != null ) {
				fingerTipEffect = Instantiate(fingerTipEffectPrefab, fingerTip.position, fingerTip.rotation);
				fingerTipGlow = fingerTipEffect.GetComponent<SpiritGunGlow>();
				fingerTipGlow.target = fingerTip;
				fingerTipGlow.snap.localEulerAngles = new Vector3(fingerTipGlow.snap.localEulerAngles.x, fingerTipRotationYOffset, fingerTipGlow.snap.localEulerAngles.z);
			}
		}
		
		// Update is called once per frame
		void Update () {
			castForce = cast.GetAxis( hand );
			if ( spiritGunCooldown > 0 ) {
				spiritGunCooldown -= Time.deltaTime;
			}
			
			if ( castForce > 0.1f && poses.currentPose != null ) {
				if ( poses.currentPose.name == "finger-gun" && spiritGunCooldown <= 0 ) {
					fingerTipGlow.fadeIn();
					if ( castForce > 0.7f ) {
						fingerTipGlow.fire();
					}
				} else {
					fingerTipGlow.fadeOut();
				}
			} else {
				fingerTipGlow.fadeOut();
			}
		}
	}
}