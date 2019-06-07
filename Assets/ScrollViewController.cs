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

		foreach(var model in data)
		{
			ScrollViewElementController.Instantiate(prefab, elementsContainer, model);
		}
	}
}
