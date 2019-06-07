using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class ScannedCodes : MonoBehaviour
{
	[Serializable]
	public class ScannedCodeDataModel
	{
		public string code;
		public DateTime scannedTime;
	}

	public string savePrefix = "Data";
	public List<ScannedCodeDataModel> data = new List<ScannedCodeDataModel>();

	private void Awake()
	{
		Load();
	}

	private void OnDestroy()
	{
		Save();
	}

	public bool Contains(string code)
	{
		return data.Find(item => item.code == code) != null;
	}

	public void Add(string newCode)
	{
		data.Add(new ScannedCodeDataModel()
		{
			code = newCode,
			scannedTime = DateTime.Now
		});
	}

	public void Remove(string code)
	{
		var d = data.Find(item => item.code == code);
		if (d != null)
			data.Remove(d);
	}

	public void Load()
	{
		data = new List<ScannedCodeDataModel>();

		int index = 0;
		while(PlayerPrefs.HasKey(savePrefix + index.ToString()))
		{
			var line = PlayerPrefs.GetString(savePrefix + index.ToString());
			string[] values = line.Split(new char[] { ',' });
			data.Add(new ScannedCodeDataModel()
			{
				code = values[0],
				scannedTime = DateTime.ParseExact(values[1], "dd/MM HH:mm", CultureInfo.InvariantCulture)
			});

			++index;
		}
	}

	public void Save()
	{
		int index = 0;
		foreach(var value in data)
		{
			var line = string.Format("{0},{1}", value.code, value.scannedTime.ToString("dd/MM HH:mm"));
			PlayerPrefs.SetString(savePrefix + index.ToString(), line);

			++index;
		}
	}

}
