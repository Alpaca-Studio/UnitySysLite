//---SYS_MASTER.cs v1.0.5--- Created by Alpaca Studio [ http://alpaca.studio ]//
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

namespace Sys
{
    public class Logging : MonoBehaviour
    {

        public static string temp;
        public static List<string> logRaw = new List<string>();
        public static List<string> logData = new List<string>();
        public static Dictionary<string, string> messages = new Dictionary<string, string>();
        public static string lastPath;

	///LOGGING METHODS///
        public static void addMessage(string code, string message)
        {
            messages.Add(code, message);
        }
		
		public static void Log (string message, bool showInConsole) {
			if(message != null){
				temp = message;
				if(showInConsole){Debug.Log(message);}
				Handle(temp);
			} else {
				LogError("[Sys API] ERROR003: Sys.Log(string, bool) has Invalid Arguments: (string) message cannot be null. "+Sys.Trace.GetErrorStackTrace(),true);
			}
		}

        public static void Log(string message)
        {
            Log(message, false);
        }

        public static void LogCode(string messageCode)
        {
            Log(messages[messageCode], false);
        }

        public static void LogCode(string messageCode, bool showInConsole)
        {
            Log(messages[messageCode], showInConsole);
        }

        public static void LogCode(string messageCode, MonoBehaviour M)
        {
            LogCode(messageCode, M, false);
        }

        public static void LogCode(string messageCode, MonoBehaviour M, bool showInConsole)
        {
            Log(messages[messageCode].ToString() + " from " + M.ToString() + " ID# " + M.GetInstanceID(), showInConsole);
        }

        public static void LogError(string message)
        {
            LogError(message, false);
        }
		
		public static void LogError (string message, bool showInConsole) {
			if(message != null){
				temp = message + "  -  " + Sys.Trace.GetErrorStackTrace(-1);
				if(showInConsole){Debug.LogError(message);}
				HandleError(temp);
			} else {
				LogError("[Sys API] ERROR003: Sys.LogError(string, bool) has Invalid Arguments: (string) message cannot be null. "+Sys.Trace.GetErrorStackTrace(),true);
			}
		}
        private static void Handle(string message)
        {
            string output = "[" + System.DateTime.Now + "]: " + message;
            logData.Add(output);
            logRaw.Add(message);
        }
		
		 private static void HandleError(string message)
        {
            string output = "[" + System.DateTime.Now + "]: ERROR!!! " + message;
            logData.Add(output);
            logRaw.Add(message);
        }

        // Deletes all entries in the log file
        public static void ClearLog()
        {
            if (lastPath == null) { lastPath = Application.persistentDataPath + "/" + Application.productName + "/Logs/SysLog.txt"; }
            if (!Directory.Exists(System.IO.Path.GetDirectoryName(lastPath)))
            {
                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(lastPath));
                File.Create(lastPath).Dispose();
            }
            else
            {
                if (!File.Exists(lastPath))
                {
                    File.Create(lastPath).Dispose();
                }
                else
                {
                    File.WriteAllLines(lastPath, new string[] { "" });
                }
            }
            logData.Clear();
            logRaw.Clear();
        }

        // Save the log to its location
        public static void SaveLog()
        {
            //logData.Clear();
            string path = Application.persistentDataPath + "/" + Application.productName + "/Logs/SysLog.txt";

            foreach (string message in logData)
            {
                File.AppendAllText(path, string.Format("{0}{1}", message.ToString(), System.Environment.NewLine));
            }
            lastPath = path;
            Debug.Log("[Sys API] SysLog.txt saved to default location: " + (Application.persistentDataPath + "/" + Application.productName + "/Logs/SysLog.txt"));
        }

        // Save the log to the given file path
        public static void SaveLog(string path)
        {
			if(path != null){
            //logData.Clear();
            foreach (string message in logData)
            {
                File.AppendAllText(path, string.Format("{0}{1}", message.ToString(), System.Environment.NewLine));
            }
            lastPath = path;
            Log("[Sys API] SysLog.txt saved to location: " + path,true);
			} else {
				LogError("[Sys API] ERROR004: Path is null or empty. (EC-SYS-"+Sys.Trace.GetLine()+")",true);
			}
        }

        // Save the log to the given file path, specifying whether or not to open the directory
        public static void SaveLog(string path, bool openDir)
        {
			if(path != null){
				//logData.Clear();
				foreach (string message in logData)
				{
					File.AppendAllText(path, string.Format("{0}{1}", message.ToString(), System.Environment.NewLine));
				}
				if (openDir) { System.Diagnostics.Process.Start(path); }
				lastPath = path;
				Log("[Sys API] SysLog.txt saved to location: " + path, true);
				Log("[Sys API] Opening Directory: " + path, true);
			} else {
				LogError("[Sys API] ERROR004: Path is null or empty. (EC-SYS-"+Sys.Trace.GetLine()+")",true);
			}
        }
	}
    

    
	public class M : MonoBehaviour {	
	///MISC METHODS///
		public static string URLAntiCache(){ 
			return "?t="+System.DateTime.Now.ToString("MMddyyyyhhmmss");
		}
		
        public static string GenerateUniqueID(int length)
        {
            int a = 0;
            string hc = "";

            while (a < length)
            {
                float r = UnityEngine.Random.Range(0.0f, 2);
                Debug.Log(r);
                if (r <= 0.999f)
                {
                    int v = GetValue();
                    hc += v.ToString();
                }
                if (r >= 1)
                {
                    string c = GetCharacter();
                    hc += c;
                }
                a++;
            }

            return hc;
        }

        private static int GetValue()
        {
            int val = UnityEngine.Random.Range(0, 9);
            return val;
        }

        private static string GetCharacter()
        {
            string[] alp = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            string returnChar = alp[UnityEngine.Random.Range(0, alp.Length)];
            return returnChar;
        }
    }
}

