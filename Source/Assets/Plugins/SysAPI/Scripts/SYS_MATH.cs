using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

namespace Sys {
	public class Math : MonoBehaviour {
	///ARITHMETIC METHODS///
        public static int Add(params int[] a)
        {
            int sum = 0;

            for (int k = 0; k < a.Length; k++)
            {
                sum += a[k];
            }
            return sum;
        }

        public static float Add(params float[] a)
        {
            float sum = 0;

            for (int k = 0; k < a.Length; k++)
            {
                sum += a[k];
            }
            return sum;
        }

        public static int Subtract(params int[] a)
        {
            int sum = a.Length > 0 ? a[0] : 0;

            for (int k = 1; k < a.Length; k++)
            {
                sum -= a[k];
            }
            return sum;
        }

        public static float Subtract(params float[] a)
        {
            float sum = a.Length > 0 ? a[0] : 0;

            for (int k = 1; k < a.Length; k++)
            {
                sum -= a[k];
            }
            return sum;
        }

        public static int Multiply(params int[] a)
        {
            int product = 1;

            for (int k = 0; k < a.Length; k++)
            {
                product *= a[k];
            }
            return product;
        }

        public static float Multiply(params float[] a)
        {
            float product = 1;

            for (int k = 0; k < a.Length; k++)
            {
                product *= a[k];
            }
            return product;
        }

        public static float Divide(float a, float b)
        {
            float sum = (a / b);
            return sum;
        }

        private static int get(int[] d, int index)
        {
            return index >= 0 && index < d.Length ? d[index] : 0;
        }

        public static int[] convolve(int[] a, int[] b)
        {
            int[] c = new int[] { a.Length > b.Length ? a.Length : b.Length };

            for (int x = 0; x < c.Length; x++)
            {
                int n = 0;

                for (int k = 0; k <= x; k++)
                {
                    n += get(a, k) * get(b, x - k);
                }
                c[x] = n;
            }
            return c;
        }

        public static int[] cProduct(int[] a, int[] b)
        {
            int[] c = new int[] { a.Length > b.Length ? a.Length : b.Length };

            for (int x = 0; x < c.Length; x++)
            {
                int n = 0;

                for (int k = 0; k <= x; k++)
                {
                    n += get(a, k) * get(b, x - k);
                }
                c[x] = n;
            }
            return c;
        }
	}
}