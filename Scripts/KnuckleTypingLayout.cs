using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MayBeKnuckles {
	public class KnuckleTypingLayout : MonoBehaviour {
		public KnuckleTyping typing;
		// Use this for initialization
		void Start () {
			if ( typing != null ) {
				// e t a o i n s r h l d c u m f p g w y b v k x j q z
				typing.addCord( KnuckleTyping.Gestures.XXXX_XXXX, "AETO" );
				typing.addCord( KnuckleTyping.Gestures.XXXI_XXXX, "SINR" );
				typing.addCord( KnuckleTyping.Gestures.XXXX_IXXX, "DHLC" );
				typing.addCord( KnuckleTyping.Gestures.XXII_XXXX, "FUMP" );
				typing.addCord( KnuckleTyping.Gestures.XIII_XXXX, "FUMP" );
				typing.addCord( KnuckleTyping.Gestures.IIII_XXXX, "FUMP" );
				typing.addCord( KnuckleTyping.Gestures.XXXX_IIXX, "YGWB" );
				typing.addCord( KnuckleTyping.Gestures.XXXX_IIIX, "YGWB" );
				typing.addCord( KnuckleTyping.Gestures.XXXX_IIII, "YGWB" );
				typing.addCord( KnuckleTyping.Gestures.XXXI_IXXX, "XVKJ" );
				typing.addCord( KnuckleTyping.Gestures.XXII_IIXX, ".!qQzZ ," );
				typing.addCord( KnuckleTyping.Gestures.XIII_IIIX, ".!qQzZ ," );
				typing.addCord( KnuckleTyping.Gestures.IIII_IIII, ".!qQzZ ," );
			}
		}
		
		// Update is called once per frame
		void Update () {
			
		}
	}
}
