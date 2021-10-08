using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Sockets;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public static class Utils {

    public static void RemoveAllChildsIn(Transform container)
    {
        int num = container.transform.childCount;
        for (int i = 0; i < num; i++) UnityEngine.Object.DestroyImmediate(container.transform.GetChild(0).gameObject);
    }
	/*public static string SetFormatedNumber(string n)
	{
		int i = n.Length;
		int num_id = 0;
		string returnString = "";
		while (i > 0) {
			char c = n[i - 1];
			if (num_id >= 3) {
				num_id = 0;
				returnString = c + "." + returnString;
			} else {				
				returnString = c + returnString;
			}
			num_id++;
			i--;
		}
		return returnString;
	}*/
    public static string UnformatString(string value)
    {
        return value.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
    }
	public static string SetFormatedNumber(string n){
		string[] arr = n.Split (',');

		string returnString = "";
		for (int i = 1; i < arr[0].Length+1; i++) {
			if (i%3 == 0 && i!=arr[0].Length) {				
				returnString = "." + arr[0][arr[0].Length-i] + returnString;
			} else {				
				returnString = arr[0][arr[0].Length-i] + returnString;
			}
		}
		if (arr [0].Length < 1)
			returnString = "0";
		if (arr.Length > 1)
			returnString += ","+arr [1];
		return returnString;
	}

	public static void Shuffle<T>(List<T> list){
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

	public static void Shuffle<T>(T[] array){
		System.Random _random = new System.Random ();
		int n = array.Length;
		for (int i = 0; i < n; i++) {
			// Use Next on random instance with an argument.
			// ... The argument is an exclusive bound.
			//     So we will not go past the end of the array.
			int r = i + _random.Next (n - i);
			T t = array [r];
			array [r] = array [i];
			array [i] = t;
		}
	}

	static string int2Hex(int c) {
			string hex = c.ToString("X2");
			return hex.Length == 1 ? "0" + hex : hex;
	}

	public static string rgb2Hex(float r, float g, float b) {
		return rgb2Hex ((int)(r*255), (int)(g*255), (int)(b*255));
	}

	public static string rgb2Hex(int r, int g, int b) {
			return "#" + int2Hex(r) + int2Hex(g) + int2Hex(b);
	}

    public static string CSV2JSON(string csv, char delimiter) {
        string[] lines = csv.Split('\n');
        string[] keys = lines[0].Split(delimiter);
        string json = "[";
        //Debug.Log (csv);
        for (int i = 1; i < lines.Length; i++) {
            string[] vals = lines[i].Split(delimiter);
            if (vals[0] != "") {
                if (i > 1)
                    json += ",";
                json += "{";
                for (int j = 0; j < vals.Length-1; j++) {
                    json += keys[j] + ":" + vals[j];
                    if (j < keys.Length - 2)
                        json += ",";
                }
                json += "}";
            }
        }
        json += "]";
        //Debug.Log (json);
        return json;
    }

    public static string CSV2JSON(string csv, char delimiter, string matchArray) {

        string[] lines = csv.Split('\n');
        string[] keys = lines[0].Split(delimiter);
        string json = "[";
        //Debug.Log (csv);
        for (int i = 1; i < lines.Length; i++) {
            string[] vals = lines[i].Split(delimiter);
            if (vals[0] != "") {
                if (i > 1)
                    json += ",";
                json += "{";
                string opt = "[";
                for (int j = 0; j < keys.Length - 1; j++) {
                    //Debug.Log(keys[j] + ":" + vals[j]);
                    //if (!Regex.Match(keys[j], @"opcion\d").Success) {
                    if (!Regex.Match(keys[j], @matchArray + "\\d").Success) {
                        //if(!keys[j].Contains("opcion"))
                        if (!keys[j].Contains("\""))
                            json += "\"" + keys[j] + "\":\"" + vals[j] + "\"";
                        else
                            json += keys[j] + ":" + vals[j];
                        if (j < keys.Length - 1)
                            json += ",";
                    } else if (vals[j] != "\"-\"" && vals[j] != "-" && vals[j] != "" && vals[j] != "\"\"") {
                        if (!vals[j].Contains("\""))
                            opt += "\""+vals[j] + "\",";
                        else
                            opt += vals[j] + ",";
                    }
                }
                opt = opt.Substring(0, opt.Length - 1);
                opt += "]";
                json += "\"opciones\":" + opt + "}";
            }
        }
        json += "]";
        //Debug.Log (json);
        return json;
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

    public static System.DateTime NetworkTime() {
        return NetworkTime(-3);
    }

    public static System.DateTime NetworkTime(int utc, int index=0) {
        //default Windows time server
        string[] ntpServer = { "time.windows.com", "time.nist.gov","time-nw.nist.gov", "time-a.nist.gov", "time-b.nist.gov" };
        //const string ntpServer = "time.nist.gov";

        // NTP message size - 16 bytes of the digest (RFC 2030)
        var ntpData = new byte[48];

        //Setting the Leap Indicator, Version Number and Mode values
        ntpData[0] = 0x1B; //LI = 0 (no warning), VN = 3 (IPv4 only), Mode = 3 (Client Mode)

        var addresses = Dns.GetHostEntry(ntpServer[index]).AddressList;

        //The UDP port number assigned to NTP is 123
        var ipEndPoint = new IPEndPoint(addresses[0], 123);
        //NTP uses UDP
        var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        try {
            socket.Connect(ipEndPoint);

            //Stops code hang if NTP is blocked
            socket.ReceiveTimeout = 3000;

            socket.Send(ntpData);
            socket.Receive(ntpData);
            socket.Close();

            //Offset to get to the "Transmit Timestamp" field (time at which the reply 
            //departed the server for the client, in 64-bit timestamp format."
            const byte serverReplyTime = 40;

            //Get the seconds part
            ulong intPart = System.BitConverter.ToUInt32(ntpData, serverReplyTime);

            //Get the seconds fraction
            ulong fractPart = System.BitConverter.ToUInt32(ntpData, serverReplyTime + 4);

            //Convert From big-endian to little-endian
            intPart = SwapEndianness(intPart);
            fractPart = SwapEndianness(fractPart);

            double milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
            milliseconds += utc * 60 * (60 * 1000);
            //**UTC** time
            var networkDateTime = (new System.DateTime(1900, 1, 1, 0, 0, 0, System.DateTimeKind.Utc)).AddMilliseconds((long)milliseconds);

            //return networkDateTime.ToLocalTime();
            return networkDateTime;
        }
        // Manage of Socket's Exceptions
        catch (ArgumentNullException ane) {
            Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
        } catch (SocketException se) {
            Console.WriteLine("SocketException : {0}", se.ToString());
        } catch (Exception e) {
            Console.WriteLine("Unexpected exception : {0}", e.ToString());
        }

        index++;
        if (index < ntpServer.Length)
            return NetworkTime(utc, index + 1);
        else
            return DateTime.Now;
    }

    // stackoverflow.com/a/3294698/162671
    static uint SwapEndianness(ulong x) {
        return (uint)(((x & 0x000000ff) << 24) +
                       ((x & 0x0000ff00) << 8) +
                       ((x & 0x00ff0000) >> 8) +
                       ((x & 0xff000000) >> 24));
    }


}
