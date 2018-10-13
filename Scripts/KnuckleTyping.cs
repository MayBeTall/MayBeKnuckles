using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace MayBeKnuckles {
	/* 
	 * We're going to try and emulate T9 typing.
	 * By combining both hands, we should only need 2 gestures and some thumb input.
	*/
	public class KnuckleTyping : MonoBehaviour {
		public FingerCurler leftCurl;
		public FingerCurler rightCurl;

		public TextMesh output;

		public char typedKey;
		public char lastTypedKey;

		private Cord currentCord;

		private KnuckleTyping instance;

		// Left pinky to right pinky
		private bool[] fingers = new bool[8];

		[SteamVR_DefaultActionSet("typing")]
        public SteamVR_ActionSet actionSet;

        [SteamVR_DefaultAction("Key1", "typing")]
        public SteamVR_Action_Boolean Key1;
		[SteamVR_DefaultAction("Key1Touch", "typing")]
        public SteamVR_Action_Boolean Key1Touch;

		[SteamVR_DefaultAction("Key2", "typing")]
        public SteamVR_Action_Boolean Key2;
		[SteamVR_DefaultAction("Key2Touch", "typing")]
        public SteamVR_Action_Boolean Key2Touch;

		private bool leftKey1;
		private bool leftKey1Touch;
		private bool leftKey1Down;
		private bool leftKey1Shift;
		private bool leftKey2;
		private bool leftKey2Touch;
		private bool leftKey2Down;
		private bool leftKey2Shift;

		private bool rightKey1;
		private bool rightKey1Touch;
		private bool rightKey1Down;
		private bool rightKey1Shift;
		private bool rightKey2;
		private bool rightKey2Touch;
		private bool rightKey2Down;
		private bool rightKey2Shift;

		public class Key {
			char upper, lower;
			public Key(string key) {
				if ( key.Length == 1 ) {
					this.upper = key.ToUpperInvariant()[0];
					this.lower = key.ToLowerInvariant()[0];
				}
				if ( key.Length == 2 ) {
					this.lower = key[0];
					this.upper = key[1];
				}
			}
			public Key(char lower, char upper) {
				this.upper = upper;
				this.lower = lower;
			}

			public char getChar(bool uppercase = false) {
				if (uppercase){
					return getUpper();
				} else {
					return getLower();
				}
			}

			public char getUpper() {
				return upper;
			}

			public char getLower() {
				return lower;
			}
		}

		public Dictionary<Gestures, Cord> Cords = new Dictionary<Gestures, Cord>();
		public class Cord {
			Gestures gesture;
			Key[] keys = new Key[4];
			public Cord(Key key1 = null, Key key2 = null, Key key3 = null, Key key4 = null) {
				this.keys[0] = key1;
				this.keys[1] = key2;
				this.keys[2] = key3;
				this.keys[3] = key4;
			}

			public Cord(string key1 = null, string key2 = null, string key3 = null, string key4 = null) {
				this.keys[0] = new Key(key1);
				this.keys[1] = new Key(key2);
				this.keys[2] = new Key(key3);
				this.keys[3] = new Key(key4);
			}

			public Cord(string keys) {
				if ( keys.Length == 4 ) {
					this.keys[0] = new Key( keys.Substring(0, 1) );
					this.keys[1] = new Key( keys.Substring(1, 1) );
					this.keys[2] = new Key( keys.Substring(2, 1) );
					this.keys[3] = new Key( keys.Substring(3, 1) );
				}
				if ( keys.Length == 8 ) {
					this.keys[0] = new Key( keys.Substring(0, 2) );
					this.keys[1] = new Key( keys.Substring(2, 2) );
					this.keys[2] = new Key( keys.Substring(4, 2) );
					this.keys[3] = new Key( keys.Substring(6, 2) );
				}
			}

			public char getChar(int keyIndex, bool uppercase = false) {
				if ( keys[keyIndex] != null ) {
					return keys[keyIndex].getChar(uppercase);
				} else {
					return '\0'; // Return empty char
				}
			}
			public Key getKey(int keyIndex) {
				return keys[keyIndex];
			}
		}

		// All the gestures this system tracks
		public enum Gestures {
			XXXX_XXXX,
			XXXI_IXXX,
			XXII_IIXX,
			XIII_IIIX,
			IIII_IIII,
			XXXI_XXXX,
			XXII_XXXX,
			XIII_XXXX,
			IIII_XXXX,
			XXXX_IXXX,
			XXXX_IIXX,
			XXXX_IIIX,
			XXXX_IIII,
			XXII_IXXX,
			XIII_IXXX,
			IIII_IXXX,
			XIII_IIXX,
			IIII_IIXX,
			IIII_IIIX,
			XXXI_IIXX,
			XXXI_IIIX,
			XXXI_IIII,
			XXII_IIIX,
			XXII_IIII,
			XIII_IIII
		};

		private Gestures currentGesture;

		// Use this for initialization
		void Start () {
			actionSet.ActivateSecondary();
			Cords.Add( Gestures.XXXX_XXXX, new Cord("ABCD") );
			Cords.Add( Gestures.XXXI_IXXX, new Cord("EFGH") );
			Cords.Add( Gestures.XXII_IIXX, new Cord("HIJK") );
			Cords.Add( Gestures.XIII_IIIX, new Cord("LMNO") );
			Cords.Add( Gestures.IIII_IIII, new Cord("PQRS") );
			Cords.Add( Gestures.XXXI_XXXX, new Cord("TUVW") );
			Cords.Add( Gestures.XXXX_IXXX, new Cord("YXYZ") );
		}
		
		// Update is called once per frame
		void Update () {
			updateTyping();
			updateFingers();
			updateGesture();
			UpdateCord();
		}

		private void updateFingers() {
			fingers[0] = leftCurl.PinkyCurl < 0.6f;
			fingers[1] = leftCurl.RingCurl < 0.6f;
			fingers[2] = leftCurl.MiddleCurl < 0.6f;
			fingers[3] = leftCurl.IndexCurl < 0.6f;
			fingers[4] = rightCurl.IndexCurl < 0.6f;
			fingers[5] = rightCurl.MiddleCurl < 0.6f;
			fingers[6] = rightCurl.RingCurl < 0.6f;
			fingers[7] = rightCurl.PinkyCurl < 0.6f;
		}

		private void updateGesture() {
			string gesture = "";
			gesture += fingers[0] ? "I" : "X";
			gesture += fingers[1] ? "I" : "X";
			gesture += fingers[2] ? "I" : "X";
			gesture += fingers[3] ? "I" : "X";
			gesture += "_";
			gesture += fingers[4] ? "I" : "X";
			gesture += fingers[5] ? "I" : "X";
			gesture += fingers[6] ? "I" : "X";
			gesture += fingers[7] ? "I" : "X";

			TryParseEnum<Gestures>(gesture, out currentGesture);
		}

		private void UpdateCord() {
			Cord cord;
			if ( Cords.TryGetValue(currentGesture, out cord) ) {
				currentCord = cord;
			} else {
				currentCord = null;
			}
		}

		public Gestures getGesture() {
			return currentGesture;
		}

		public Cord getCord() {
			return currentCord;
		}

		private void updateTyping() {
			leftKey1Down =  Key1.GetState(SteamVR_Input_Sources.LeftHand);
			leftKey1Touch =  Key1Touch.GetState(SteamVR_Input_Sources.LeftHand);
			leftKey1 = leftKey1 ?  leftKey1 : leftKey1Down;

			if ( leftKey1Down && ( Time.time - Key1.GetTimeLastChanged(SteamVR_Input_Sources.LeftHand) ) > 0.3f ) {
				leftKey1Shift = leftKey1Shift ? leftKey1Shift : Key1.GetState(SteamVR_Input_Sources.LeftHand);
			}

			if ( ! leftKey1Down && leftKey1 ) {
				typedKey = currentCord.getChar(0, leftKey1Shift);
				leftKey1Shift = false;
				leftKey1 = false;
			}

			leftKey2Down =  Key2.GetState(SteamVR_Input_Sources.LeftHand);
			leftKey2Touch =  Key2Touch.GetState(SteamVR_Input_Sources.LeftHand);
			leftKey2 = leftKey2 ?  leftKey2 : leftKey2Down;

			if ( leftKey2Down && ( Time.time - Key2.GetTimeLastChanged(SteamVR_Input_Sources.LeftHand) ) > 0.3f ) {
				leftKey2Shift = leftKey2Shift ? leftKey2Shift : Key2.GetState(SteamVR_Input_Sources.LeftHand);
			}

			if ( ! leftKey2Down && leftKey2 ) {
				typedKey = currentCord.getChar(1, leftKey2Shift);
				leftKey2Shift = false;
				leftKey2 = false;
			}

			rightKey1Down =  Key1.GetState(SteamVR_Input_Sources.RightHand);
			rightKey1Touch =  Key1Touch.GetState(SteamVR_Input_Sources.RightHand);
			rightKey1 = rightKey1 ?  rightKey1 : rightKey1Down;

			if ( rightKey1Down && ( Time.time - Key1.GetTimeLastChanged(SteamVR_Input_Sources.RightHand) ) > 0.3f ) {
				rightKey1Shift = rightKey1Shift ? rightKey1Shift : Key1.GetState(SteamVR_Input_Sources.RightHand);
			}

			if ( ! rightKey1Down && rightKey1 ) {
				typedKey = currentCord.getChar(2, rightKey1Shift);
				rightKey1Shift = false;
				rightKey1 = false;
			}

			rightKey2Down =  Key2.GetState(SteamVR_Input_Sources.RightHand);
			rightKey2Touch =  Key2Touch.GetState(SteamVR_Input_Sources.RightHand);
			rightKey2 = rightKey2 ?  rightKey2 : rightKey2Down;

			if ( rightKey2Down && ( Time.time - Key2.GetTimeLastChanged(SteamVR_Input_Sources.RightHand) ) > 0.3f ) {
				rightKey2Shift = rightKey2Shift ? rightKey2Shift : Key2.GetState(SteamVR_Input_Sources.RightHand);
			}

			if ( ! rightKey2Down && rightKey2 ) {
				typedKey = currentCord.getChar(3, rightKey2Shift);
				rightKey2Shift = false;
				rightKey2 = false;
			}

			if ( typedKey != '\0' ) {
				lastTypedKey = typedKey;
				output.text += typedKey;
			}

			typedKey = '\0';
		}

		public bool getKeyTouch(int key) {
			if ( key == 0 ) {
				return leftKey1Touch;
			}
			if ( key == 1 ) {
				return leftKey2Touch;
			}
			if ( key == 2 ) {
				return rightKey1Touch;
			}
			if ( key == 3 ) {
				return rightKey2Touch;
			}
			return false;
		}

		public bool getKeyDown(int key) {
			if ( key == 0 ) {
				return leftKey1Down;
			}
			if ( key == 1 ) {
				return leftKey2Down;
			}
			if ( key == 2 ) {
				return rightKey1Down;
			}
			if ( key == 3 ) {
				return rightKey2Down;
			}
			return false;
		}

		public bool getKeyShift(int key) {
			if ( key == 0 ) {
				return leftKey1Shift;
			}
			if ( key == 1 ) {
				return leftKey2Shift;
			}
			if ( key == 2 ) {
				return rightKey1Shift;
			}
			if ( key == 3 ) {
				return rightKey2Shift;
			}
			return false;
		}

		public static bool TryParseEnum<TEnum>(string aName, out TEnum aValue) where TEnum : struct {
			try {
				aValue = (TEnum)System.Enum.Parse(typeof(TEnum), aName);
				return true;
			} catch {
				aValue = default(TEnum);
				return false;
			}
		}
	}
}