using UnityEngine;
using MayBeKnuckles;

public class KnucklesTypingCurrentCord : MonoBehaviour {
	public TextMesh Key1;
	public TextMesh Key2;
	public TextMesh Key3;
	public TextMesh Key4;
	public KnuckleTyping typing;
	public Color color, touchColor, pressColor, shiftColor;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Key1.text = "";
		Key2.text = "";
		Key3.text = "";
		Key4.text = "";
		if ( typing.getCord() != null ) {
			Key1.text = typing.getCord().getChar(0, true).ToString() + typing.getCord().getChar(0).ToString();
			Key2.text = typing.getCord().getChar(1, true).ToString() + typing.getCord().getChar(1).ToString();
			Key3.text = typing.getCord().getChar(2, true).ToString() + typing.getCord().getChar(2).ToString();
			Key4.text = typing.getCord().getChar(3, true).ToString() + typing.getCord().getChar(3).ToString();

			Key1.color = color;
			if ( typing.getKeyTouch(0) ) {
				Key1.color = touchColor;
				if ( typing.getKeyDown(0) ) {
					Key1.color = pressColor;
					if ( typing.getKeyShift(0) ) {
						Key1.color = shiftColor;
					}
				}
			}

			Key2.color = color;
			if ( typing.getKeyTouch(1) ) {
				Key2.color = touchColor;
				if ( typing.getKeyDown(1) ) {
					Key2.color = pressColor;
					if ( typing.getKeyShift(1) ) {
						Key2.color = shiftColor;
					}
				}
			}

			Key3.color = color;
			if ( typing.getKeyTouch(2) ) {
				Key3.color = touchColor;
				if ( typing.getKeyDown(2) ) {
					Key3.color = pressColor;
					if ( typing.getKeyShift(2) ) {
						Key3.color = shiftColor;
					}
				}
			}

			Key4.color = color;
			if ( typing.getKeyTouch(3) ) {
				Key4.color = touchColor;
				if ( typing.getKeyDown(3) ) {
					Key4.color = pressColor;
					if ( typing.getKeyShift(3) ) {
						Key4.color = shiftColor;
					}
				}
			}
		}
	}
}
