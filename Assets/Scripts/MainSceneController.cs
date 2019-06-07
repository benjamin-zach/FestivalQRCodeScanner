using BarcodeScanner;
using BarcodeScanner.Scanner;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneController : MonoBehaviour
{
	private IScanner BarcodeScanner;
	public Text statusText;
	public RawImage Image;
	public AudioSource Audio;
	public ScannedCodes scannedCodes;
	public ScrollViewController scrollViewController;
	public Button optionsButton;
	public GameObject optionsPanel;
	public Button removeCodeButton;
	public InputField removeCodeInputField;
	public Button resetButton;
	private float RestartTime;

	// Disable Screen Rotation on that screen
	void Awake()
	{
		Screen.autorotateToPortrait = false;
		Screen.autorotateToPortraitUpsideDown = false;
		Screen.orientation = ScreenOrientation.Portrait;
	}

	void Start()
	{
		// Create a basic scanner
		BarcodeScanner = new Scanner();
		BarcodeScanner.Camera.Play();

		// Display the camera texture through a RawImage
		BarcodeScanner.OnReady += (sender, arg) => {
			// Set Orientation & Texture
			Image.transform.localEulerAngles = BarcodeScanner.Camera.GetEulerAngles();
			Image.transform.localScale = BarcodeScanner.Camera.GetScale();
			Image.texture = BarcodeScanner.Camera.Texture;

			// Keep Image Aspect Ratio
			var rect = Image.GetComponent<RectTransform>();
			var newHeight = rect.sizeDelta.x * BarcodeScanner.Camera.Height / BarcodeScanner.Camera.Width;
			rect.sizeDelta = new Vector2(rect.sizeDelta.x, newHeight);

			RestartTime = Time.realtimeSinceStartup;
		};

		optionsButton.onClick.AddListener(OnOptionsButton);
		removeCodeButton.onClick.AddListener(OnRemoveCodeButton);

		RefreshView();
	}

	private void RefreshView()
	{
		scrollViewController.Init(scannedCodes.data);
	}

	/// <summary>
	/// Start a scan and wait for the callback (wait 1s after a scan success to avoid scanning multiple time the same element)
	/// </summary>
	private void StartScanner()
	{
		BarcodeScanner.Scan(OnQrCodeRecognized);
	}

	/// <summary>
	/// The Update method from unity need to be propagated
	/// </summary>
	void Update()
	{
		if (BarcodeScanner != null)
		{
			BarcodeScanner.Update();
		}

		// Check if the Scanner need to be started or restarted
		if (RestartTime != 0 && RestartTime < Time.realtimeSinceStartup)
		{
			StartScanner();
			RestartTime = 0;
			statusText.color = Color.white;
			statusText.text = "READY";
		}
	}

	private void OnQrCodeRecognized(string barCodeType, string barCodeValue)
	{
		BarcodeScanner.Stop();
		RestartTime += Time.realtimeSinceStartup + 1f;

		// Feedback
		Audio.Play();

#if UNITY_ANDROID || UNITY_IOS
			Handheld.Vibrate();
#endif

		if(scannedCodes.Contains(barCodeValue))
		{
			statusText.color = Color.red;
			statusText.text = "ALREADY SCANNED";
		}
		else
		{
			statusText.color = Color.green;
			statusText.text = "NEW QRCODE ADDED";
			scannedCodes.Add(barCodeValue);
			scannedCodes.Save();
			scrollViewController.Init(scannedCodes.data);
		}
	}

	private void OnOptionsButton()
	{
		optionsPanel.SetActive(!optionsPanel.activeSelf);
	}

	private void OnRemoveCodeButton()
	{
		var code = removeCodeInputField.text;
		scannedCodes.Remove(code);
		scannedCodes.Save();
		RefreshView();
	}
}
