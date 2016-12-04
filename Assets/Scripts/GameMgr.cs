using UnityEngine;
using System.Collections;

public class GameMgr : MonoBehaviour 
{	
	// 状態
	public enum eState
	{
		Main,		// メイン
		StageClear,	// ステージクリア
		GameOver	// ゲームオーバー
	}

	// 状態
	public eState state = eState.Main;

	// 現在のステージ番号
	public int nStage = 1;

	// プレイヤへの参照
	Player player = null;

	// GamePad
	public Pad      pad;
	public FieldMgr field;

	// 開始
	void Start()
	{
		pad   = new Pad();
		field = new FieldMgr ();

		// 壁オブジェクト管理生成
		// 移動床オブジェクト管理作成
		// パーティクル管理作成
		// トゲ監理生成
		Wall.parent      = new TokenMgr<Wall> ("Wall", 128);
		FloorMove.parent = new TokenMgr<FloorMove> ("FloorMove", 32);
		Particle.parent  = new TokenMgr<Particle> ("Particle", 32);
		Spike.parent     = new TokenMgr<Spike> ("Spike", 32);

		// マップデータの読み込み
		Load (nStage);
	}

	// プレイヤのゲーム状態をチェックする
	void CheckPlayerGameState()
	{
		if (player == null) 
		{
			// プレイヤへの参照を取得する
			GameObject obj = GameObject.Find("Player") as GameObject;
			player = obj.GetComponent<Player>();
		}

		switch ( player.GetGameState() ) 
		{
		// ステージクリア
		case Player.eGameState.StageClear:
			state = eState.StageClear;
			break;

		// ゲームオーバー
		case Player.eGameState.GameOver:
			state = eState.GameOver;
			break;
		}
	}

	// 更新
	void Update()
	{
		if ( Input.GetKeyDown( KeyCode.Escape ) )
		{
			Application.Quit();
			return;
		}

		pad.Update();

		switch (state) 
		{
		case eState.Main:
			// プレイヤのゲーム状態をチェックする
			CheckPlayerGameState();
			break;

		// ステージクリア
		case eState.StageClear:
			if ( pad.IsPushed() )
			{
				// PUSH押下で次に進む
				Restore();

				// 次のステージに進む
				nStage++;

				if (nStage > 3)
				{
					// 全ステージクリア
					// ステージ1に戻る
					nStage = 1;
				}

				// マップデータ読み込み
				Load(nStage);

				state = eState.Main;
			}
			break;

		// ゲームオーバー
		case eState.GameOver:
			if ( pad.IsPushed() )
			{
				// Spaceキーを押したらやり直し
				Restore();

				// マップデータ読み込み
				Load(nStage);

				state = eState.Main;
			}
			break;
		}
	}

	// マップをロードする
	void Load(int stage)
	{
		// マップデータの読み込み
		field.Load (stage);
	}

	// 各種オブジェクトを全部消す
	void Restore()
	{
		Wall.parent.Vanish ();
		FloorMove.parent.Vanish ();
		Particle.parent.Vanish ();
		Spike.parent.Vanish ();

		// プレイヤを復活させる
		player.Revive ();

		// 初期状態に戻す
		player.SetGameState (Player.eGameState.None);
	}

	// ラベルを画面中央に表示
	void DrawLabelCenter(string message)
	{
		// フォントサイズ設定
		Util.SetFontSize (32);

		// 中央揃え
		Util.SetFontAlignment (TextAnchor.MiddleCenter);

		// フォントの位置
		float w = 128;	// 幅
		float h = 32;	// 高さ
		float px = Screen.width / 2 - w / 2;
		float py = Screen.height / 2 - h / 2;

		// フォント描画
		Util.GUILabel (px, py, w, h, message);
	}

	void OnGUI()
	{
		switch (state) 
		{
		case eState.StageClear:
			DrawLabelCenter("STAGE CLEAR!");
			break;

		case eState.GameOver:
			DrawLabelCenter("GAME OVER");
			break;
		}
	}
}
