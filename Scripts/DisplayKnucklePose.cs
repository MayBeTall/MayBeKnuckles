using UnityEngine;
using MayBeKnuckles;

public class DisplayKnucklePose : MonoBehaviour {
	private TextMesh text;
	public KnucklePoses pose;
	// Use this for initialization
	void Start () {
		text = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		if ( pose.currentPose != null ) {
			text.text = pose.currentPose.name;
		} else {
			text.text = "No pose";
		}
	}
}
