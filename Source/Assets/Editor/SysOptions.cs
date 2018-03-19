using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
public class SysOptions : EditorWindow {
	[MenuItem("Tools/Sys API/Options %_w")]
	public static void ShowWindow () {
		EditorWindow.GetWindow(typeof(SysOptions));
	}
	[MenuItem("Tools/Sys API/Examples")]
	public static void GenerateExample () {
		GameObject newObj = new GameObject("SYS");
		newObj.AddComponent<Sys_API_CSharp_Example>();
		newObj.AddComponent<Sys_API_JS_Example>();
	}
	[MenuItem("Tools/Sys API/Check For Updates")]
	public static void CheckForUpdates () {
		EditorApplication.isPlaying = true;
		GameObject newObj = new GameObject("SysOptions");
		newObj.AddComponent<SysUpdateDownloader>();
	}
	[MenuItem("Tools/Sys API/Wiki Documentation")]
	public static void OpenWebpage () {
		Application.OpenURL("https://github.com/Alpaca-Studio/UnitySysAPI/wiki");
	}
	
	string _path;
	string _docPath;
	//List<string> VC = new List<string>();
	string _currentVersion;
	string _documentation;
	bool _showDocumentation;
	string _docuLabel = "Show Documentation";
	[SerializeField]
	bool _checkForUpdates;
	Vector2 _scrollPosition;
	bool _addLibraries;
	int _updateResult;
	bool _showUpdateGUI;
	GameObject obj;
	
