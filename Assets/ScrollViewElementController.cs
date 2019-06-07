using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewElementController : MonoBehaviour
{
	public string timeFormatString = "dd/MM HH:mm";
	public Text codeText;
	public Text timeText;

	public void Init(ScannedCodes.ScannedCodeDataModel data)
	{
		codeText.text = data.code;
		timeText.text = data.scannedTime.ToString(timeFormatString);		
	}

	public static void Instantiate(GameObject prefab, Transform parent, ScannedCodes.ScannedCodeDataModel data)
	{
		var instance = Instantiate(prefab, parent);
		instance.GetComponent<ScrollViewElementController>().Init(data);
	}
}
