using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Range {
		public float min;
		public float max;
		
		public Range (float mi, float ma) {
			min = mi;
			max = ma;
		}
	}
