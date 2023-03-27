﻿using System.IO;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class PostBuild : IPostprocessBuildWithReport {
	public int callbackOrder => 0;

	public void OnPostprocessBuild(BuildReport report) {
		//if (report.summary.result == BuildResult.Succeeded) {
			MoveFiles();
	}

	private void MoveFiles() {
		string[] filesToMove = {
			"/sheetData.json",
			"/map.png",
		};

		string targetDir = Application.dataPath + "/../" + "/Build/World War Mode Map Manager_Data";

		foreach (string file in filesToMove) File.Copy(Application.dataPath + file, targetDir + file, true);
	}
}

