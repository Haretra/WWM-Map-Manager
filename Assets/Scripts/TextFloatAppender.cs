﻿using TMPro;
using UnityEngine;

public class TextFloatAppender : MonoBehaviour {
	private TextMeshProUGUI text;
	// Use this for initialization
	void Start() {
		text = GetComponent<TextMeshProUGUI>();
	}

	public void UpdateText(float newText) {
		try {
			if (text.text.Split(':')[1].Trim().Length > 1) {
				text.text = text.text.Substring(0, text.text.Length - 2) + newText;
			} else {
				text.text = text.text.Substring(0, text.text.Length - 1) + newText;
			}
		} catch (System.NullReferenceException) { }

	}
}
