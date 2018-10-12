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

		private string[] keyMap;
		private string[] keyMapSpecial;

		public char typedKey;
		public char lastTypedKey;

		public string currentCord;

		public bool leftIndex, leftMiddle, rightIndex, rightMiddle;

		[SteamVR_DefaultActionSet("typing")]
        public SteamVR_ActionSet actionSet;

        [SteamVR_DefaultAction("Key1", "typing")]
        public SteamVR_Action_Boolean Key1;

		[SteamVR_DefaultAction("Key2", "typing")]
        public SteamVR_Action_Boolean Key2;

		public bool leftKey1;
		public bool leftKey1Shift;
		public bool leftKey2;
		public bool leftKey2Shift;

		public bool rightKey1;
		public bool rightKey1Shift;
		public bool rightKey2;
		public bool rightKey2Shift;

		// Use this for initialization
		void Start () {
			keyMap = new string[] {
				"ABCD",
				"EFGH",
				"IJKL",
				"MNOP",
				"QRST",
				"UVWX",
				"YZ"
			};
			keyMapSpecial = new string[] {
				" ,!?"
			};
			actionSet.ActivateSecondary();
		}
		
		// Update is called once per frame
		void Update () {
			if ( currentCord != null ) {
				bool leftKey1Down =  Key1.GetState(SteamVR_Input_Sources.LeftHand);
				leftKey1 = leftKey1 ?  leftKey1 : Key1.GetState(SteamVR_Input_Sources.LeftHand);

				if ( leftKey1Down && ( Time.time - Key1.GetTimeLastChanged(SteamVR_Input_Sources.LeftHand) ) > 0.3f ) {
					leftKey1Shift = leftKey1Shift ? leftKey1Shift : Key1.GetState(SteamVR_Input_Sources.LeftHand);
				}

				if ( ! leftKey1Down && currentCord.Length >= 1 ) {
					if ( leftKey1Shift ) {
						typedKey =  currentCord.ToUpper()[0];
					} else {
						if (leftKey1) {
							typedKey =  currentCord.ToLower()[0];
						}
					}

					leftKey1Shift = false;
					leftKey1 = false;
				}

				bool leftKey2Down =  Key2.GetState(SteamVR_Input_Sources.LeftHand);
				leftKey2 = leftKey2 ?  leftKey2 : Key2.GetState(SteamVR_Input_Sources.LeftHand);

				if ( leftKey2Down && ( Time.time - Key2.GetTimeLastChanged(SteamVR_Input_Sources.LeftHand) ) > 0.3f ) {
					leftKey2Shift = leftKey2Shift ? leftKey2Shift : Key2.GetState(SteamVR_Input_Sources.LeftHand);
				}

				if ( ! leftKey2Down && currentCord.Length >= 2 ) {
					if ( leftKey2Shift ) {
						typedKey =  currentCord.ToUpper()[1];
					} else {
						if (leftKey2) {
							typedKey =  currentCord.ToLower()[1];
						}
					}

					leftKey2Shift = false;
					leftKey2 = false;
				}

				bool rightKey1Down =  Key1.GetState(SteamVR_Input_Sources.RightHand);
				rightKey1 = rightKey1 ?  rightKey1 : Key1.GetState(SteamVR_Input_Sources.RightHand);

				if ( rightKey1Down && ( Time.time - Key1.GetTimeLastChanged(SteamVR_Input_Sources.RightHand) ) > 0.3f ) {
					rightKey1Shift = rightKey1Shift ? rightKey1Shift : Key1.GetState(SteamVR_Input_Sources.RightHand);
				}

				if ( ! rightKey1Down && currentCord.Length >= 3 ) {
					if ( rightKey1Shift ) {
						typedKey =  currentCord.ToUpper()[2];
					} else {
						if (rightKey1) {
							typedKey =  currentCord.ToLower()[2];
						}
					}

					rightKey1Shift = false;
					rightKey1 = false;
				}

				bool rightKey2Down =  Key2.GetState(SteamVR_Input_Sources.RightHand);
				rightKey2 = rightKey2 ?  rightKey2 : Key2.GetState(SteamVR_Input_Sources.RightHand);

				if ( rightKey2Down && ( Time.time - Key2.GetTimeLastChanged(SteamVR_Input_Sources.RightHand) ) > 0.3f ) {
					rightKey2Shift = rightKey2Shift ? rightKey2Shift : Key2.GetState(SteamVR_Input_Sources.RightHand);
				}

				if ( ! rightKey2Down && currentCord.Length >= 4 ) {
					if ( rightKey2Shift ) {
						typedKey =  currentCord.ToUpper()[3];
					} else {
						if (rightKey2) {
							typedKey =  currentCord.ToLower()[3];
						}
					}

					rightKey2Shift = false;
					rightKey2 = false;
				}
			}

			if ( typedKey != (char) 0 ) {
				lastTypedKey = typedKey;
				output.text += typedKey;
			}

			typedKey = (char) 0;

			currentCord = null;

			leftIndex = leftCurl.IndexCurl < 0.6f;
			leftMiddle = leftCurl.MiddleCurl < 0.6f;
			rightIndex = rightCurl.IndexCurl < 0.6f;
			rightMiddle = rightCurl.MiddleCurl < 0.6f;
	
			// ( . | ) ( . . )
			if ( ! leftMiddle && leftIndex && ! rightIndex && ! rightMiddle ) {
				currentCord = keyMap[0];
			}
			// ( | | ) ( . . )
			if ( leftMiddle && leftIndex && ! rightIndex && ! rightMiddle ) {
				currentCord = keyMap[1];
			}
			// ( | | ) ( | . )
			if ( leftMiddle && leftIndex && rightIndex && ! rightMiddle ) {
				currentCord = keyMap[2];
			}
			// ( | | ) ( | | )
			if ( leftMiddle && leftIndex && rightIndex && rightMiddle ) {
				currentCord = keyMap[3];
			}
			// ( . | ) ( | | )
			if ( ! leftMiddle && leftIndex && rightIndex && rightMiddle ) {
				currentCord = keyMap[4];
			}
			// ( . . ) ( | | )
			if ( ! leftMiddle && ! leftIndex && rightIndex && rightMiddle ) {
				currentCord = keyMap[5];
			}
			// ( . . ) ( | . )
			if ( ! leftMiddle && ! leftIndex && rightIndex && ! rightMiddle ) {
				currentCord = keyMap[6];
			}
			// ( . | ) ( | . )
			if ( ! leftMiddle && leftIndex && rightIndex && ! rightMiddle ) {
				currentCord = keyMapSpecial[0];
			}
		}
	}
}