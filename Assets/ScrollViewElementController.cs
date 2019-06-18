using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewElementController : MonoBehaviour
{
	public string timeFormatString = "dd/MM HH:mm";
	public Image backgroundImage;
	public Text codeText;
	public Text timeText;

	public void Init(ScannedCodes.ScannedCodeDataModel data, bool present = false)
	{
		codeText.text = data.code;
		timeText.text = data.scannedTime.ToString(timeFormatString);
		backgroundImage.color = present ? Color.green : new Color(60, 60, 60);
		codeText.color = present ? Color.black : Color.white;
		timeText.gameObject.SetActive(present);
		if(present)
		{
			transform.SetAsFirstSibling();
		}
	}

	public static void Instantiate(GameObject prefab, Transform parent, ScannedCodes.ScannedCodeDataModel data, bool present = false)
	{
		var instance = Instantiate(prefab, parent);
		instance.GetComponent<ScrollViewElementController>().Init(data);
	}
}
