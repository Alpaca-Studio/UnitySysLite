using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

namespace Sys {

	public class ScreenShot : MonoBehaviour {
	///SCREEN CAPTURING METHODS///
        public static void CaptureScreenshot(MonoBehaviour instance)
        {
            string path;
            path = (Application.persistentDataPath + "/" + Application.productName + "/Screenshots/" + System.DateTime.Now.ToString("MMddyyyy - hhmmss") + ".png");
            instance.StartCoroutine(GetTexture2D(path, false));
        }

        public static void CaptureScreenshot(MonoBehaviour instance, string path)
        {
            instance.StartCoroutine(GetTexture2D(path, false));
        }

        public static void CaptureScreenshot(MonoBehaviour instance, string path, bool openDir)
        {
            instance.StartCoroutine(GetTexture2D(path, openDir));
        }

        public static IEnumerator GetTexture2D(string path, bool openDir)
        {
            yield return new WaitForEndOfFrame();

            Texture2D tex = new Texture2D(Screen.width, Screen.height);
            tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            tex.Apply();
            HandleScreenshot(tex, path, openDir);
        }

        private static void HandleScreenshot(Texture2D tex, string path, bool openDir)
        {
            File.WriteAllBytes(path, tex.EncodeToPNG());
            if (openDir) { System.Diagnostics.Process.Start(path); }
        }

        public static Texture2D ScreenToTexture2D()
        {
            Texture2D tex = new Texture2D(Screen.width, Screen.height);
            tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            tex.Apply();
            return tex;
        }

        public static Texture2D LoadImageAtPath(string path)
        {
            Texture2D tex;
            byte[] bytes;
            bytes = File.ReadAllBytes(path);
            tex = new Texture2D(1, 1);
            tex.LoadImage(bytes);
            return tex;
        }
	
	}
	
}