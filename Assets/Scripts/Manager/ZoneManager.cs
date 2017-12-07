using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneManager : MonoBehaviour {

	static ZoneManager m_Instance;
	public static bool zone = false;

	public static ZoneManager Instance {
		get {
			if(m_Instance == null){
				GameObject obj = new GameObject ("ZoneManager");
				m_Instance = obj.AddComponent<ZoneManager> ();
			}
			return m_Instance;
		}
	}
		
	public void setZoneState(bool boolean) {
		zone = boolean;
		Debug.Log("set zone : " + zone);
	}
	public bool getZoneState() {
		return zone;
	}

}
