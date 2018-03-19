//---SYS_MASTER.cs v1.0.5--- Created by Alpaca Studio [ http://alpaca.studio ]//
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

namespace Sys
{
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

