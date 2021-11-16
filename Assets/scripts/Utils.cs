﻿ ﻿using UnityEngine;
 using System.Collections;
 using System.Collections.Generic;
 
 public static class Utils {
    public static float GetRandomFloatBetween(float a, float b)
    {
        return (float)Random.Range(a*10, b * 10) / 10;
    }
    public static void RemoveAllChildsIn(Transform container)
     {
         int num = container.transform.childCount;
         for (int i = 0; i < num; i++) UnityEngine.Object.DestroyImmediate(container.transform.GetChild(0).gameObject);
     }
     public static void Shuffle(List<int> texts)
     {
         if (texts.Count < 2) return;
         for (int a = 0; a < 100; a++)
         {
             int id = Random.Range(1, texts.Count);
             int value1 = texts[0];
            int value2 = texts[id];
             texts[0] = value2;
             texts[id] = value1;
         }
     }
    public static void Shuffle(AudioClip[] arr)
    {
        if (arr.Length < 2) return;
        for (int a = 0; a < 100; a++)
        {
            int id = Random.Range(1, arr.Length);
            AudioClip value1 = arr[0];
            AudioClip value2 = arr[id];
            arr[0] = value2;
            arr[id] = value1;
        }
    }
    public static class CoroutineUtil
	{
		public static IEnumerator WaitForRealSeconds(float time)
		{
			float start = Time.realtimeSinceStartup;
			while (Time.realtimeSinceStartup < start + time)
			{
				yield return null;
			}
		}
	}
	public static string FormatNumbers(int num)
	{
        return ToFormattedString(num);

        return string.Format ("{0:#,#}",  num);
	}
    public static string SetFirstLetterToUpper(string text)
    {
        if(text == null || text.Length == 0)
            return "";
        return char.ToUpper(text[0]) +
            ((text.Length > 1) ? text.Substring(1).ToLower() : string.Empty);
    }
    public static string ToFormattedString(this double rawNumber)
    {
        string[] letters = new string[] { "", "K", "M", "B", "T", "P", "E", "Z", "Y", "KY", "MY", "BY", "TY", "PY", "EY", "ZY", "YY" };
        int prefixIndex = 0;
        while (rawNumber > 1000)
        {
            rawNumber /= 1000.0f;
            prefixIndex++;
            if (prefixIndex == letters.Length - 1)
            {
                break;
            }
        }
        string numberString = rawNumber.ToString();
        if (prefixIndex < letters.Length - 1)
        {
            numberString = ToThreeDigits(numberString);
        }

        string prefix = letters[prefixIndex];
        return $"{numberString}{prefix}";
    }
    private static string ToThreeDigits(string numString)
    {
        if (numString.Length > 4)
        {
            if (numString.Substring(0, 4).Contains("."))
                numString = numString.Substring(0, 5);
            else
                numString = numString.Substring(0, 4);
        }
        return numString;
    }
    public static string Md5Sum(string strToEncrypt)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }
    public static void Shuffle<T>(List<T> list)
    {
        System.Random _random = new System.Random();
        int n = list.Count;
        for (int i = 0; i < n; i++)
        {
            // Use Next on random instance with an argument.
            // ... The argument is an exclusive bound.
            //     So we will not go past the end of the array.
            int r = i + _random.Next(n - i);
            T t = list[r];
            list[r] = list[i];
            list[i] = t;
        }
    }

    public static void Shuffle<T>(T[] array)
    {
        System.Random _random = new System.Random();
        int n = array.Length;
        for (int i = 0; i < n; i++)
        {
            // Use Next on random instance with an argument.
            // ... The argument is an exclusive bound.
            //     So we will not go past the end of the array.
            int r = i + _random.Next(n - i);
            T t = array[r];
            array[r] = array[i];
            array[i] = t;
        }
    }
}
