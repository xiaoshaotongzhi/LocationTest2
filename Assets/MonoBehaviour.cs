using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{

	public Text infoText;
	public Button getLocationBtn;

	void Start()
	{
		getLocationBtn.onClick.AddListener(() => {
			StartCoroutine(StartLocate());
		});
	}

	IEnumerator StartLocate()
	{
		if (!Input.location.isEnabledByUser)
		{
			Input.location.Start();
			infoText.text = "false == location.isEnabledByUser";
			yield break;
		}

		// Start service before querying location
		Input.location.Start();

		// Wait until service initializes
		int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
		{
			yield return new WaitForSeconds(1);
			maxWait--;
		}

		// Service didn't initialize in 20 seconds
		if (maxWait < 1)
		{
			var logStr = "Timed out";
			infoText.text = logStr;
			Debug.Log(logStr);
			yield break;
		}

		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed)
		{
			var logStr = "Unable to determine device location";
			infoText.text = logStr;
			Debug.Log(logStr);
			yield break;
		}
		else
		{
			// Access granted and location value could be retrieved
			var logStr = "Location, 纬度: " + Input.location.lastData.latitude + ",经度: " + Input.location.lastData.longitude + ",高度: " + Input.location.lastData.altitude + "\n水平精确度: " + Input.location.lastData.horizontalAccuracy + ",时间戳: " + Input.location.lastData.timestamp;
			infoText.text = logStr;
			Debug.Log(logStr);
			//取出位置的经纬度
			string str = Input.location.lastData.longitude + "," + Input.location.lastData.latitude;

		}

		// Stop service if there is no need to query location updates continuously
		Input.location.Stop();
	}

}
