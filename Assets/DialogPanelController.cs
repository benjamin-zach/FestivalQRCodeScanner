using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogPanelController : MonoBehaviour
{
	public Text messageText;
	public Button positiveButton;
	public Button negativeButton;

	public void Show(UnityEngine.Events.UnityAction positiveAction, UnityEngine.Events.UnityAction negativeAction, string message = null)
	{
		if(!string.IsNullOrEmpty(message))
		{
			messageText.text = message;
		}
		positiveButton.onClick.AddListener(positiveAction);
		negativeButton.onClick.AddListener(negativeAction);

		gameObject.SetActive(true);
	}

	public void Dismiss()
	{
		gameObject.SetActive(false);
	}
}
