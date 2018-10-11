using UnityEngine;
using Valve.VR;

public class FingerCurler : MonoBehaviour {
    public SteamVR_Behaviour_Skeleton skeleton;

    public float IndexCurl = 0f;
    public float MiddleCurl = 0f;
    public float RingCurl = 0f;
    public float PinkyCurl = 0f;
    public float ThumbCurl = 0f;

    private float IndexMinDistance = 10000f, IndexMaxDistance = 0f;
    private float MiddleMinDistance = 10000f, MiddleMaxDistance = 0f;
    private float RingMinDistance = 10000f, RingMaxDistance = 0f;
    private float PinkyMinDistance = 10000f, PinkyMaxDistance = 0f;
    private float ThumbMinDistance = 10000f, ThumbMaxDistance = 0f;

    private float IndexAng, MiddleAng, RingAng, PinkyAng, ThumbAng;

    void Start () {
        skeleton.SetRangeOfMotion(Valve.VR.EVRSkeletalMotionRange.WithoutController);
    }
	
	void Update () {
		IndexAng =  skeleton.GetBoneRotation(SteamVR_Skeleton_JointIndexes.indexProximal, true).eulerAngles.z;
		IndexMinDistance = Mathf.Min(IndexMinDistance, IndexAng);
		IndexMaxDistance = Mathf.Max(IndexMaxDistance, IndexAng);
        IndexCurl = Mathf.InverseLerp(IndexMaxDistance, IndexMinDistance, IndexAng);

		MiddleAng = skeleton.GetBoneRotation(SteamVR_Skeleton_JointIndexes.middleProximal, true).eulerAngles.z;
		MiddleMinDistance = Mathf.Min(MiddleMinDistance, MiddleAng);
		MiddleMaxDistance = Mathf.Max(MiddleMaxDistance, MiddleAng);
        MiddleCurl = Mathf.InverseLerp(MiddleMaxDistance, MiddleMinDistance, MiddleAng);

		RingAng = skeleton.GetBoneRotation(SteamVR_Skeleton_JointIndexes.ringProximal, true).eulerAngles.z;
		RingMinDistance = Mathf.Min(RingMinDistance, RingAng);
		RingMaxDistance = Mathf.Max(RingMaxDistance, RingAng);
        RingCurl = Mathf.InverseLerp(RingMaxDistance, RingMinDistance, RingAng);

		PinkyAng = skeleton.GetBoneRotation(SteamVR_Skeleton_JointIndexes.pinkyProximal, true).eulerAngles.z;
		PinkyMinDistance = Mathf.Min(PinkyMinDistance, PinkyAng);
		PinkyMaxDistance = Mathf.Max(PinkyMaxDistance, PinkyAng);
        PinkyCurl = Mathf.InverseLerp(PinkyMaxDistance, PinkyMinDistance, PinkyAng);

		ThumbAng = skeleton.GetBoneRotation(SteamVR_Skeleton_JointIndexes.thumbProximal, true).eulerAngles.z;
		ThumbMinDistance = Mathf.Min(ThumbMinDistance, ThumbAng);
		ThumbMaxDistance = Mathf.Max(ThumbMaxDistance, ThumbAng);
        ThumbCurl = Mathf.InverseLerp(ThumbMaxDistance, ThumbMinDistance, ThumbAng);
    }
}