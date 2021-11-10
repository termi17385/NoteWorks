using UnityEditor;
using UnityEngine;
public class ExampleWindow : EditorWindow
{
    private Vector2 scrollPos;
    private static Texture2D tex;
    private Vector2 mainWindowScrollPos;
    
    [MenuItem("Window/Example")]
    public static void ShowWindow()
    {
        GetWindow<ExampleWindow>("Example");
    }

    private int increment = 0;
    private void OnGUI()
    {
        if(Input.GetKeyDown(KeyCode.A)) Debug.Log("A");
        
        mainWindowScrollPos = EditorGUILayout.BeginScrollView(mainWindowScrollPos, GUILayout.Width(position.width), GUILayout.Height(position.height));
        GUILayout.Label("Testing", EditorStyles.boldLabel);
        if(GUILayout.Button("+")) increment++;
        if(GUILayout.Button("-")) increment--;

        if(increment > 0)
        {
            float windWidith = position.width;
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(windWidith), GUILayout.Height(100));
            for(int i = 0; i < increment; i++)
            {
                if(GUILayout.Button("button: " + i))
                {
                    InputTesting.SpawnTest(i * 2);
                    Debug.Log("Button: " + i);
                }
            }
            EditorGUILayout.EndScrollView();
        }
        if(GUILayout.Button("Reset")) increment = 0;

        // handles creation of a new texture and changing its color
        tex = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        tex.SetPixel(0, 0, new Color(.7f, .7f, 0));
        tex.Apply();
        
        GUIStyle newStyle = new GUIStyle
        {
            wordWrap = focusedWindow,
            normal =
            {
                textColor = Color.black,
                background = tex
            },
            alignment = TextAnchor.MiddleCenter,
            
            fontSize = 25,
            fontStyle = FontStyle.Bold
        };

        GUILayout.Label("Title", newStyle);
        GUILayout.Box("blah blah blh ahjiwad awawk ja ak kf k=kw=k jak kwakd ik jap faj awjf a kapfjapj fa f;kaj fkaw faw fipaf [oa fpia fo[a fpjaw f[kawo[ fjaop fjoa[ fjpoa fkp[a fop[awfj[oa k[oaj f ");
        EditorGUILayout.EndScrollView();
        
        /*if(GUILayout.Button("Text"))
        {
            string text = InputTesting.ReturnTest(ReturnType.Text).ToString();
            Debug.Log(text);
        }
        
        if(GUILayout.Button("Number"))
        {
            string text = InputTesting.ReturnTest(ReturnType.Number).ToString();
            Debug.Log(text);
        }
       
        if(GUILayout.Button("Mix"))
        {
            string text = InputTesting.ReturnTest(ReturnType.RandomMix).ToString();
            Debug.Log(text);
        }
        
        EditorGUILayout.BeginVertical();
        var windWidith = position.width;
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(windWidith - 50), GUILayout.Height(100));
        for(int i = 0; i < 100; i++)
        {
            string text = "Button:" + i;
            if(GUILayout.Button(text))
            {
                Debug.Log(i);
            }
        }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();*/
    }
}
