using UnityEngine;
using Valve.VR;

namespace MayBeKnuckles {
	public class KnuckleClaws : MonoBehaviour {
		[SteamVR_DefaultAction("Squeeze")]
		public SteamVR_Action_Single clawToggle;
		public GameObject clawPrefab; 

		private KnucklePoses poses;
		private SteamVR_Behaviour_Skeleton skeleton;
		private SteamVR_Input_Sources hand;
		private KnuckleClawScaler claws;
		private bool showingClaws = false;
		private bool toggleClawTriggered;

		// Use this for initialization
		void Start () {
			poses = GetComponent<KnucklePoses>();
			skeleton = GetComponent<SteamVR_Behaviour_Skeleton>();
			hand = skeleton.inputSource;
			GameObject _claws = Instantiate( clawPrefab, skeleton.GetBonePosition( SteamVR_Skeleton_JointIndexes.wrist ), skeleton.GetBoneRotation( SteamVR_Skeleton_JointIndexes.wrist) );
			_claws.transform.parent = skeleton.GetBone( SteamVR_Skeleton_JointIndexes.wrist );
			if (hand == SteamVR_Input_Sources.LeftHand) {
				_claws.transform.localScale = new Vector3(1, 1, 1);
			}
			claws = _claws.GetComponent<KnuckleClawScaler>();
		}

		// Update is called once per frame
		void Update () {
			claws.setScale( Mathf.Lerp( claws.getScale(), showingClaws ? 1 : 0, 0.15f ) );

			if ( poses != null ) {
				if ( poses.currentPose != null && poses.currentPose.name == "fist" ) {
					if ( clawToggle.GetAxis( hand ) >= 0.6f && ! toggleClawTriggered ) {
						toggleClaws();
						toggleClawTriggered = true;
					} else {
						if ( clawToggle.GetAxis( hand ) < 0.6f ) {
							toggleClawTriggered = false;
						}
					}
				} else {
					hideClaws();
				}
			}
		}

		public void showClaws() {
			showingClaws = true;
		}

		public void hideClaws() {
			showingClaws = false;
			toggleClawTriggered = false;
		}

		public void toggleClaws() {
			showingClaws = ( ! showingClaws );
		}
	}
}