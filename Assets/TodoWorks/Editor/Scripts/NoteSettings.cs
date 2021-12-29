using UnityEditor.AnimatedValues;
using System.Collections.Generic;
using TodoWorks.Editor.Scripts;
using UnityEngine;
using UnityEditor;
using System;

using TodoWorks.Editor.Utils;



namespace TodoWorks.Editor.Tests
{
	/// <summary> Handles setting up the
	/// note as well as searching for a notes </summary>
	public class NoteSettings : EditorWindow
	{
		/// <summary> Stores text for
		/// the notes title </summary>
		private string titleText;
		/// <summary> Stores text for
		/// the notes description </summary>
		private string descriptionText;
		
		private Color backgroundColor = Color.yellow;
		private Color titleTextColor = Color.black;
		
		private Vector2 mainScrollBar;
		private Vector2 checklistBar;
		
		private int checkListCount = 0;
		private AnimBool loadCards;
		private AnimBool newCard;
		
		//private List<string> noteNames = new List<string>();
		private string[] checkListText;
		private string[] loadedFileNames;

		[MenuItem("NoteWork/NoteSettings")]
		public static void ShowWindow()
		{
			// used to make sure that the settings are reset to their defaults
			var setupSettings = GetWindow<NoteSettings>("Note Settings");
			setupSettings.titleText = "";

			setupSettings.descriptionText = "";
			setupSettings.checkListCount = 0;

			setupSettings.checkListText = Array.Empty<string>();
			setupSettings.loadCards.target = false;
		}
		
		private void OnEnable()
		{
			// sets up the animations for the editor
			newCard = new AnimBool(false);
			loadCards = new AnimBool(false);
			//
			loadCards.valueChanged.AddListener(Repaint);
			newCard.valueChanged.AddListener(Repaint);
		}

		/// <summary> Generates a new note and sets all the values for that note </summary>
		/// <param name="_desText">sets the text for the description</param>
		/// <param name="_titleText">sets the text for the title</param>
		/// <param name="_bCol">sets the color of the title background</param>
		/// <param name="_tCol">sets the color of the title text</param>
		private void CreateNewNote(string _desText, string _titleText, Color _bCol, Color _tCol)
		{
			NoteSaveFile saveFile = new NoteSaveFile();
			saveFile.StoreData(titleText, descriptionText, checkListText, _bCol, _tCol);
			var fileCheck = XMLSaveSystem.SaveFile(titleText, saveFile);

			var newNote = CreateWindow<NoteWindow>(_titleText);
			if(!fileCheck)
			{
				newNote.titleText = _titleText;
				newNote.descriptionText = _desText;
				
				newNote.backgroundCol = _bCol;
				newNote.titleCol = _tCol;
				
				newNote.SetChecklistUp(checkListText);
				newNote.closed = false;
			}
			else LoadNote(XMLSaveSystem.LoadFile(titleText));
			//var newNote = CreateWindow<NoteWindow>(_titleText);
			//newNote.descriptionText = _desText;
			//newNote.titleText = _titleText;
			//
			//newNote.backgroundCol = _bCol;
			//newNote.titleCol = _tCol;
		}

