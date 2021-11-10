using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

namespace TodoWorks.Editor.Scripts
{
	// todo: add saving and loading of the note data to file
	public static class NoteManager
	{
		/*public static Dictionary<string, List<CheckListData>> noteData = new Dictionary<string, List<CheckListData>>();
		private static readonly string filePath = "NoteData.fun";

		public static void SaveToFile()
		{
			FileStream fs = new FileStream(filePath, FileMode.Create);
			BinaryFormatter formatter = new BinaryFormatter();
			
			try { formatter.Serialize(fs, noteData); }
			catch(SerializationException e)
			{
				Debug.LogException(new Exception("Failed To serialize. Reason: " + e.Message));
				throw;
			}
			finally { fs.Close(); }
		}
		
		
		public static void LoadFileToDictionary()
		{
			//noteData.Clear();
			FileStream fs = File.Open(filePath, FileMode.Open);
			try
			{
				BinaryFormatter formatter = new BinaryFormatter();
				noteData = (Dictionary<string, List<CheckListData>>)formatter.Deserialize(fs);
			}
			catch(SerializationException e)
			{
				Debug.LogException(new Exception("Failed to deserialize. Reason: " + e.Message));
				throw;
			}
			finally { fs.Close(); }
		}
		/// <summary> Stores the data too the
		/// dictionary and saves it </summary>
		/// <param name="_name">the name of the data to save</param>
		/// <param name="_data">the list of checklist info</param>
		public static void AddToDictionary(string _name, List<CheckListData> _data)
		{
			if(noteData.ContainsKey(_name))
			{
				Debug.LogWarning("Note Already Exists");
				return;
			}
			noteData.Add(_name, _data);
		}
		/// <summary> Overrides the dictionary
		/// with any changes made </summary>
		/// <param name="_name">name of the data being saved</param>
		/// <param name="_data">checklist data being overridden</param>
		public static void SaveToDictionary(string _name, List<CheckListData> _data) => noteData[_name] = _data;
		public static void ClearDictionary() => noteData.Clear();*/
	}
}