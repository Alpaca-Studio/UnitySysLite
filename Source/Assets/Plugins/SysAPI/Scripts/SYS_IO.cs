//---SYS_MASTER.cs v1.0.5--- Created by Alpaca Studio [ http://alpaca.studio ]//
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

namespace Sys {
    public class IO : MonoBehaviour
    {
        public static string temp;
        public static string lastPath;
		
	///FILE PERSISTANCE///
		//Creates a new directory
		private static void CreateDirectory(string path){ 
			Directory.CreateDirectory(path);
			Sys.Logging.Log("[Sys API] Directory: "+path+" sucessfully created. "+Sys.Trace.GetStackTrace());
		}
		//Checks if a directory exists
		public static bool DirectoryCheck (string path){
			if(Directory.Exists (path)){return true;} 
			else { return false;} 
		}
		//Checks if a directory exists, and creates the directory if not.
		public static bool DirectoryCheck (string path, bool createDirectory){
				if(Directory.Exists (path)){
					return true;
				} else { 
					if (createDirectory){ 
						CreateDirectory(path);
						if(Directory.Exists(path)){
							Sys.Logging.Log("[Sys API] Directory: "+path+" sucessfully created. "+Sys.Trace.GetStackTrace(),true);
						}
					}
					return true;
				} 
		} 
		//Creates an empty file at a given path.
		private static void CreateEmptyFile(string path){ 
				File.Create(path).Dispose();
		}
			private static void CreateEmptyFile(string path, string file){ 
					File.Create(path+file).Dispose();
			}
		//Checks if a file already exists
		public static bool FileCheck (string path){
			if(File.Exists (path)){return true;} 
			else { return false;} 
		}
			public static bool FileCheck (string path, string file){
				if(File.Exists (path+file)){return true;} 
				else { return false;} 
			}
		//Checks if a file already exists, if not an empty file will be created.
		public static bool FileCheck (string path, bool createDispose){
				if(File.Exists (path)){
					return true;
				} else { 
					if (createDispose){ 
						CreateEmptyFile(path);
						if(File.Exists(path)){
							Sys.Logging.Log("[Sys API] File: "+Path.GetFileName(path)+" sucessfully created. "+Sys.Trace.GetStackTrace(),true);
						}
						return true;
					} else {
						return false;
					}
				} 
		}
			public static bool FileCheck (string path, string file, bool createDispose){
				string newPath = path+file;
					if(File.Exists (newPath)){
						return true;
					} else { 
						if (createDispose){ 
							CreateEmptyFile(newPath);
							if(File.Exists(newPath)){
								Sys.Logging.Log("[Sys API] File: "+Path.GetFileName(newPath)+" sucessfully created. "+Sys.Trace.GetStackTrace(),true);
							}
							return true;
						} else {
							return false;
						}
					} 
			}
		//Returns the external storage location per platform.
		public static string DeviceExternalStorage(){
			string dir = "";
			#if UNITY_EDITOR
				dir = Application.persistentDataPath;
			#endif
			#if UNITY_ANDROID && !UNITY_EDITOR
				dir = "/storage/emulated/0";
			#endif
			#if UNITY_IOS && !UNITY_EDITOR
				dir = Application.persistentDataPath;
			#endif
			return dir;
		}

        // File Saving Methods//
        public static void SaveDataToFile(string path, string[] data)
        {
			if(path != null){
				if (!File.Exists(path))
				{
					File.Create(path).Dispose();
					foreach (string d in data)
					{
						File.AppendAllText(path, string.Format("{0}{1}", d.ToString(), System.Environment.NewLine));
					}
				}
				else
				{
					File.Create(path).Dispose();
					foreach (string d in data)
					{
						File.AppendAllText(path, string.Format("{0}{1}", d.ToString(), System.Environment.NewLine));
					}
				}
			} else {
				Sys.Logging.LogError("[Sys API] ERROR004: Path is null or empty. (EC-SYS-"+Sys.Trace.GetLine()+")",true);
			}
        }

