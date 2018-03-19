using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersionControlDownloader : MonoBehaviour {
#if UNITY_EDITOR
	string _versionURL = "https://raw.githubusercontent.com/Alpaca-Studio/UnitySysLite/master/Source/Assets/Plugins/SysAPI/vc.dxt"+Sys.M.URLAntiCache();
	string _path;
	int result; //0:No Update | 1: Update Available
	//List<string> VC = new List<string>();
	string _currentVersion;
	public bool forceUpdate = false;
	//int dlCount = 0;
	
	void Start () {
		//dlCount = 0;
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
			if(newVC != _currentVersion){
				result = 1;
				UnityEditor.EditorPrefs.SetInt("result",result);
				UnityEditor.EditorApplication.isPlaying = false;
				Destroy(this.gameObject);
			} else {
				result = 0;
				UnityEditor.EditorPrefs.SetInt("result",result);
				Debug.LogWarning("[Sys API]: You already have the latest version." + " Sys API " + _currentVersion + ".");
				UnityEditor.EditorPrefs.SetString("VC",_currentVersion);
				UnityEditor.EditorApplication.isPlaying = false;
				Destroy(this.gameObject);
			}
		}
	}
#endif
}