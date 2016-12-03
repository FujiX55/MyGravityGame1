using UnityEngine;
using System.Collections;

public class Wall : Token
{
	public static TokenMgr<Wall> parent = null;

	// 壁を作る
	public static Wall Add(float x, float y)
	{
		Wall w = parent.Add (x, y);

		return w;
	}

	// 当たり判定を切り替える
	void OnTriggerEnter2D( Collider2D other )
	{
		;
	}
}
