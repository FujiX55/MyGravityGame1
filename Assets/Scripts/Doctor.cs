using UnityEngine;
using System.Collections;

public class Doctor : Token {
	public Token target;	// 引力の発生する相手
	public float coefficient;	// 万有引力係数

	// ゲームマネージャ
	GameMgr gameMgr;

	// Use this for initialization
	void Start () {
		GameObject obj = GameObject.Find("GameMgr") as GameObject;

		gameMgr = obj.GetComponent<GameMgr>();	
	}
	
	// Update is called once per frame
	void Update () {
		if (target && (gameMgr.pad.latest_.x > 0.0f || gameMgr.pad.latest_.y > 0.0f)) {
			// 相手に向かう向きの取得
			Vector3 direction = target.transform.position - this.transform.position;

			// 相手までの距離の２乗を取得
			float distance = direction.magnitude * direction.magnitude;

			Rigidbody2D rgdThis   = this.GetComponent<Rigidbody2D>();
			Rigidbody2D rgdTarget = target.GetComponent<Rigidbody2D>();

			// 万有引力計算
			float gravity = coefficient * rgdTarget.mass * rgdThis.mass	/ distance;

			if (10.0f < gravity)
			{
				gravity = 10.0f;
			}

			// 力を与える
			rgdThis.AddForce(gravity * direction.normalized, ForceMode2D.Force);
		}	
	}

	// プレイヤとの接触チェック
	public void OnTriggerEnter2D(Collider2D other)
	{
		// レイヤ名を取得
		string name = LayerMask.LayerToName (other.gameObject.layer);

		if (name == "Player")
		{
			GameObject obj = GameObject.Find("Player") as GameObject;
			float vx = obj.GetComponent<Player>().VX;
			float vy = obj.GetComponent<Player>().VY;

			// プレイヤの横方向の移動速度がハカセの上昇速度に影響する
			this.AddVelocityXY(0.0f, Mathf.Abs(vx) * 2.0f);
		}
	}
}
