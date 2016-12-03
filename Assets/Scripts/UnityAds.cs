using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAds : MonoBehaviour
{
	public void ShowAd()
	{
		if (Advertisement.IsReady())
		{
			Advertisement.Show();
		}
	}
}
