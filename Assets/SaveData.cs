using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using System.Linq;

public static class SaveData {

    public static void SaveToJSON<T>(List<T> toSave, string filename) {
        Debug.Log(GetPath(filename));
        string content = JsonHelper.ToJson(toSave.ToArray(), true);
        WriteFile(GetPath(filename), content);
    }
    public static List<T> ReadFromJSON<T>(string filename) {
        string content = ReadFile(GetPath(filename));
        if (string.IsNullOrEmpty(content) || content == "{}") {
            return new List<T>();
        }
        List<T> res = JsonHelper.FromJson<T>(content).ToList();
        return res;
    }
    private static string GetPath(string filename) {
        Debug.Log("Save location: " + Application.persistentDataPath + "/Saves/" + filename);
        return Application.persistentDataPath + "/Saves/" + filename;
	}
    private static void WriteFile(string path, string content) {
        FileStream fileStream = new FileStream(path, FileMode.Create);
		using StreamWriter writer = new StreamWriter(fileStream); writer.Write(content);
    }
    private static string ReadFile(string path) {
		if (File.Exists(path)) {
			using StreamReader reader = new StreamReader(path); string content = reader.ReadToEnd();
			return content;
		}
		else {
            return "";
		}
	}
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json) {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Save;
    }

    public static string ToJson<T>(T[] array) {
		Wrapper<T> wrapper = new Wrapper<T> {
			Save = array
		};
		return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint) {
		Wrapper<T> wrapper = new Wrapper<T> {
			Save = array
		};
		return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Save;
    }
}
