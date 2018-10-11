using UnityEngine;
using Valve.VR;

namespace MayBeKnuckles {
	/**
	 * Defines and tracks different poses the Knuckles controllers can make.
	 */
	public class KnucklePoses : MonoBehaviour {
		[System.Serializable]
		public class Pose {
			public string name;
			public enum thumbControls {Unused, Off, A_Button, B_Button, Track_Button, Thumbstick};
			public thumbControls thumb;
			[Range(-1, 100)]
			public float index;
			[Range(-1, 100)]
			public float middle;
			[Range(-1, 100)]
			public float ring;
			[Range(-1, 100)]
			public float pinky;
		}

		[System.Serializable]
		public class DebugPose {
			public enum thumbControls {Off, A_Button, B_Button, Track_Button, Thumbstick};
			public thumbControls thumb;
			[Range(-1, 100)]
			public float index;
			[Range(-1, 100)]
			public float middle;
			[Range(-1, 100)]
			public float ring;
			[Range(-1, 100)]
			public float pinky;
		}

		public class Finger {
			public SteamVR_Behaviour_Skeleton hand;
			public int jointIndex;
			public float min;
			public float max;
			public float current;
			public float value;

			public Finger(int jointIndex) {
				this.jointIndex = jointIndex;
			}

			public void update() {
				current = hand.skeletonRoot.InverseTransformPoint( hand.GetBonePosition( jointIndex ) ).z;

				if (current > 0) {
					min = Mathf.Min(min, current);
					max = Mathf.Max(max, current);
					value = ((current - min) * 100) / (max - min);
				}

				// Reset min if it bugs out and gets an impossible value.
				if (min == 0) {
					min = 999;
				}
			}
		}
		
		public Finger[] fingers = new Finger[4];

		public SteamVR_Behaviour_Skeleton hand;
		public Pose[] poses;
		public Pose currentPose;
		public DebugPose debugPose;
		
		public Finger index = new Finger(SteamVR_Skeleton_JointIndexes.indexTip);
		public Finger middle = new Finger(SteamVR_Skeleton_JointIndexes.middleTip);
		public Finger ring = new Finger(SteamVR_Skeleton_JointIndexes.ringTip);
		public Finger pinky = new Finger(SteamVR_Skeleton_JointIndexes.pinkyTip);

		private float waitForInit = 1;
		// Use this for initialization
		void Start () {
			SteamVR_Behaviour_Skeleton hand = GetComponent<SteamVR_Behaviour_Skeleton>();
			fingers[0] = index;
			fingers[1] = middle;
			fingers[2] = ring;
			fingers[3] = pinky;

			foreach(Finger finger in fingers ) {
				finger.hand = hand;
			}
		}
		
		// Update is called once per frame
		void Update () {
			currentPose = null;

			waitForInit -= Time.deltaTime;
			if ( waitForInit < 0 ) {
				foreach(Finger finger in fingers ) {
					finger.update();
				}

				foreach(Pose pose in poses) {
					bool testIndex = Mathf.Abs(pose.index - index.value) < 10;
					bool testMiddle = Mathf.Abs(pose.middle - middle.value) < 10;
					bool testRing = Mathf.Abs(pose.ring - ring.value) < 10;
					bool testPinky = Mathf.Abs(pose.pinky - pinky.value) < 10;

					if ( testIndex && testMiddle && testRing && testPinky ) {
						currentPose = pose;
						return;
					}
				}

				debugPose.index = index.value;
				debugPose.middle = middle.value;
				debugPose.ring = ring.value;
				debugPose.pinky = pinky.value;
			}
		}
	}
}
