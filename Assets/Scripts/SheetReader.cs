﻿using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SheetReader : MonoBehaviour {
	static private string spreadsheetId;
	static private string serviceAccountID;
	static private string private_key;
	static private SheetsService service;

	void Awake() {
		Initiate();
		ServiceAccountCredential.Initializer initializer = new ServiceAccountCredential.Initializer(serviceAccountID);
		ServiceAccountCredential credential = new ServiceAccountCredential(
			initializer.FromPrivateKey(private_key)
		);

		service = new SheetsService(
			new BaseClientService.Initializer() {
				HttpClientInitializer = credential,
			}
		);
	}

	public IList<IList<object>> GetSheetRange(string sheetNameAndRange) {
		SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(spreadsheetId, sheetNameAndRange);

		ValueRange response = request.Execute();
		IList<IList<object>> values = response.Values;
		if (values != null && values.Count > 0) {
			return values;
		} else {
			Debug.Log("No data found.");
			return null;
		}
	}

	public void SetSheetRange(IList<IList<object>> dataArray, string range) {
		ValueRange convertedData = new ValueRange();
		convertedData.Values = dataArray;
		convertedData.Range = range;

		SpreadsheetsResource.ValuesResource.UpdateRequest request = service.Spreadsheets.Values.Update(convertedData, spreadsheetId, range);

		request.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
		UpdateValuesResponse response = request.Execute();
	}

	public void Initiate() {
		string path = Application.dataPath + "/sheetData.json";
		string jsonString = File.ReadAllText(path);
		SheetData data = JsonUtility.FromJson<SheetData>(jsonString);

		serviceAccountID = data.client_email;
		spreadsheetId = data.spreadsheet_id;
		private_key = data.private_key;
	}
}

[System.Serializable]
public class SheetData {
	public string private_key;
	public string client_email;
	public string spreadsheet_id;
}