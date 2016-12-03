using UnityEngine;
using System.Collections;

public class TitleMgr : MonoBehaviour {

	// GamePad
	public Pad pad;

	// Use this for initialization
	void Start () {
		pad = new Pad();
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetKeyDown( KeyCode.Escape ) )
		{
			Application.Quit();
			return;
		}

		pad.Update();

		if ( pad.IsPushed() )
		{
			Application.LoadLevel("Main");
		}
	}
}
