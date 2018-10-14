using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnuckleClawScaler : MonoBehaviour {
	public Transform Claw1, Claw2, Claw3;

	private float scale;

	public void setScale(float scale) {
		Claw1.localScale = new Vector3( 1, 1, scale );
		Claw2.localScale = new Vector3( 1, 1, scale );
		Claw3.localScale = new Vector3( 1, 1, scale );
		this.scale = scale;
	}

	public float getScale() {
		return scale;
	}
}
