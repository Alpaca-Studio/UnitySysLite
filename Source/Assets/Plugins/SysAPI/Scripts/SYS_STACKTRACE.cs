using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
 namespace Sys {
	public class Trace : MonoBehaviour {
	///STACK TRACE METHODS///
		public static int GetLine(){
			System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(0,true);
			System.Diagnostics.StackFrame stackFrame = stackTrace.GetFrame(stackTrace.FrameCount -1);
			int line = stackFrame.GetFileLineNumber(); 
			return line;
		}
		
		public static string GetErrorStackTrace(){
			System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(0,true);
			System.Diagnostics.StackFrame stackFrame = stackTrace.GetFrame(stackTrace.FrameCount - 1);
			string file = stackFrame.GetFileName();
			//string method = stackFrame.GetMethod().ToString();
			int line = stackFrame.GetFileLineNumber();
			string trace = string.Format("({0}-{1}-{2})","EC",Path.GetFileName(file),line);			
			return trace;
		}
		public static string GetErrorStackTrace(int frames){
			System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(0,true);
			System.Diagnostics.StackFrame stackFrame = stackTrace.GetFrame(stackTrace.FrameCount + frames);
			string file = stackFrame.GetFileName();
			//string method = stackFrame.GetMethod().ToString();
			int line = stackFrame.GetFileLineNumber();
			string trace = string.Format("({0}-{1}-{2})","EC",Path.GetFileName(file),line);			
			return trace;
		}
		public static string GetStackTrace(){
			System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(0,true);
			System.Diagnostics.StackFrame stackFrame = stackTrace.GetFrame(stackTrace.FrameCount -1);
			string file = stackFrame.GetFileName();
			string method = stackFrame.GetMethod().ToString();
			int line = stackFrame.GetFileLineNumber();
			string trace = string.Format("{0}({1}:{2})",method,Path.GetFileName(file),line);			
			return trace;
		}
		public static string FormatStackTrace(string format){
			System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(0,true);
			System.Diagnostics.StackFrame stackFrame = stackTrace.GetFrame(stackTrace.FrameCount -1);
			string file = stackFrame.GetFileName();
			string method = stackFrame.GetMethod().ToString();
			int line = stackFrame.GetFileLineNumber();
			string trace = string.Format(format,method,Path.GetFileName(file),line);			
			return trace;
		}
		public static string FormatStackTrace(string format, bool getFileName, bool getMethodName, bool getLineNumber){
			System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(0,true);
			System.Diagnostics.StackFrame stackFrame = stackTrace.GetFrame(stackTrace.FrameCount -1);
			string file = stackFrame.GetFileName();
			string method = stackFrame.GetMethod().ToString();
			int line = stackFrame.GetFileLineNumber();
			string trace = null;
			//int index = 0; //[0 F,M,L] [1 F,M] [2 F,L] [3 M,L] [4 F] [5 L] [6 M] [7 ---]
			if(getFileName){
				if(getMethodName && getLineNumber){
					//index = 0;
					trace = string.Format(format,Path.GetFileName(file),method,line);
				}
				if(getMethodName && !getLineNumber){
					//index = 1;
					trace = string.Format(format,Path.GetFileName(file),method);
				}
				if(!getMethodName && getLineNumber){
					//index = 2;
					trace = string.Format(format,Path.GetFileName(file),line);
				}
				if(!getMethodName && !getLineNumber){
					//index = 4;
					trace = string.Format(format,Path.GetFileName(file));
				}
				return trace;
			} else {
				if(getMethodName && getLineNumber){
					//index = 3;
					trace = string.Format(format,method,line);
				}
				if(!getMethodName && getLineNumber){
					//index = 5;
					trace = string.Format(format,line);
				}
				if(getMethodName && !getLineNumber){
					//index = 6;
					trace = string.Format(format,method);
				}
				if(!getMethodName && !getLineNumber){
					//index = 7;
					Logging.LogError("[Sys API] Invalid Arguments. All cannot be false.",true);
					trace = "Error!";
				}
				return trace;
			}
		}
	}
 }