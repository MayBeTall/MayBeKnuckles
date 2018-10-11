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
			[Range(-0.01f, 1)]
			public float thumb;
			[Range(-0.01f, 1)]
			public float index;
			[Range(-0.01f, 1)]
			public float middle;
			[Range(-0.01f, 1)]
			public float ring;
			[Range(-0.01f, 1)]
			public float pinky;
		}

		public Pose[] poses;
		public Pose currentPose;

		private FingerCurler fingerCurler;

		// Use this for initialization
		void Start () {
			fingerCurler = GetComponent<FingerCurler>();
		}
		
		// Update is called once per frame
		void Update () {
			currentPose = null;

			foreach(Pose pose in poses) {
				bool testIndex = Mathf.Abs(pose.index - fingerCurler.IndexCurl) < 0.20 || pose.index < 0;
				bool testMiddle = Mathf.Abs(pose.middle - fingerCurler.MiddleCurl) < 0.20 || pose.middle < 0;
				bool testRing = Mathf.Abs(pose.ring - fingerCurler.RingCurl) < 0.20 || pose.ring < 0;
				bool testPinky = Mathf.Abs(pose.pinky - fingerCurler.PinkyCurl) < 0.20|| pose.pinky < 0;
				bool testThumb = Mathf.Abs(pose.thumb - fingerCurler.ThumbCurl) < 0.20|| pose.thumb < 0;

				if ( testIndex && testMiddle && testRing && testPinky && testThumb ) {
					currentPose = pose;
					return;
				}
			}
		}
	}
}
