using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
public class _Test_EditorWindow : EditorWindow
{

    GUIStyle titleStyle = null;
    string strProjectName = "SlotGame001";
    string strProjectDescription = "";
    Vector2 scrollPosition = Vector2.zero;
    public int iNumberOfReels = 5;
    bool bShowDetailData = true;
    public GameObject manager = null;
    public GameObject assetmgr = null;
    public GameObject reelLevel = null;
    public GameObject animationLevel = null;
    public GameObject winingAnimation = null;
    private DefaultAsset targetFolder = null;
    // Start is called before the first frame update
    [MenuItem("Window/測試視窗功能")]
    //靜態函數, 為選項按下時要執行的功能, 會啟動EditorWindow 獨立的視窗
    static void startWork()
    {
        _Test_EditorWindow myWork = (_Test_EditorWindow)EditorWindow.GetWindow(typeof(_Test_EditorWindow));
        myWork.Show();
    }
    public void OnEnable()
    {

    }
    public void OnGUI()
    {
        Color originColor = GUI.contentColor;
        float width = EditorGUIUtility.labelWidth;
        if (titleStyle == null)
        {
            titleStyle = new GUIStyle(GUI.skin.label);
            titleStyle.fontSize = 18;
            titleStyle.fontStyle = FontStyle.Bold;
            titleStyle.alignment = TextAnchor.MiddleCenter;
        }

        //======================================================
        //顯示頁面標題
        showWindowTitle();
        GUILayout.Space(10);
        drawLine();
        GUILayout.Space(10);
        EditorGUIUtility.labelWidth = 60;
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(position.width), GUILayout.Height(position.height - 60));
        {
            //======================================================
            //專案基本資料
            setupProjectData();
            GUILayout.Space(10);
            drawLine();
            GUILayout.Space(10);

            //======================================================
            //圖庫設定
            setupIconAsset();
            GUILayout.Space(10);
            drawLine();
            GUILayout.Space(10);

            //======================================================
            //轉輪詳細資料設定
            setupAllReelsData();
            GUILayout.Space(10);
            drawLine();
            GUILayout.Space(10);

            //======================================================
            //產生資料及畫面
            generateAllData();
        }
        // Debug.Log("windows width: " + EditorGUIUtility.currentViewWidth + ", " + position.width + ",  " + position.height);
        GUILayout.EndScrollView();
        EditorGUIUtility.labelWidth = width;
    }
    //-----------------------------------------------------------------------------------------
    void showWindowTitle()
    {
        GUILayout.Space(15);
        GUILayout.BeginHorizontal();
        {
            GUILayout.FlexibleSpace();
            GUILayout.Label("專案基本資料設定", titleStyle);
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();

    }
    void setupAllReelsData()
    {
        //轉輪數量設定
        iNumberOfReels = EditorGUILayout.IntSlider("轉輪數量", iNumberOfReels, 3, 10, GUILayout.Width(250));
        // multiReelSetupEditor.checkNumberOfReels();
        GUILayout.Space(10);
        drawLine();
        GUILayout.Space(10);
        //轉輪詳細資料設定
        bShowDetailData = EditorGUILayout.BeginFoldoutHeaderGroup(bShowDetailData, "轉輪詳細資料");
        // bShowDetailData = EditorGUILayout.Foldout(bShowDetailData, "詳細資料");
        // if (bShowDetailData)
        // {
        //     multiReelSetupEditor.multiReelData();
        // }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }
    //-----------------------------------------------------------------------------------------
    void setupProjectData()
    {
        strProjectName = EditorGUILayout.TextField("專案名稱", strProjectName, GUILayout.Width(300));
        GUILayout.Space(10);
        EditorGUILayout.LabelField("專案概述");
        EditorGUI.indentLevel += 2;
        strProjectDescription = EditorGUILayout.TextArea(strProjectDescription, GUILayout.Width(400), GUILayout.Height(100));
        EditorGUI.indentLevel -= 2;
        GUILayout.Space(10);
    }
    //-----------------------------------------------------------------------------------------
    void generateAllData()
    {
        if (GUILayout.Button("建立結構", GUILayout.Width(200)))
        {
            createEmptyStructure();
        }
        if (GUILayout.Button("偵測", GUILayout.Width(200)))
        {
            detectGameStructureInScene();
        }
        if (GUILayout.Button("刪除", GUILayout.Width(200)))
        {
            deleteGameStructureInScene();
        }
    }
    void setupIconAsset()
    {
        float width = EditorGUIUtility.labelWidth;
        int len = 130;
        GUILayout.BeginHorizontal();
        {
            EditorGUIUtility.labelWidth = len;
            EditorGUI.indentLevel += 2;
            DefaultAsset origin = targetFolder;
            targetFolder = (DefaultAsset)EditorGUILayout.ObjectField(
                        "Icon目錄",
                        targetFolder,
                        typeof(DefaultAsset),
                        false, GUILayout.Width(len + 160));
            EditorGUI.indentLevel -= 2;

            if (GUILayout.Button("重新匯入", GUILayout.Width(80)))
            {
                // processSpriteAssest();
            }
        }
        GUILayout.EndHorizontal();
        EditorGUIUtility.labelWidth = width;
    }
    public void detectGameStructureInScene()
    {
        manager = GameObject.Find("SlotGameManager");
        assetmgr = GameObject.Find("IconAssetManager");
        animationLevel = GameObject.Find("AnimationLevel");
        winingAnimation = GameObject.Find("WiningAnimationLevel");
        reelLevel = GameObject.Find("ReelLevel");

        string str = "";

        if (manager == null)
        {
            str += "SlotGameManager 不存在, \\n";
        }
        if (assetmgr == null)
        {
            str += "IconAssetManager 不存在 \\n";
        }
        if (reelLevel == null)
        {
            str += "ReelLevel 不存在, \\n";
        }

        if (animationLevel == null)
        {
            str += "AnimationLevel 不存在 \\n";
        }
        if (winingAnimation == null)
        {
            str += "WiningAnimationLevel 不存在 \\n";
        }
        Debug.Log(str);
    }
    //-----------------------------------------------------------------------------------------
    public void ModifyGameStructureInScene()
    {
        if (manager == null)
        {
            createGameManager();
        }
        if (assetmgr == null)
        {
            createIconAssetManager();
        }
        if (reelLevel == null)
        {
            createReelManager();
        }
        if (animationLevel == null)
        {
            createAnimationLevel();
        }
        if (winingAnimation == null)
        {
            createWiningAnimationLevel();
        }
        orderGameobject();
    }
    //-----------------------------------------------------------------------------------------
    public void deleteGameStructureInScene()
    {
        manager = GameObject.Find("SlotGameManager");
        if (manager != null)
        {
            GameObject.DestroyImmediate(manager);
        }

    }
    //-----------------------------------------------------------------------------------------
    public void createEmptyStructure()
    {
        //=======================================
        //建立空物件SlotGameManager當作遊戲管理程式
        createGameManager();

        //=======================================
        //建立空物件IconAssetManager當作圖片管理程式
        createIconAssetManager();

        //=======================================
        //建立空物件ReelLevel當作轉輪管理程式
        createReelManager();

        //=======================================
        //建立空物件AnimationLevel當作動畫層管理程式
        createAnimationLevel();

        //=======================================
        //建立空物件WiningAnimationLevel當作中獎層管理程式
        createWiningAnimationLevel();
        orderGameobject();
    }
    void orderGameobject()
    {
        assetmgr.transform.SetSiblingIndex(0);
        reelLevel.transform.SetSiblingIndex(1);
        animationLevel.transform.SetSiblingIndex(2);
        winingAnimation.transform.SetSiblingIndex(3);
    }
    //-----------------------------------------------------------------------------------------
    //建立空物件SlotGameManager當作遊戲管理程式
    void createGameManager()
    {
        manager = new GameObject("SlotGameManager");
        manager.transform.localPosition = Vector3.zero;
        manager.transform.localScale = Vector3.one;
        manager.transform.eulerAngles = Vector3.zero;
    }
    //-----------------------------------------------------------------------------------------
    //建立空物件IconAssetManager 圖庫管理程式
    public void createIconAssetManager()
    {
        assetmgr = new GameObject("IconAssetManager");
        assetmgr.transform.localPosition = Vector3.zero;
        assetmgr.transform.localScale = Vector3.one;
        assetmgr.transform.eulerAngles = Vector3.zero;
        assetmgr.transform.parent = manager.transform;
        // assetManager = assetmgr.AddComponent<IconAssetManager>();
        // iconAssetSetup_Editor.processSpriteAssest();
    }
    //建立空物件ReelLevel當作轉輪管理程式
    //-----------------------------------------------------------------------------------------
    public void createReelManager()
    {
        reelLevel = new GameObject("ReelLevel");
        reelLevel.transform.localPosition = Vector3.zero;
        reelLevel.transform.localScale = Vector3.one;
        reelLevel.transform.eulerAngles = Vector3.zero;
        reelLevel.transform.parent = manager.transform;
    }
    //-----------------------------------------------------------------------------------------
    //建立空物件AnimationLevel當作動畫層管理程式
    void createAnimationLevel()
    {
        animationLevel = new GameObject("AnimationLevel");
        animationLevel.transform.localPosition = Vector3.zero;
        animationLevel.transform.localScale = Vector3.one;
        animationLevel.transform.eulerAngles = Vector3.zero;
        animationLevel.transform.parent = manager.transform;
    }
    //-----------------------------------------------------------------------------------------
    //建立空物件WiningAnimationLevel當作中獎層管理程式
    void createWiningAnimationLevel()
    {
        winingAnimation = new GameObject("WiningAnimationLevel");
        winingAnimation.transform.localPosition = Vector3.zero;
        winingAnimation.transform.localScale = Vector3.one;
        winingAnimation.transform.eulerAngles = Vector3.zero;
        winingAnimation.transform.parent = manager.transform;
    }
    //-----------------------------------------------------------------------------------------
    public void drawLine()
    {
        int i_height = 1;
        Rect rect = EditorGUILayout.GetControlRect(false, i_height);
        rect.height = i_height;
        EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
    }
}
