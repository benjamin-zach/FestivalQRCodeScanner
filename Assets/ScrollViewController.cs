using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollViewController : MonoBehaviour
{
	#region View
	public Transform elementsContainer;
	public GameObject prefab;
	#endregion

	public void Init(List<ScannedCodes.ScannedCodeDataModel> data)
	{
		foreach(Transform child in elementsContainer)
		{
			Destroy(child.gameObject);
		}

		foreach(var code in MainSceneController.expectedCodes)
		{
			ScannedCodes.ScannedCodeDataModel model = data.Find(item => item.code == code);
			bool present = true;
			if(model == null)
			{
				model = new ScannedCodes.ScannedCodeDataModel() { code = code, scannedTime = System.DateTime.Now };
				present = false;
			}
			ScrollViewElementController.Instantiate(prefab, elementsContainer, model, present);
		}
	}
}