        public static void SaveFile(string path, string[] data)
        {
			if(path != null){
				if (!File.Exists(path))
				{
					File.Create(path).Dispose();
					foreach (string d in data)
					{
						File.AppendAllText(path, string.Format("{0}{1}", d.ToString(), System.Environment.NewLine));
					}
				}
				else
				{
					File.Create(path).Dispose();
					foreach (string d in data)
					{
						File.AppendAllText(path, string.Format("{0}{1}", d.ToString(), System.Environment.NewLine));
					}
				}
			} else {
				Sys.Logging.LogError("[Sys API] ERROR004: Path is null or empty. (EC-SYS-"+Sys.Trace.GetLine()+")",true);
			}
        }
        // Save the given data to the given file, specifying whether to append or overwrite
        public static void SaveDataToFile(string path, string[] data, bool clearOldFiles)
        {
			if(path != null){
				if (!File.Exists(path))
				{
					File.Create(path).Dispose();
					foreach (string d in data)
					{
						File.AppendAllText(path, string.Format("{0}{1}", d.ToString(), System.Environment.NewLine));
					}
				}
				else
				{
					if (clearOldFiles)
					{
						File.Create(path).Dispose();
						foreach (string d in data)
						{
							File.AppendAllText(path, string.Format("{0}{1}", d.ToString(), System.Environment.NewLine));
						}
					}
					else
					{
						foreach (string d in data)
						{
							File.AppendAllText(path, string.Format("{0}{1}", d.ToString(), System.Environment.NewLine));
						}
					}
				}
			} else {
				Sys.Logging.LogError("[Sys API] ERROR004: Path is null or empty. (EC-SYS-"+Sys.Trace.GetLine()+")",true);
			}
        }
		
        //'SaveDataToFile' Alternate
        public static void SaveFile(string path, string[] data, bool clearOldFiles)
        {
			if(path != null){
				if (!File.Exists(path))
				{
					File.Create(path).Dispose();
					foreach (string d in data)
					{
						File.AppendAllText(path, string.Format("{0}{1}", d.ToString(), System.Environment.NewLine));
					}
				}
				else
				{
					if (clearOldFiles)
					{
						File.Create(path).Dispose();
						foreach (string d in data)
						{
							File.AppendAllText(path, string.Format("{0}{1}", d.ToString(), System.Environment.NewLine));
						}
					}
					else
					{
						foreach (string d in data)
						{
							File.AppendAllText(path, string.Format("{0}{1}", d.ToString(), System.Environment.NewLine));
						}
					}
				}
			} else {
				Sys.Logging.LogError("[Sys API] ERROR004: Path is null or empty. (EC-SYS-"+Sys.Trace.GetLine()+")",true);
			}
        }

        // Load data from a text file to a string array.
        public static string[] LoadDataFromFile(string path)
        {
			if(path != null){
				string[] data = File.ReadAllLines(path);
				return data;
			} else {
				Sys.Logging.LogError("[Sys API] ERROR004: Path is null or empty. (EC-SYS-"+Sys.Trace.GetLine()+")",true);
				return null;
			}
        }
		//'LoadDataFromFile' Alternate
        public static string[] ReadFile(string path)
        {
			if(path != null){
				string[] data = File.ReadAllLines(path);
				return data;
			} else {
				Sys.Logging.LogError("[Sys API] ERROR004: Path is null or empty. (EC-SYS-"+Sys.Trace.GetLine()+")",true);
				return null;
			}
        }
		// Load data from a text file to a string list.
        public static List<string> LoadDataListFromFile(string path)
        {
			if(path != null){
				List<string> data = new List<string>(File.ReadAllLines(path));
				return data;
			} else {
				Sys.Logging.LogError("[Sys API] ERROR004: Path is null or empty. (EC-SYS-"+Sys.Trace.GetLine()+")",true);
				return null;
			}
        }
	}
}