	void OnEnable () {
		_path = Application.dataPath + "/Plugins/SysAPI/";
		_docPath = Application.dataPath + "/Sys_API/";
		if(!Directory.Exists(_path)){
			Directory.CreateDirectory(_path);
			File.Create(_path+"vc.dxt").Dispose();
			_currentVersion = "1.0.2";
			File.WriteAllText(_path+"vc.dxt", _currentVersion);
			//System.Diagnostics.Process.Start(_path);
		} else {
			if(File.Exists(_path+"vc.dxt")){
				_currentVersion = File.ReadAllLines(_path+"vc.dxt")[0];
			} else {
				File.Create(_path+"vc.dxt").Dispose();
				_currentVersion = "1.0.2";
				File.WriteAllText(_path+"vc.dxt", _currentVersion);
			}
		}
		
		_documentation = "";
		List<string> docu = new List<string>(File.ReadAllLines(_docPath+"Documentation.txt"));
		for(int i = 0; i < docu.Count; i++){
			if(i==0){
				_documentation += "<b>"+docu[i]+"</b> \n";
			}
			if(i!=0){
				_documentation += docu[i]+"\n";
			}
		}
	}
	void OnFocus () {
		if(EditorPrefs.HasKey("result")){_updateResult=EditorPrefs.GetInt("result");} else { EditorPrefs.SetInt("result",0);}
		if(EditorPrefs.HasKey("VC")){
			_currentVersion = EditorPrefs.GetString("VC");
			if(EditorApplication.isPlaying == false){
				foreach( var gObj in FindObjectsOfType(typeof(GameObject)) as GameObject[]){
					if(gObj.name == "SysOptions"){
						DestroyImmediate(gObj);
					}
				}
			}
		}	
	}
	void OnLostFocus () {
		EditorPrefs.SetString("VC",_currentVersion);
		
		_documentation = "";
		List<string> docu = new List<string>(File.ReadAllLines(_docPath+"Documentation.txt"));
		for(int i = 0; i < docu.Count; i++){
			if(i==0){
				_documentation += "<b>"+docu[i]+"</b> \n";
			}
			if(i!=0){
				_documentation += docu[i]+"\n";
			}
		}
		
		if(EditorApplication.isPlaying == false){
				foreach( var gObj in FindObjectsOfType(typeof(GameObject)) as GameObject[]){
					if(gObj.name == "SysOptions"){
						DestroyImmediate(gObj);
					}
				}
		}
	}
	void OnDestroy () {
		EditorPrefs.SetString("VC",_currentVersion);
		DestroyImmediate(GameObject.Find("SysOptions"));
	}
	void OnGUI () {
		GUIStyle helpBox = GUI.skin.GetStyle("HelpBox");
		helpBox.richText = true;
		
		EditorGUILayout.Space();
		GUILayout.Label("Sys API v" + _currentVersion+ " Editor Options", EditorStyles.boldLabel);
		using (var horizontalScope = new GUILayout.HorizontalScope("box"))
        {
		////GUILayout.BeginHorizontal();
		if(GUILayout.Button("Check For Updates", EditorStyles.miniButton)){
			EditorPrefs.SetString("VC",_currentVersion);
			EditorApplication.isPlaying = true;
			GameObject newObj = new GameObject("SysOptions");
			newObj.AddComponent<VersionControlDownloader>();
			_showUpdateGUI = true;
		}
		if(GUILayout.Button("Force Update", EditorStyles.miniButton)){
			EditorPrefs.SetString("VC",_currentVersion);
			EditorApplication.isPlaying = true;
			GameObject newObj = new GameObject("SysOptions");
			newObj.AddComponent<SysUpdateDownloader>();
			newObj.GetComponent<SysUpdateDownloader>().forceUpdate = true;
		}
		if(GUILayout.Button("Examples", EditorStyles.miniButton)){
			GameObject newObj = new GameObject("SYS");
			newObj.AddComponent<Sys_API_CSharp_Example>();
			newObj.AddComponent<Sys_API_JS_Example>();
		}	
		////GUILayout.EndHorizontal();
		////}
		
		////EditorGUILayout.Space();
		////GUILayout.BeginHorizontal();
		if(GUILayout.Button("Add Libraries", EditorStyles.miniButton)){ _addLibraries = !_addLibraries; _showDocumentation =false;}
		////EditorGUILayout.Space(); 
		if(GUILayout.Button(_docuLabel, EditorStyles.miniButton)){ _showDocumentation = !_showDocumentation; _addLibraries =false;}
		////GUILayout.EndHorizontal();
		////EditorGUILayout.Space();
		}
		
		if(_addLibraries){
			using (var horizontalScope = new GUILayout.HorizontalScope("box"))
			{
				GUILayout.BeginHorizontal(); EditorGUILayout.Space();
				if(GUILayout.Button("SYS_LOGGING", EditorStyles.miniButton)){ File.Create(Application.dataPath + "/Plugins/SysAPI/Scripts/SYS_LOGGING.cs").Dispose(); Debug.LogWarning("[Sys API]: Downloading Sys.Logging Library" ); CheckForUpdates();}
				EditorGUILayout.Space(); 
				if(GUILayout.Button("SYS_IO", EditorStyles.miniButton)){ File.Create(Application.dataPath + "/Plugins/SysAPI/Scripts/SYS_IO.cs").Dispose(); Debug.LogWarning("[Sys API]: Downloading Sys.IO Library" ); CheckForUpdates();}
				EditorGUILayout.Space(); 
				if(GUILayout.Button("SYS_INFO", EditorStyles.miniButton)){ File.Create(Application.dataPath + "/Plugins/SysAPI/Scripts/SYS_INFO.cs").Dispose(); Debug.LogWarning("[Sys API]: Downloading Sys.Info Library" ); CheckForUpdates();}
				EditorGUILayout.Space(); GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
				if(GUILayout.Button("SYS_MATH", EditorStyles.miniButton)){ File.Create(Application.dataPath + "/Plugins/SysAPI/Scripts/SYS_MATH.cs").Dispose(); Debug.LogWarning("[Sys API]: Downloading Sys.Math Library" ); CheckForUpdates();}
				EditorGUILayout.Space(); 
				if(GUILayout.Button("SYS_SCREENSHOT", EditorStyles.miniButton)){ File.Create(Application.dataPath + "/Plugins/SysAPI/Scripts/SYS_SCREENSHOT.cs").Dispose(); Debug.LogWarning("[Sys API]: Downloading Sys.ScreenShot Library" ); CheckForUpdates();}
				EditorGUILayout.Space(); 
				if(GUILayout.Button("SYS_STACKTRACE", EditorStyles.miniButton)){ File.Create(Application.dataPath + "/Plugins/SysAPI/Scripts/SYS_STACKTRACE.cs").Dispose(); Debug.LogWarning("[Sys API]: Downloading Sys.Trace Library" ); CheckForUpdates();}
				GUILayout.EndHorizontal();
			}
		}
		EditorGUILayout.Space(); 
		if(_showUpdateGUI){
			if(_updateResult == 0){
				GUILayout.Label("You have the latest version ["+_currentVersion+"]");
				EditorGUILayout.Space();
				if(GUILayout.Button("Close", EditorStyles.miniButton)){_showUpdateGUI=false;}
			} else {
				GUILayout.Label("An update is available, would you like to download this now?");
				GUILayout.BeginHorizontal();
				if(GUILayout.Button("Yes", EditorStyles.miniButton)){
					EditorApplication.isPlaying = true;
					GameObject newObj = new GameObject("SysOptions");
					newObj.AddComponent<SysUpdateDownloader>();
					newObj.GetComponent<SysUpdateDownloader>().forceUpdate = true;
				}
				EditorGUILayout.Space();
				if(GUILayout.Button("No", EditorStyles.miniButton)){_showUpdateGUI=false;}
				GUILayout.EndHorizontal();
			}
		}
		GUILayout.BeginHorizontal(); EditorGUILayout.Space(); 
		//if(GUILayout.Button(_docuLabel, EditorStyles.miniButton)){ _showDocumentation = !_showDocumentation; }
		EditorGUILayout.Space(); GUILayout.EndHorizontal();
			EditorGUILayout.Space();
				EditorGUILayout.Space();
		//_showDocumentation = EditorGUILayout.Foldout(_showDocumentation, _docuLabel);
		if(_showDocumentation){
				_scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition,false,false);
				EditorGUILayout.LabelField(_documentation, helpBox);
				EditorGUILayout.EndScrollView();
				_docuLabel = "Hide Documentation";
		} else { _docuLabel = "Show Documentation"; }
		EditorGUILayout.Space();		
	}
	
}
#endif