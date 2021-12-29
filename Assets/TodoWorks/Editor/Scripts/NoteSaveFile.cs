// Creator: Josh Jones
// Creation Time: 2021/10/14 3:51 PM
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using System.IO;
using System;
using System.Text.RegularExpressions;

using UnityEditor;

namespace TodoWorks.Editor.Tests
{
	[Serializable]
	public struct CheckList
	{
		public string text;
		public bool completed;
	}
	[Serializable]
	public class NoteSaveFile
	{
		public string noteName;
		public string noteDescription;

		public Color backgroundColor;
		public Color titleTextColor;
		
		public List<CheckList> checkList = new List<CheckList>();

		/// <summary> Handles the storing and setting of the notes data
		/// ie text, and the checklist </summary>
		/// <param name="_title">the title / filename of the note</param>
		/// <param name="_description">the notes description</param>
		/// <param name="_names">the checklist data being saved to the note</param>
		/// <param name="_bgColor">the color for the background of the title</param>
		/// <param name="_ttColor">the color for the text of the title</param>
		public void StoreData(string _title, string _description, string[] _names, Color _bgColor, Color _ttColor)
		{
			noteName = _title;
			noteDescription = _description;

			backgroundColor = _bgColor;
			titleTextColor = _ttColor;

			checkList.Clear();
			for(int i = 0; i < _names.Length; i++)
			{
				checkList.Add(new CheckList
				{
					text = _names[i]
				});
			}
		}
		
		/// <summary> Used to update the data in the
		/// saved file overriding its values </summary>
		/// <param name="_names">checklist descriptions</param>
		/// <param name="_completed">checklist bools</param>
		public void UpdateData(string _title, string _description, string[] _names, bool[] _completed, Color _bgColor, Color _ttColor)
		{
			noteName = _title;
			noteDescription = _description;

			backgroundColor = _bgColor;
			titleTextColor = _ttColor;

			checkList.Clear();
			for(int i = 0; i < _names.Length; i++)
			{
				checkList.Add(new CheckList
				{
					text = _names[i],
					completed = _completed[i]
				});
			}
		}
	}
	
	public static class XMLSaveSystem
	{
		private static readonly string filePath = "NoteFiles";
		
		/// <summary> Saves a note to xml and stores it in the unity
		/// project folder while also assigning it a name </summary>
		/// <param name="_fileName">The name assigned to the saved note</param>
		/// <param name="_data">the note data being saved</param>
		public static bool SaveFile(string _fileName, NoteSaveFile _data)
		{
			CheckForDirectory();
			XmlSerializer serializer = new XmlSerializer(typeof(NoteSaveFile));
			var filename = RemoveSpecialChar(_fileName);
			
			TextWriter writer = new StreamWriter(filePath + "/" + filename, false);
			NoteSaveFile noteSaveFile = _data;

			serializer.Serialize(writer, noteSaveFile);
			writer.Close();

			return new DirectoryInfo(filePath + "/" + filename).Exists;
		}
		
		/// <summary> Searches for files in the folder path and returns
		/// an array of the file names for loading later </summary>
		public static string[] SearchForFiles()
		{
			CheckForDirectory();
			
			var saveFiles = new DirectoryInfo(filePath).GetFiles();
			string[] fileNames = new string[saveFiles.Length];
			
			for(int i = 0; i < saveFiles.Length; i++) fileNames[i] = saveFiles[i].Name;
			return fileNames;
		}

		/// <summary> Loads any files
		/// matching the name </summary>
		/// <param name="_fileName">the file to load</param>
		/// <returns>returns a converted version of the xml
		/// file to be loaded back into the script</returns>
		public static NoteSaveFile LoadFile(string _fileName)
		{
			CheckForDirectory();
			var filename = RemoveSpecialChar(_fileName);
			
			XmlSerializer serializer = new XmlSerializer(typeof(NoteSaveFile));
			FileStream fs = new FileStream(filePath + "/" + filename, FileMode.Open);
			NoteSaveFile noteSaveFile = (NoteSaveFile)serializer.Deserialize(fs);

			fs.Close();
			return noteSaveFile;
		}

		/// <summary> Handles the deletion of any
		/// files in the directory </summary>
		/// <param name="_fileName">the file to delete</param>
		/// <param name="_allFiles">if we want to delete everything or not</param>
		public static void DeleteFile(string _fileName, bool _allFiles = false)
		{
			var fileName = RemoveSpecialChar(_fileName);
			var file = new DirectoryInfo(filePath).GetFiles(); //.Exists;
			if(!_allFiles)
			{
				foreach(var f in file)
				{
					if(f.Name == fileName)
					{
						f.Delete();
						AssetDatabase.Refresh();

						break;
					}			
				}
			}
			else
			{
				foreach(var f in file)
				{
					f.Delete();
					AssetDatabase.Refresh();
				}
			}
		}

		/// <summary> Handles making sure that files arent stored with special characters
		/// to solve issues with finding the files later </summary>
		/// <param name="_strIn">takes in a string to convert into standard characters</param>
		private static string RemoveSpecialChar(string _strIn)
		{
			// Replace invalid characters with empty strings.
			try {
				return Regex.Replace(_strIn, @"[^\w\.@-]", "",
					RegexOptions.None, TimeSpan.FromSeconds(1.5f));
			}
			// If we timeout when replacing invalid characters,
			// we should return Empty.
			catch (RegexMatchTimeoutException) {
				return String.Empty;
			}
		}
	
		/// <summary> Scans for a Folder
		/// in the project files </summary>
		private static void CheckForDirectory()
		{
			// creates a new folder to save files to if one doesn't exist
			var folderExists = Directory.Exists(filePath);
			if(!folderExists) Directory.CreateDirectory(filePath);
		}
	}
}