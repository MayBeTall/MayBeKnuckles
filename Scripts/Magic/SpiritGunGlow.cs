using UnityEngine;

public class SpiritGunGlow : MonoBehaviour {
	private float scale = 0.0f;
	private bool fadingOut = false;
	public ParticleSystem glowParticles;
	public ParticleSystem blastParticles;
	// Use this for initialization
	void Start () {
		scale = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if ( fadingOut ) {
			if ( glowParticles.isEmitting ) {
				glowParticles.Stop();
			}
			if ( scale > 0 ) {
				scale -= 1f * Time.deltaTime;
			}
		} else {
			if ( ! glowParticles.isEmitting ) {
				glowParticles.Play();
			}
			if ( scale < 1 ) {
				scale += 1f * Time.deltaTime;
			}
		}

		transform.localScale = new Vector3(scale, scale, scale);
	}

	public void fadeOut() {
		fadingOut = true;
	}
	public void fadeIn() {
		fadingOut = false;
	}
	public void fire() {
		if (scale >= 0.9) {
			blastParticles.Emit(1);
			scale = 0;
		}
	}
}
