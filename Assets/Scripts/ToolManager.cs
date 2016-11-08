﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ToolManager : MonoBehaviour {
	public GameObject con;
	public Text teltext;
	public static bool canteleport; 
	public static bool couldteleport; 
	public GameObject laser;
	public Image teleportbutton;
	public BrushManager brusher;
	public GameObject cursor;
	public Text retexturetxt;
	public SteamVR_TrackedObject trackedObj;
	public static GameObject curSelGameObject;
	public UndoManager Undoer;
	public GameObject eportmesh;
	// Use this for initialization
	void Start () {
		//laser.enabled = false;
	}
	
	void Awake () {
		laser.SetActive (false);
	}// Update is called once per frame
	void Update () {
	//hacky will change

		SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);
		if (curSelGameObject != null && device.GetTouch (SteamVR_Controller.ButtonMask.Grip)) {
			if (curSelGameObject.transform.parent == (eportmesh.transform)) {
				
				Undoer.prevTrans.Add (new UndoManager.PrevTrans (curSelGameObject.transform.position, curSelGameObject.transform.rotation, curSelGameObject.transform.localScale, curSelGameObject)); 
				curSelGameObject.transform.SetParent (cursor.transform);
			}

			curSelGameObject.transform.SetParent (cursor.transform);

		} else {

			curSelGameObject.transform.SetParent (eportmesh.transform);

		}

	}
	public void teleportoff(){
		cursor.SetActive (true);
		con.GetComponent <SteamVR_Teleporter> ().teleportOnClick = false;
		couldteleport = false;
		canteleport = false;
		teleportbutton.color = Color.white;
		teltext.text = "Teleporter: Off";
		BrushManager.canpaint = true;
		laser.SetActive (false);
	}
	public void telporton(){

		cursor.SetActive (false);
		BrushManager.canpaint = false;
		teleportbutton.color = Color.gray;
		laser.SetActive (true);
		con.GetComponent <SteamVR_Teleporter> ().teleportOnClick = true;
		teltext.text = "Teleporter: On";
		brusher.deselectAllbrushes ();

		Debug.Log (couldteleport);

	
	
	
	
	}



	public void teleporter(){
		couldteleport =!couldteleport;
		canteleport = !canteleport;


		if (couldteleport == true) {
			cursor.SetActive (false);
			BrushManager.canpaint = false;
			teleportbutton.color = Color.gray;
			laser.SetActive (true);
			con.GetComponent <SteamVR_Teleporter> ().teleportOnClick = true;
			teltext.text = "Teleporter: On";
			brusher.deselectAllbrushes ();

			Debug.Log (couldteleport);
		} else {
			if (BrushManager.brushswitchbool == false) {
				brusher.CylindarBrushtoggle ();

			} else {
				
		brusher.flatbrushtoggle ();

			}
			cursor.SetActive (true);
			laser.SetActive (false);
			Debug.Log (couldteleport);
			teleportbutton.color = Color.white;
			con.GetComponent <SteamVR_Teleporter> ().teleportOnClick =false;
			teltext.text = "Teleporter: Off";
			BrushManager.canpaint = true;
		}
	}
}
