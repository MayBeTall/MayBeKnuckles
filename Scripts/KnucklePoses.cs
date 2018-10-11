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
			[Space(10)]
			[Range(0.0f, 1.0f)]
			public float thumbMin = 0.0f;
			[Range(0.0f, 1.0f)]
			public float thumbMax = 1.0f;
			[Space(10)]
			[Range(0.0f, 1.0f)]
			public float indexMin = 0.0f;
			[Range(0.0f, 1.0f)]
			public float indexMax = 1.0f;
			[Space(10)]
			[Range(0.0f, 1.0f)]
			public float middleMin = 0.0f;
			[Range(0.0f, 1.0f)]
			public float middleMax = 1.0f;
			[Space(10)]
			[Range(0.0f, 1.0f)]
			public float ringMin = 0.0f;
			[Range(0.0f, 1.0f)]
			public float ringMax = 1.0f;
			[Space(10)]
			[Range(0.0f, 1.0f)]
			public float pinkyMin = 0.0f;
			[Range(0.0f, 1.0f)]
			public float pinkyMax = 1.0f;
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
				bool testThumb = fingerCurler.ThumbCurl >= pose.thumbMin && fingerCurler.ThumbCurl <= pose.thumbMax;
				bool testIndex = fingerCurler.IndexCurl >= pose.indexMin && fingerCurler.IndexCurl <= pose.indexMax;
				bool testMiddle = fingerCurler.MiddleCurl >= pose.middleMin && fingerCurler.MiddleCurl <= pose.middleMax;
				bool testRing = fingerCurler.RingCurl >= pose.ringMin && fingerCurler.RingCurl <= pose.ringMax;
				bool testPinky = fingerCurler.PinkyCurl >= pose.pinkyMin && fingerCurler.PinkyCurl <= pose.pinkyMax;

				if ( testThumb && testIndex && testMiddle && testRing && testPinky ) {
					currentPose = pose;
					return;
				}
			}
		}
	}
}