		/// <summary> loads the specified note and displays the
		/// title description and its checklist information </summary>
		/// <param name="_data">the specified note to load</param>
		private void LoadNote(NoteSaveFile _data)
		{
			NoteWindow loadedNote = GetWindow<NoteWindow>("Loaded Note: " + _data.noteName);
			loadedNote.titleText = _data.noteName;
			
			loadedNote.descriptionText = _data.noteDescription;
			loadedNote.checkListData = _data.checkList;

			loadedNote.backgroundCol = _data.backgroundColor;
			loadedNote.titleCol = _data.titleTextColor;
			loadedNote.closed = false;
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

		/// <summary> Allows customisation
		/// of the text box </summary>
		private GUIStyle TextStyle()
		{
			GUIStyle style = new GUIStyle
			{
				wordWrap = true,
				normal =
				{
					textColor = Color.white,
				},
			};
			return style;
		}

		private void OnGUI()
		{
			mainScrollBar = EditorGUILayout.BeginScrollView(mainScrollBar, GUILayout.Width(position.width), GUILayout.Height(position.height));
			GUILayout.Label("NoteWork Menu", TitleStyle(titleTextColor, backgroundColor, 25));
			
			if(GUILayout.Button("LoadCards"))
			{
				var names = XMLSaveSystem.SearchForFiles();
				loadedFileNames = names;

				string debugText = "namesArray: ";
				foreach(var fname in names)
					debugText += fname + "\n";

				loadCards.target = !loadCards.target;
				Debug.Log(debugText);
			}
			
			if(EditorGUILayout.BeginFadeGroup(loadCards.faded)) 
				foreach(var fName in loadedFileNames)
					if(GUILayout.Button(fName))
						LoadNote(XMLSaveSystem.LoadFile(fName));
			
			EditorGUILayout.EndFadeGroup();
			
			// ReSharper disable once BadControlBracesIndent
			#region New note and settings
			// new note section
			string buttonText = newCard.target ? "CloseMenu" : "NewCard";
			if(EditorGUILayout.BeginFadeGroup(newCard.faded))
			{

				#region note creation

				// todo: change this to be a placeholder that displays over the top
				if(titleText == "") titleText = "Title here....";
				if(descriptionText == "") descriptionText = "description here....";

				titleText = GUILayout.TextField(titleText, TextStyle());
				descriptionText = GUILayout.TextArea(descriptionText, TextStyle());
				if(GUILayout.Button("Save")) CreateNewNote(descriptionText, titleText, backgroundColor, titleTextColor);

				#endregion

			#region Text and Color options

				// handles text and color options
				EditorGUILayout.BeginVertical();
				EditorGUILayout.BeginHorizontal();
				// positions the color options for the background
				GUILayout.Label("Background Color");
				GUILayout.Space(35);
				backgroundColor = EditorGUILayout.ColorField(backgroundColor);

				EditorGUILayout.EndHorizontal();
				EditorGUILayout.BeginHorizontal();
				// positions the color options for the text
				GUILayout.Label("Title Color");
				GUILayout.Space(78);
				titleTextColor = EditorGUILayout.ColorField(titleTextColor);

				EditorGUILayout.EndHorizontal();
				EditorGUILayout.EndVertical();

			#endregion
				
				//todo: setup checklist, need a place to store the data of the checklist as well as keep track of how many tasks are completed
				if(GUILayout.Button("New Checklist", GUILayout.Width(100)))
				{
					//todo: improve this feature so it doesnt wipe the list
					checkListCount++;
					checkListText = new string[checkListCount];
					for(int i = 0; i < checkListCount; i++) checkListText[i] = "Text here...";
				}
				
				// handles displaying a preview of the checklist
				// as well as lets the user name the checklist data
				if(checkListCount >= 1)
				{
					// makes things smoother
					var y = checkListCount * 20;
					var x = Mathf.Clamp(y, 0, 100);
					
					// checklist scroll view so that it takes up less space
					checklistBar = EditorGUILayout.BeginScrollView(checklistBar, GUILayout.Width(200), GUILayout.Height(x));
					for(int i = 0; i < checkListText.Length; i++)
					{
						// generates the checklist
						if(checkListText[i] == "") checkListText[i] = "Text here...";
						checkListText[i] = GUILayout.TextField(checkListText[i]);
					} 
					EditorGUILayout.EndScrollView();
					if(GUILayout.Button("Remove Checklist", GUILayout.Width(120)))
					{
						checkListCount--;
						checkListText = new string[checkListCount];
					}
				}
			}
			EditorGUILayout.EndFadeGroup();
			#endregion

			if(GUILayout.Button(buttonText, GUILayout.Width(100)))
				newCard.target = !newCard.target;
			EditorGUILayout.EndScrollView();
		}
	}
}
