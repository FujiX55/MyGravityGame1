using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {
	// 追従するターゲット
	public Token target;

	// ゲームマネージャ
	GameMgr gameMgr;

	// Use this for initialization
	void Start () {
		GameObject obj = GameObject.Find("GameMgr") as GameObject;

		gameMgr = obj.GetComponent<GameMgr>();	
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null)
		{
			Vector3 pos = Camera.main.transform.position;
			Vector3 bak = pos;

			pos.x = target.X;

			Camera.main.transform.position = pos;

			Vector2 min = Camera.main.ViewportToWorldPoint(Vector2.zero);
			Vector2 max = Camera.main.ViewportToWorldPoint(Vector2.one);

			// カメラの座標をプレイヤに追従させる
			if ( (max.x < gameMgr.field.right) && (min.x > gameMgr.field.left) )
			{
				;
			}
			else
			{
				Camera.main.transform.position = bak;
			}
		}
	}
}
