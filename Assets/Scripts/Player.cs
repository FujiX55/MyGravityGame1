                                                                                                                                                                                                                                                                                                         using UnityEngine;
using System.Collections;

/// <summary>
/// Player.
/// </summary>
public class Player : Token
{
	// GamePad
	public Pad pad;
	
	// ゲーム状態
	public enum eGameState
	{
		None,		// なし
		StageClear,	// ステージクリア
		GameOver	// ゲームオーバー
	}

	eGameState _gameState = eGameState.None;

	// ゲーム状態を設定する
	public void SetGameState(eGameState s)	{	_gameState = s;	}

	// ゲーム状態を取得する
	public eGameState GetGameState()	{	return _gameState;	}

	// 左を向いているかどうか
	bool _bFacingLeft = false;

	// 状態
	enum eState
	{
		Idle,	// 待機
		Run,	// 走り状態
		Jump	// ジャンプ
	}

	eState _state = eState.Idle;

	// アニメーションタイマー
	int _tAnim = 0;

	// 各種スプライト
	public Sprite Sprite0;		// 待機状態
	public Sprite Sprite1;		// 待機状態(まばたき)
	public Sprite Sprite2;		// 走り1
	public Sprite Sprite3;		// 走り2

	// 走る速さ
	[SerializeField]
	float _RunSpeed = 2;

	// ジャンプの速さ
	[SerializeField]
	float _JumpSpeed = 4;

	// ゲームマネージャ
	GameMgr gameMgr_;

	// 生成
	void Start()
	{
		gameMgr_ = GameObject.Find("GameMgr").GetComponent<GameMgr>();
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update()
	{
		Vector2 v = gameMgr_.pad.GetVector ();

		// 左右キーで移動する
		VX = v.x * _RunSpeed;

		// 向いている方向チェック
		if 		(VX <= -1.0f)
		{	
			_bFacingLeft = true;
		}			// 左を向く
		else if (VX >=  1.0f)	
		{	
			_bFacingLeft = false;
		}			// 右を向く
		else
		{
			GravityScale = 1.0f;
		}
	
		// ジャンプ判定
		if ( gameMgr_.pad.IsPushed() )
		{
			VY = _JumpSpeed;
		}
	}

	// 固定フレームで更新
	void FixedUpdate()
	{
		// 左右の向きを切り替える
		ScaleX = (_bFacingLeft) ? -1.0f : 1.0f;
	
		// アニメーションタイマーを更新
		_tAnim++;

		// 状態更新
		if (Mathf.Abs (VX) >= 1.0f) {
			// 移動しているので走り状態
			_state = eState.Run;
		} else {
			// 待機状態
			_state = eState.Idle;
		}

		// アニメーション更新
		switch (_state)
		{
		case eState.Idle:
			if (_tAnim % 200 < 10)
			{
				// たまに瞬きする
				SetSprite(Sprite1);
			}
			else
			{
				SetSprite(Sprite0);
			}
			break;

		case eState.Run:
			// 走り状態
			if (_tAnim % 12 < 6)
			{
				SetSprite(Sprite2);
			}
			else
			{
				SetSprite(Sprite3);
			}
			break;

		case eState.Jump:
			// ジャンプ中
			SetSprite(Sprite2);
			break;
		}
	}

	// 消滅
	public override void Vanish()
	{
		// パーティクル生成
		for (int i = 0; i < 32; i++)
		{
			Particle.Add(X, Y);
		}
		base.Vanish();

		GameObject obj = GameObject.Find("Doctor") as GameObject;

		if (obj != null)
		{
//			Doctor doc = obj.GetComponent<Doctor>();
//			doc.Vanish();
////		obj.SetActive (false);
		}
	}

	// テスト
	void OnTriggerEnter2D( Collider2D other )
	{
		;
	}
}
