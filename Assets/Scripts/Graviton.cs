using UnityEngine;
using System.Collections;

public class Graviton : Token {
	public GameObject target;	// 引力の発生する相手
	public float coefficient;	// 万有引力係数

	// ゲームマネージャ
	GameMgr gameMgr;

	// 生成
	void Start()
	{
		GameObject obj = GameObject.Find("GameMgr") as GameObject;

		gameMgr = obj.GetComponent<GameMgr>();
	}

	void FixedUpdate () 
	{
		if (gameMgr) {
			Vector3 cursor = new Vector3();
	//		cursor.x = gameMgr.pad.latest_.x;
	//		cursor.y = gameMgr.pad.latest_.y;
			cursor.x = gameMgr.pad.init_.x;
			cursor.y = gameMgr.pad.init_.y;
			cursor.z = 0.0f;

//			cursor = Camera.main.ScreenToWorldPoint(cursor);

			this.SetPosition(cursor.x, cursor.y);
		}

		if (target) {
			// 相手に向かう向きの取得
			Vector3 direction = target.transform.position - this.transform.position;

			// 相手までの距離の２乗を取得
			float distance = direction.magnitude * direction.magnitude;

			Rigidbody2D rgdThis   = this.GetComponent<Rigidbody2D>();
			Rigidbody2D rgdTarget = target.GetComponent<Rigidbody2D>();

			// 万有引力計算
			float gravity = coefficient 
				* rgdTarget.mass * rgdThis.mass	/ distance;

			// 力を与える
			rgdThis.AddForce(gravity * direction.normalized, ForceMode2D.Force);
		}
	}

	/// 座標の描画
	void OnGUI()
	{
		// テキストを黒にする
		Util.SetFontColor(Color.black);

		// テキストを大きめにする
		Util.SetFontSize(24);

		// 中央揃えにする
		Util.SetFontAlignment(TextAnchor.MiddleCenter);

		// テキスト描画
//		string text = string.Format("{0}\n{1}", 
//										this.transform.position.x, 
//										this.transform.position.y);
//		string text = string.Format("{0}\n{1}", 
//										gameMgr.pad.latest_.x, 
//										gameMgr.pad.latest_.y);

		string text = string.Format("{0}\n{1}", 
			gameMgr.pad.init_.x, 
			gameMgr.pad.init_.y);

		Util.GUILabel(380, 200, 120, 30, text);
	}
}
