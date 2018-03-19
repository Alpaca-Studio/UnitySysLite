using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SysUpdateDownloader : MonoBehaviour {
#if UNITY_EDITOR
	string _versionURL = "https://raw.githubusercontent.com/Alpaca-Studio/UnitySysLite/master/Source/Assets/Plugins/SysAPI/vc.dxt"+Sys.M.URLAntiCache();
	string[] _sourceURL = {
		"https://raw.githubusercontent.com/Alpaca-Studio/UnitySysLite/master/Source/Assets/Plugins/SysAPI/Scripts/SYS_LOGGING.cs"+"?t="+System.DateTime.Now.ToString("MMddyyyyhhmmss"),
		"https://raw.githubusercontent.com/Alpaca-Studio/UnitySysLite/master/Source/Assets/Plugins/SysAPI/Scripts/SYS_IO.cs"+"?t="+System.DateTime.Now.ToString("MMddyyyyhhmmss"),
		"https://raw.githubusercontent.com/Alpaca-Studio/UnitySysLite/master/Source/Assets/Plugins/SysAPI/Scripts/SYS_SCREENSHOT.cs"+"?t="+System.DateTime.Now.ToString("MMddyyyyhhmmss"),
		"https://raw.githubusercontent.com/Alpaca-Studio/UnitySysLite/master/Source/Assets/Plugins/SysAPI/Scripts/SYS_INFO.cs"+"?t="+System.DateTime.Now.ToString("MMddyyyyhhmmss"),
		"https://raw.githubusercontent.com/Alpaca-Studio/UnitySysLite/master/Source/Assets/Plugins/SysAPI/Scripts/SYS_MATH.cs"+"?t="+System.DateTime.Now.ToString("MMddyyyyhhmmss"),
		"https://raw.githubusercontent.com/Alpaca-Studio/UnitySysLite/master/Source/Assets/Plugins/SysAPI/Scripts/SYS_STACKTRACE.cs"+"?t="+System.DateTime.Now.ToString("MMddyyyyhhmmss"),
		"https://raw.githubusercontent.com/Alpaca-Studio/UnitySysLite/master/Source/Assets/Plugins/SysAPI/Scripts/SYS_M.cs"+"?t="+System.DateTime.Now.ToString("MMddyyyyhhmmss"),
	
	};
	string _docuURL = "https://raw.githubusercontent.com/Alpaca-Studio/UnitySysLite/master/Source/Assets/Sys_API/Documentation.txt"+"?t="+System.DateTime.Now.ToString("MMddyyyyhhmmss");
	string _sysCSURL = "https://raw.githubusercontent.com/Alpaca-Studio/UnitySysLite/master/Source/Assets/Sys_API/Examples/Sys_API_CSharp_Example.cs"+"?t="+System.DateTime.Now.ToString("MMddyyyyhhmmss");
	string _sysJSURL = "https://raw.githubusercontent.com/Alpaca-Studio/UnitySysLite/master/Source/Assets/Sys_API/Examples/Sys_API_JS_Example.js";
	string _changelogURL = "https://raw.githubusercontent.com/Alpaca-Studio/UnitySysLite/master/Source/Assets/Sys_API/change.log"+"?t="+System.DateTime.Now.ToString("MMddyyyyhhmmss");
	string _silURL = "https://raw.githubusercontent.com/Alpaca-Studio/UnitySysLite/master/Source/Assets/Editor/SystemInformationLogger.cs"+"?t="+System.DateTime.Now.ToString("MMddyyyyhhmmss");
	string _path;
	//List<string> VC = new List<string>();
	string _currentVersion;
	public bool forceUpdate = false;
	int dlCount = 0;
	
	void Start () {
		dlCount = 0;
		_path = Application.dataPath + "/Plugins/SysAPI/";
		_currentVersion = UnityEditor.EditorPrefs.GetString("VC");
		if(!forceUpdate){
			StartCoroutine(DownloadVC(_versionURL));
		} else {
			File.Create(Application.dataPath + "/Plugins/SysAPI/Scripts/SYS_MASTER.cs").Dispose();
			InitDataFiles();
		}
	}
	
	IEnumerator DownloadVC(string url) {
		WWW www = new WWW(url);
		yield return www;
		string file = www.text;
		if(www.error != null){Debug.LogError("[Sys API] ERROR001: "+www.error+" (EC-SUD-037)");}
		if(file.Length == 0 || file == null){Debug.LogError("[Sys API] ERROR000: Unable to establish connection. (EC-SUD-036)");} else {
			File.Create(_path+"vc.dxt").Dispose();
			File.WriteAllText(_path+"vc.dxt", file); 
			InitDataFiles();
		}
		
	}
	
	void InitDataFiles () {
		if(File.ReadAllLines(_path+"vc.dxt")[0] != null){
			string newVC = File.ReadAllLines(_path+"vc.dxt")[0];
			if(newVC != _currentVersion || forceUpdate){
			//Get Sys Logging
				if(File.Exists(Application.dataPath + "/Plugins/SysAPI/Scripts/SYS_LOGGING.cs")){
					File.Create(Application.dataPath + "/Plugins/SysAPI/Scripts/SYS_LOGGING.cs").Dispose();
					Debug.LogWarning("[Sys API]: Downloading Sys API v" + newVC );
					StartCoroutine(DownloadDataFiles(_sourceURL[0], Application.dataPath + "/Plugins/SysAPI/Scripts/","SYS_LOGGING.cs"));
					Debug.LogWarning("[Sys API]: Downloading SYS_LOGGING.cs");
				}
			//Get Sys IO
				if(File.Exists(Application.dataPath + "/Plugins/SysAPI/Scripts/SYS_IO.cs")){
					File.Create(Application.dataPath + "/Plugins/SysAPI/Scripts/SYS_IO.cs").Dispose();
					
					StartCoroutine(DownloadDataFiles(_sourceURL[1], Application.dataPath + "/Plugins/SysAPI/Scripts/","SYS_IO.cs"));
					Debug.LogWarning("[Sys API]: Downloading SYS_IO.cs");
				}
			//Get Sys Info
				if(File.Exists(Application.dataPath + "/Plugins/SysAPI/Scripts/SYS_INFO.cs")){
					File.Create(Application.dataPath + "/Plugins/SysAPI/Scripts/SYS_INFO.cs").Dispose();
					
					StartCoroutine(DownloadDataFiles(_sourceURL[3], Application.dataPath + "/Plugins/SysAPI/Scripts/","SYS_INFO.cs"));
					Debug.LogWarning("[Sys API]: Downloading SYS_INFO.cs");
				}
			//Get Sys Math
				if(File.Exists(Application.dataPath + "/Plugins/SysAPI/Scripts/SYS_MATH.cs")){
					File.Create(Application.dataPath + "/Plugins/SysAPI/Scripts/SYS_MATH.cs").Dispose();
					
					StartCoroutine(DownloadDataFiles(_sourceURL[4], Application.dataPath + "/Plugins/SysAPI/Scripts/","SYS_MATH.cs"));
					Debug.LogWarning("[Sys API]: Downloading SYS_MATH.cs");
				}
			//Get Sys StackTrace
				if(File.Exists(Application.dataPath + "/Plugins/SysAPI/Scripts/SYS_STACKTRACE.cs")){
					File.Create(Application.dataPath + "/Plugins/SysAPI/Scripts/SYS_STACKTRACE.cs").Dispose();
					
					StartCoroutine(DownloadDataFiles(_sourceURL[5], Application.dataPath + "/Plugins/SysAPI/Scripts/","SYS_STACKTRACE.cs"));
					Debug.LogWarning("[Sys API]: Downloading SYS_STACKTRACE.cs");
				}
			//Get Sys ScreenShot
				if(File.Exists(Application.dataPath + "/Plugins/SysAPI/Scripts/SYS_SCREENSHOT.cs")){
					File.Create(Application.dataPath + "/Plugins/SysAPI/Scripts/SYS_SCREENSHOT.cs").Dispose();
					
					StartCoroutine(DownloadDataFiles(_sourceURL[2], Application.dataPath + "/Plugins/SysAPI/Scripts/","SYS_SCREENSHOT.cs"));
					Debug.LogWarning("[Sys API]: Downloading SYS_SCREENSHOT.cs");
				}
			//Get Sys Misc.
				if(File.Exists(Application.dataPath + "/Plugins/SysAPI/Scripts/SYS_M.cs")){
					File.Create(Application.dataPath + "/Plugins/SysAPI/Scripts/SYS_M.cs").Dispose();
					
					StartCoroutine(DownloadDataFiles(_sourceURL[6], Application.dataPath + "/Plugins/SysAPI/Scripts/","SYS_M.cs"));
					Debug.LogWarning("[Sys API]: Downloading SYS_M.cs");
				}
				
				
				File.Create(Application.dataPath + "/Sys_API/Documentation.txt").Dispose();
				StartCoroutine(DownloadDataFiles(_docuURL,Application.dataPath + "/Sys_API/","Documentation.txt"));
				//File.Create(Application.dataPath + "/Plugins/SysAPI/Documentation.txt").Dispose();
				//StartCoroutine(DownloadDataFiles(_docuURL,Application.dataPath + "/Plugins/SysAPI/","Documentation.txt"));
				Debug.LogWarning("[Sys API]: Downloading 'Documentation.txt'");
				
				File.Create(Application.dataPath + "/Sys_API/change.log").Dispose();
				StartCoroutine(DownloadDataFiles(_changelogURL,Application.dataPath + "/Sys_API/","change.log"));
				Debug.LogWarning("[Sys API]: Downloading 'change.log'");
				
				File.Create(Application.dataPath + "/Editor/SystemInformationLogger.cs").Dispose();
				StartCoroutine(DownloadDataFiles(_silURL,Application.dataPath + "/Editor/","SystemInformationLogger.cs"));
				Debug.LogWarning("[Sys API]: Downloading 'SystemInformationLogger.cs'");
				
				File.Create(Application.dataPath + "/Sys_API/Examples/Sys_API_CSharp_Example.cs").Dispose();
				StartCoroutine(DownloadDataFiles(_sysCSURL,Application.dataPath + "/Sys_API/Examples/","Sys_API_CSharp_Example.cs"));
				Debug.LogWarning("[Sys API]: Downloading 'Sys_API_CSharp_Example.cs'");
				
				File.Create(Application.dataPath + "/Sys_API/Examples/Sys_API_JS_Example.js").Dispose();
				StartCoroutine(DownloadDataFiles(_sysJSURL,Application.dataPath + "/Sys_API/Examples/","Sys_API_JS_Example.js"));
				Debug.LogWarning("[Sys API]: Downloading 'Sys_API_JS_Example.js'");
				
				_currentVersion = newVC;
				UnityEditor.EditorPrefs.SetString("VC",_currentVersion);
			} else {
				Debug.LogWarning("[Sys API]: You already have the latest version." + " Sys API " + _currentVersion + ".");
				UnityEditor.EditorPrefs.SetString("VC",_currentVersion);
				Destroy(this.gameObject);
				UnityEditor.EditorApplication.isPlaying = false;
				Destroy(this.gameObject);
			}
		}
	}

	IEnumerator DownloadDataFiles(string url, string path, string fileName) {
		WWW www = new WWW(url);
		yield return www;
		string file = www.text;
		if(www.isDone){
			if(file.Length == 0 || file == null){UnityEditor.EditorApplication.isPlaying = false; Debug.LogError("[Sys API] ERROR000: Unable to establish connection. (EC-SUD-119)");} else {
				File.Create(path + fileName).Dispose();
				File.AppendAllText(path + fileName, file);
				Debug.LogWarning("[Sys API]: " + fileName + " successfully updated.");
				dlCount++;
				//Debug.Log(dlCount);
				if(dlCount >= 12){
					UnityEditor.EditorPrefs.SetString("VC",_currentVersion);
					Destroy(this.gameObject);
					UnityEditor.EditorApplication.isPlaying = false;
					Debug.LogWarning("[Sys API]: Updated to version " + _currentVersion);
					Destroy(this.gameObject);
				}
			}
		}
	}
	
	
	
	
#endif
}