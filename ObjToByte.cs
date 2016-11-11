
using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// 自定义对象存储为数据流
/// </summary>
public class ObjToByte : MonoBehaviour
{
	/// <summary>
	/// 将byte[]转换成string
	/// </summary>
	/// <param name="characters"></param>
	/// <returns></returns>
	public static string UTF8ByteArrayToString(byte[] mbyte)
	{
		UTF8Encoding encoding = new UTF8Encoding();
		string constructedString = encoding.GetString(mbyte);
		return (constructedString);
	}


	/// <summary>
	/// 将string转换成byte[]
	/// </summary>
	/// <param name="pXmlString"></param>
	/// <returns></returns>
	public static byte[] StringToUTF8ByteArray(string pXmlString)
	{
		UTF8Encoding encoding = new UTF8Encoding();
		byte[] byteArray = encoding.GetBytes(pXmlString);
		return byteArray;
	}


	/// <summary>
	/// 对象克隆
	/// </summary>
	/// <param name="obj"></param>
	/// <returns></returns>
	public static T ValueClone<T>(T obj)
	{


		BinaryFormatter bf = new BinaryFormatter();


		MemoryStream ms = new MemoryStream();


		bf.Serialize(ms, obj);


		ms.Seek(0, SeekOrigin.Begin);


		return (T)bf.Deserialize(ms);
	}


	/// <summary>
	/// 对象序列化为字节流
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="type"></param>
	/// <returns></returns>
	public static byte[] ToBytes<T>(T type)
	{
		BinaryFormatter bf = new BinaryFormatter();


		MemoryStream ms = new MemoryStream();
		bf.Serialize(ms, type);
		return ms.GetBuffer();
	}


	/// <summary>
	/// 字节流反序列化为对象
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="bytes"></param>
	/// <returns></returns>
	public static T BytesTo<T>(byte[] bytes)
	{
		BinaryFormatter bf = new BinaryFormatter();


		MemoryStream ms = new MemoryStream();
		ms.Write(bytes, 0, bytes.Length);
		return (T)bf.Deserialize(ms);


	}


	/// <summary>
	/// 对象转换为数据流存在本地
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="type"></param>
	/// <param name="path"></param>
	public static void SaveToLocal<T>(T type, string path,string name)
	{
		//.Rec为存储的文件后缀名可自己随便定义但读写的要一致！
		FileStream fs = new FileStream(path + name + ".Rec", FileMode.Create);
		fs = ToFileStream<T>(type, fs);
		fs.Close();
		fs.Dispose();
	}


	/// <summary>
	/// 本地数据流转换为对象
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="path"></param>
	/// <returns></returns>
	public static T ReadFromLocal<T>(string path,string name) 
	{
		try {
			FileStream fs = new FileStream(path + name + ".Rec", FileMode.Open);
			return FileStreamTo<T>(fs);
		} catch {
			return default(T);
		}
	}


	/// <summary>
	/// 对象序列化为文件流
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="type"></param>
	/// <param name="fs"></param>
	/// <returns></returns>
	private static FileStream ToFileStream<T>(T type, FileStream fs)
	{
		BinaryFormatter bf = new BinaryFormatter();
		bf.Serialize(fs, type);
		return fs;
	}


	/// <summary>
	/// 文件流反序列化为对象
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="fs"></param>
	/// <returns></returns>
	private static T FileStreamTo<T>(FileStream fs)
	{
		BinaryFormatter bf = new BinaryFormatter();
		T t = (T)bf.Deserialize(fs);
		fs.Close();
		fs.Dispose();
		return t;
	}
}