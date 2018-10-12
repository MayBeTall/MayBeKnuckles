using UnityEngine;
using MayBeKnuckles;

public class KnucklesTypingCurrentCord : MonoBehaviour {
	public TextMesh Key1;
	public TextMesh Key2;
	public TextMesh Key3;
	public TextMesh Key4;
	public KnuckleTyping typing;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Key1.text = "";
		Key2.text = "";
		Key3.text = "";
		Key4.text = "";
		if ( typing.currentCord != null ) {
			if (typing.currentCord.Length >= 1) {
				string key = typing.currentCord[0].ToString();
				if ( key == " " ) {
					key = "[ ]";
				}

				Key1.text = key;
			}
			if (typing.currentCord.Length >= 2) {
				string key = typing.currentCord[1].ToString();
				if ( key == " " ) {
					key = "[ ]";
				}

				Key2.text = key;
			}
			if (typing.currentCord.Length >= 3) {
				string key = typing.currentCord[2].ToString();
				if ( key == " " ) {
					key = "[ ]";
				}

				Key3.text = key;
			}
			if (typing.currentCord.Length >= 4) {
				string key = typing.currentCord[3].ToString();
				if ( key == " " ) {
					key = "[ ]";
				}

				Key4.text = key;
			}
		}
	}
}
