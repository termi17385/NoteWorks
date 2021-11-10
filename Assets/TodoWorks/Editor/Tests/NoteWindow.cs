using System.Collections.Generic;
using TodoWorks.Editor.Scripts;
using UnityEditor;
using UnityEngine;
using System;
using System.Linq;

using TodoWorks.Editor.Utils;

using UnityEditor.PackageManager.UI;

namespace TodoWorks.Editor.Tests
{
	/// <summary> This is the note that is
	/// displayed when created / loaded in </summary>
	public class NoteWindow : EditorWindow
	{
		/// <summary> Text that is displayed at the
		/// top of the note to help find it </summary>
		public string titleText;
		/// <summary> the description for
		/// the note </summary>
		public string descriptionText;

		/// <summary> The notes title
		/// background color </summary>
		public Color backgroundCol;
		/// <summary> The color of
		/// the title text </summary>
		public Color titleCol;

		/// <summary> A list of the check list data struct
		/// for storing the data of the checklist </summary>
		public List<CheckList> checkListData = new List<CheckList>();

		public bool closed = false;

		// public void SendDataToManager()
		// {
		// 	var thisWindow = GetWindow<NoteWindow>();
		// 	NoteManager.AddToDictionary(thisWindow.titleText, thisWindow.checkListData);
		// }

		
		public void SetChecklistUp(string[] _data)
		{
			for(int i = 0; i < _data.Length; i++)
			{
				checkListData.Add(new CheckList
				{
					text = _data[i]
				});
			}
		}

		/// <summary> Handles customising the
		/// title text and background </summary>
		/// <param name="_textColor">changes the color of the text</param>
		/// <param name="_backGroundColor">changes the color of the background</param>
		/// <param name="_fontSize">changes the size of the font</param>
		private GUIStyle TitleStyle(Color _textColor, Color _backGroundColor, int _fontSize)
		{
			GUIStyle style = new GUIStyle
			{
				wordWrap = true,
				normal =
				{
					textColor = _textColor,
					background = EditorUtils.Tex(new Vector2Int(1, 1), _backGroundColor)
				},
				alignment = TextAnchor.MiddleCenter,

				fontSize = _fontSize,
				fontStyle = FontStyle.Bold
			};

			return style;
		}

		private void Update()
		{
			if(closed) this.Close();
		}

		private void OnGUI()
		{
			GUILayout.Label(titleText, TitleStyle(titleCol, backgroundCol, 25));
			GUILayout.Box(descriptionText);
			
			GUILayout.Label("Checklist");
			// loops through the checklist data and generates the checklist
			for(int index = 0; index < checkListData.Count; index++)
			{
				// hides data once it is completed
				CheckList data = checkListData[index];
				//if(!data.completed)
				{
					// toggles completed
					data.completed = GUILayout.Toggle(data.completed, data.text);
					checkListData[index] = data;
					Debug.Log(data.completed);
				}
			}

			GUILayout.FlexibleSpace();
			if(GUILayout.Button("Delete", GUILayout.Width(48)))
			{
				XMLSaveSystem.DeleteFile(titleText);
				NoteSettings.ShowWindow();
				closed = true;
			}
			
			// todo: improve temporary fix maybe
			if(GUILayout.Button("Save", GUILayout.Width(48)))
			{
				NoteSaveFile saveFile = new NoteSaveFile();
				
				List<string> names = new List<string>();
				List<bool> completed = new List<bool>();

				foreach(CheckList data in checkListData)
				{
					names.Add(data.text);
					completed.Add(data.completed);
				}

				saveFile.UpdateData(titleText, descriptionText, names.ToArray(), completed.ToArray(), backgroundCol, titleCol);
				XMLSaveSystem.SaveFile(titleText, saveFile);
				NoteSettings.ShowWindow();
				closed = true;
			}
		}
	}
}