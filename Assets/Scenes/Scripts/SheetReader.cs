﻿using System;
using System.Collections.Generic;

using Google.Apis.Services;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

using UnityEngine;

public class SheetReader : MonoBehaviour {
	static private string spreadsheetId = "1YE0NQo9vfnoVjp4pYJha0kraenESIsRjqUWcNpQG_1s";
	static private string serviceAccountID = "sheetkerbalbot@kerbalbotintergation.iam.gserviceaccount.com";
	static private SheetsService service;

	void Awake() {
		ServiceAccountCredential.Initializer initializer = new ServiceAccountCredential.Initializer(serviceAccountID);
		ServiceAccountCredential credential = new ServiceAccountCredential(
			initializer.FromPrivateKey("-----BEGIN PRIVATE KEY-----\nMIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQC6M5OPPW82CMJZ\nGVN3mBCVAv5PfqtkFMIebZRKz9Rh5uSMpujylrR7Y3slvoxG/ldXBw9OEJDou4Rj\nSwc0ibcN6BugDP6UFBK1iqXJcsReFfwIOJulAr8jM3FQahpkcy38R98tIzKegqKK\nk/KTXJoIpr/F3WmJopOS+xQYDvGLwq3sDkZzBvNxXK59U15R8gUl0e60Ce5dKoA/\n4mc3YuSnuxt9eSDMeiLAhVKEftYOmgmfLTjzYY61r46GQ3zyT8/nhmaZ2PybGK6L\n24fF3fwFYb8YGj3RKV2lov64SIQG4FZ3hieazzr3V/mej/0wBCgXXxOZJgF/VFSv\n3feBNpezAgMBAAECggEATL/0qz655+5dWYeAaZPTBfDpbU/7inGqAAvP67RVyRL4\nkuc7rr6i3xdGy+yKerrcumENFHLsoBmm/JyQ+D1pqEBLnsGsb9IEUjM3md0GBvgN\n3j+jHDWRHovWQbyya0Q0qL/hRIQ1GbQkFnXcL5SkYzuEYaUdmt9Z7+QxSKdkNeRQ\nz9kA1oEImDFt6whXCkonEqFiWKhr2aDfk/Nc9COkCHOCjh+RJWNhrGmj3yVibFhb\nR8Jkiz+j5NaFZvmalkH6XknaNFTQPnBBTv039cdjhr528OHYfQG7SJF71hJAs21t\nrBe7BHpj/XB4YUNGFULS5XwpmWP0N7yckVOVSD6AbQKBgQDt8Ge0yJOD7d4ufoQN\nY0mJipSloQVyMhiDC2LRRGzUnO5XMcitX6uy1i8OxUJJLlca3gGtQP92PK/8qkmx\n6gZFEbzfazFzi/Z6Tgye1wncUtBpldfCQnnReeWZ2Ix4ZE3CGdrUtY3mDxLvnaPa\ncihSSIHAi20664ig1sgMC3eODwKBgQDIVdBr2UuxplIpd+MR2V3m9M3z8Id0G5jt\nLUSn0A1+Bg5tJDtPoIaEgDVszpnDTHzqNtAQVjJIuUQCLrK8YysrDRdmBXkbrRjC\nFkaXmBR2mRbuPzfvrpnwFNFaW+cZtz15BAxwwAH63Zqe2kPrlPjieIav70XCmtHR\nH0rq3ByAHQKBgQCLaz9JFfIh7sIuWiGEOrY/Kn15I4WuaR4lwwku97oRRrb9ToV4\n2UMhMhE2xWzWtpmBe65d12UY4ex4z0sJPchYtlEGVKgQPWW6Ont4oyX6/Gd0RsG6\n6Pq6PIPFhragYJ2Ta1TnKE6yDAkbcDIvcI6h0Cx/JvNk/9f57oAfVqTCDwKBgQCF\nNEn46+rgnG5VFFnvLFC7mFq4sF3gXdk5GrhjvHq8KNq+xzCIqXvH7leXmWez68/D\nC98cfbPBly4ZJWPCz02Muo+sTkdQl6+2nYkikwRIN1J/55kRzU6dy7nIEwN+ndBJ\n/t8muQSYAXEl3or6wXsbWnhz0uzXkImiOuRddS2eIQKBgC3MGgqzMYZVkuLXNx7n\n99F5KulQOQvbi/pEJ0aBUCIX5V7V5xdd0KL1hYL3wtEt3FDX7W9LSM4nEa8S1dYl\nH+VLewp88aNioBt+QzmZ7n7vFBa8OWIwBmUL8lI14aMlktSjs90d0RF7oSiG9ZZl\nMmxWoOvWpRRNXnyH4/5Gmt2h\n-----END PRIVATE KEY-----\n")
		);
		
		service = new SheetsService(
			new BaseClientService.Initializer() {
				HttpClientInitializer = credential,
			}
		);
	}

	public IList<IList<object>> getSheetRange(string sheetNameAndRange) {
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


	public void setSheetRange(IList<IList<object>> dataArray, string range) {
		ValueRange convertedData = new ValueRange();
		convertedData.Values = dataArray;
		convertedData.Range = range;

		SpreadsheetsResource.ValuesResource.UpdateRequest request = service.Spreadsheets.Values.Update(convertedData, spreadsheetId, range);

		request.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
		UpdateValuesResponse response = request.Execute();
	}
}