using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using UnityEditor;
using UnityEngine;

public class EditorWindow1103 : EditorWindow
{
    GUIStyle titleStyle = null;
    DefaultAsset targetFolder;
    string projectName;
    string projectDescription;
    int numberOfReels;
    bool showReelsData;

    [MenuItem("Window/My Window")]
    static void Init()
    {
        EditorWindow1103 window = (EditorWindow1103)GetWindow(typeof(EditorWindow1103),false,"My window");
        window.Show();
    }

    private void OnGUI()
    {
        // ���D�˦�
        if(titleStyle == null)
        {
            titleStyle = new GUIStyle(GUI.skin.label);
            titleStyle.fontSize = 18;
            titleStyle.fontStyle = FontStyle.Bold;
            titleStyle.alignment = TextAnchor.MiddleCenter;
        }

        EditorGUIUtility.labelWidth = 60;
        EditorGUILayout.BeginScrollView(Vector2.zero, GUILayout.Width(position.width), GUILayout.Height(position.height));
        showTitle();
        drawLine();
        setupProjectData();
        drawLine();
        setupIconAsset();
        drawLine();
        setupAllReelsData();
        drawLine();
        setupReelsDetailData();
        drawLine();
        createDataButton();
        EditorGUILayout.EndScrollView();
    }

    // ���D�϶�
    void showTitle()
    {
        GUILayout.Space(15);
        GUILayout.BeginHorizontal();
        {
            GUILayout.FlexibleSpace();
            GUILayout.Label("�M�װ򥻸�Ƴ]�w", titleStyle);
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(15);
    }

    // �]�w�M�צW�ٻP���z
    void setupProjectData()
    {
        GUILayout.Space(15);
        GUILayout.BeginHorizontal();
        {
            GUILayout.FlexibleSpace();
            EditorGUILayout.BeginVertical();
            {
                projectName = EditorGUILayout.TextField("�M�צW��:", projectName, GUILayout.Width(300));
                GUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("�M�׷��z:", GUILayout.Width(57));
                    EditorGUILayout.BeginVertical();
                    {
                        GUILayout.Space(10);
                    }
                    EditorGUILayout.EndVertical();
                    projectDescription = EditorGUILayout.TextArea(projectDescription, GUILayout.Width(237), GUILayout.Height(100));
                }
                GUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(15);
    }

    // �]�wIcon�ؿ�
    void setupIconAsset()
    {
        GUILayout.Space(15);
        GUILayout.BeginHorizontal();
        {
            GUILayout.FlexibleSpace();
            targetFolder = (DefaultAsset)EditorGUILayout.ObjectField("Icon�ؿ�:", targetFolder, typeof(DefaultAsset), false, GUILayout.Width(300));
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        {
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("�פJIcon", GUILayout.Width(150)))
            {

            }
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(15);
    }

    // �]�w������
    void setupAllReelsData()
    {
        GUILayout.Space(15);
        GUILayout.BeginHorizontal();
        {
            GUILayout.FlexibleSpace();
            numberOfReels = EditorGUILayout.IntSlider("����ƶq:", numberOfReels, 3, 10, GUILayout.Width(300));
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(15);
    }

    // �]�w����ԲӸ��
    void setupReelsDetailData()
    {
        GUILayout.Space(15);
        GUILayout.BeginHorizontal();
        {
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            GUIStyle style = EditorStyles.foldoutHeader;
            style.fixedWidth = 300;
            {
                showReelsData = EditorGUILayout.BeginFoldoutHeaderGroup(showReelsData, "����ԲӸ��", style);
                if (showReelsData)
                {
                    reelsData();
                }
                EditorGUILayout.EndFoldoutHeaderGroup();
            }
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(15);
    }

    // ����ԲӸ��
    void reelsData()
    {
        GUILayout.Label("123456123456123456");

    }

    // ���Assets
    void createDataButton()
    {
        GUILayout.Space(15);
        GUILayout.BeginHorizontal();
        {
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("�إ�GameZoom", GUILayout.Width(300)))
            {
                createData();
            }
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(15);
    }



    void createData()
    {
        //assetsData();
        GameObject gameObj = GameObject.Find("GameZoom");

        if (gameObj)
        {
            DataTest data = gameObj.GetComponent<GameZoom>().data;
            Data objData = data.data;
            EditorUtility.DisplayDialog("Alert", "GameZoom�w�s�b ", "�T�w");
            if (objData == null)
            {
                Debug.Log(null);
            }
            else
            {
                Debug.Log(objData.id);
                Debug.Log(objData.name);
                Debug.Log(objData.description);
                Debug.Log(objData.creation);
                Debug.Log(objData.modification);
            }
        }
        else
        {
            GameObject gameZoom = new GameObject("GameZoom");
            gameZoom.gameObject.AddComponent<GameZoom>();
            gameZoom.GetComponent<GameZoom>().data = (DataTest)Resources.Load("New Data", typeof(DataTest));
            gameZoom.GetComponent<GameZoom>().data.data.creation = File.GetCreationTime("Assets/Resources/New Data.asset").ToString();
            gameZoom.GetComponent<GameZoom>().data.data.modification = File.GetLastWriteTime("Assets/Resources/New Data.asset").ToString();
        }
    }

    /*
    
    // Assets���
    void assetsData()
    {
        string path = Application.dataPath;
        Dir parentDir = new Dir();
        Dir dir = parentDir;
        Dir temp = dir;
        getName(path, dir);

        //parentDir.print(parentDir);
    }
    
    // ���o�Ҧ��ؿ��M�ɮ�
    void getName(string path, Dir parentDir)
    {
        Dir dir = parentDir;
        Dir temp = dir;

        foreach (string item in Directory.GetDirectories(path))
        {
            dir = new Dir
            {
                dirName = item
            };
            parentDir.subDir.Add(dir);
            temp = parentDir;
            getName(item, dir);
            parentDir = temp;
        }

        foreach (string item in Directory.GetFiles(path))
        {
            parentDir.file.Add(item);
        }
    }
    */

    // �e�u
    void drawLine()
    {
        int lineHeight = 1;
        Rect rect = EditorGUILayout.GetControlRect(false, lineHeight);
        EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
    }
}

public class Dir
{
    public List<Dir> subDir = new List<Dir>();
    public List<string> file = new List<string>();
    public string dirName;
    public void print(Dir dir)
    {
        getName(dir);

        foreach(string f in file)
        {
            Debug.Log(f);
        }
    }
    void getName(Dir dir)
    {
        foreach (Dir item in dir.subDir)
        {
            Debug.Log(item.dirName);

            getName(item);

            foreach (string f in item.file)
            {
                Debug.Log(f);
            }
        }


    }

}