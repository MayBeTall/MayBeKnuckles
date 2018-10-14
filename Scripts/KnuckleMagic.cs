using UnityEngine;
using Valve.VR;

namespace MayBeKnuckles {
	/*
	 * An example class that combines SteamVR actions
	 * and KnucklePoses.
	 */
	public class KnuckleMagic : MonoBehaviour {
		private KnucklePoses poses;
		private SteamVR_Behaviour_Skeleton skeleton;
		private SteamVR_Input_Sources hand;

		public SteamVR_ActionSet actionSet;

        [SteamVR_DefaultAction("Squeeze")]
        public SteamVR_Action_Single cast;

		[SteamVR_DefaultAction("castLightning")]
        public SteamVR_Action_Boolean castLightning;

		public GameObject spiritGunPrefab;
		private SpiritGunGlow spiritGunEffect;

		public GameObject shockPrefab;
		private GameObject[] shockObjects = new GameObject[5];

		private float castForce;
		private float spiritGunCooldown;


		// Use this for initialization
		void Start () {
			actionSet.ActivateSecondary();
			poses = GetComponent<KnucklePoses>();
			skeleton = GetComponent<SteamVR_Behaviour_Skeleton>();
			hand = skeleton.inputSource;
			if ( spiritGunPrefab != null ) {
				GameObject spiritGun = Instantiate(spiritGunPrefab, skeleton.GetBonePosition( SteamVR_Skeleton_JointIndexes.indexTip ),  skeleton.GetBoneRotation( SteamVR_Skeleton_JointIndexes.indexTip ) );
				spiritGunEffect = spiritGun.GetComponent<SpiritGunGlow>();
				spiritGunEffect.transform.parent = skeleton.GetBone( SteamVR_Skeleton_JointIndexes.indexTip );
			}
			if ( shockPrefab != null ) {
				shockObjects[0] = Instantiate(shockPrefab, skeleton.GetBonePosition( SteamVR_Skeleton_JointIndexes.indexTip ),  skeleton.GetBoneRotation( SteamVR_Skeleton_JointIndexes.indexTip ) );
				shockObjects[0].transform.parent =  skeleton.GetBone( SteamVR_Skeleton_JointIndexes.indexTip );
				shockObjects[0].SetActive(false);

				shockObjects[1] = Instantiate(shockPrefab, skeleton.GetBonePosition( SteamVR_Skeleton_JointIndexes.middleTip ),  skeleton.GetBoneRotation( SteamVR_Skeleton_JointIndexes.middleTip ) );
				shockObjects[1].transform.parent =  skeleton.GetBone( SteamVR_Skeleton_JointIndexes.middleTip );
				shockObjects[1].SetActive(false);

				shockObjects[2] = Instantiate(shockPrefab, skeleton.GetBonePosition( SteamVR_Skeleton_JointIndexes.ringTip ),  skeleton.GetBoneRotation( SteamVR_Skeleton_JointIndexes.ringTip ) );
				shockObjects[2].transform.parent =  skeleton.GetBone( SteamVR_Skeleton_JointIndexes.ringTip );
				shockObjects[2].SetActive(false);

				shockObjects[3] = Instantiate(shockPrefab, skeleton.GetBonePosition( SteamVR_Skeleton_JointIndexes.pinkyTip ),  skeleton.GetBoneRotation( SteamVR_Skeleton_JointIndexes.pinkyTip ) );
				shockObjects[3].transform.parent =  skeleton.GetBone( SteamVR_Skeleton_JointIndexes.pinkyTip );
				shockObjects[3].SetActive(false);

				shockObjects[4] = Instantiate(shockPrefab, skeleton.GetBonePosition( SteamVR_Skeleton_JointIndexes.thumbTip ),  skeleton.GetBoneRotation( SteamVR_Skeleton_JointIndexes.thumbTip ) );
				shockObjects[4].transform.parent =  skeleton.GetBone( SteamVR_Skeleton_JointIndexes.thumbTip );
				shockObjects[4].SetActive(false);

				if (hand == SteamVR_Input_Sources.LeftHand) {
					foreach ( GameObject shockObject in shockObjects ) {
						shockObject.transform.localScale = new Vector3(1, 1, 1);
					}
				}
			}
		}

		// Update is called once per frame
		void Update () {
			castForce = cast.GetAxis( hand );
			if ( spiritGunCooldown > 0 ) {
				spiritGunCooldown -= Time.deltaTime;
			}
			
			if ( castForce > 0.1f && poses.currentPose != null ) {
				if ( poses.currentPose.name == "finger-gun" && spiritGunCooldown <= 0 && spiritGunEffect != null ) {
					spiritGunEffect.fadeIn();
					if ( castForce > 0.7f ) {
						spiritGunEffect.fire();
					}
				} else {
					spiritGunEffect.fadeOut();
				}
			} else {
				spiritGunEffect.fadeOut();
			}

			if ( poses.currentPose != null && poses.currentPose.name == "shock" && shockObjects[0] != null && castLightning.GetState( hand ) ) {
				if ( ! shockObjects[0].activeSelf ) {
					foreach ( GameObject shockObject in shockObjects ) {
						shockObject.SetActive(true);
					}
				}
			} else {
				if ( shockObjects[0].activeSelf ) {
					foreach ( GameObject shockObject in shockObjects ) {
						shockObject.SetActive(false);
					}
				}
			}
		}
	}
}