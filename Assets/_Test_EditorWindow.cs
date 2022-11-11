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
    [MenuItem("Window/���յ����\��")]
    //�R�A���, ���ﶵ���U�ɭn���檺�\��, �|�Ұ�EditorWindow �W�ߪ�����
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
        //��ܭ������D
        showWindowTitle();
        GUILayout.Space(10);
        drawLine();
        GUILayout.Space(10);
        EditorGUIUtility.labelWidth = 60;
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(position.width), GUILayout.Height(position.height - 60));
        {
            //======================================================
            //�M�װ򥻸��
            setupProjectData();
            GUILayout.Space(10);
            drawLine();
            GUILayout.Space(10);

            //======================================================
            //�Ϯw�]�w
            setupIconAsset();
            GUILayout.Space(10);
            drawLine();
            GUILayout.Space(10);

            //======================================================
            //����ԲӸ�Ƴ]�w
            setupAllReelsData();
            GUILayout.Space(10);
            drawLine();
            GUILayout.Space(10);

            //======================================================
            //���͸�Ƥεe��
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
            GUILayout.Label("�M�װ򥻸�Ƴ]�w", titleStyle);
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();

    }
    void setupAllReelsData()
    {
        //����ƶq�]�w
        iNumberOfReels = EditorGUILayout.IntSlider("����ƶq", iNumberOfReels, 3, 10, GUILayout.Width(250));
        // multiReelSetupEditor.checkNumberOfReels();
        GUILayout.Space(10);
        drawLine();
        GUILayout.Space(10);
        //����ԲӸ�Ƴ]�w
        bShowDetailData = EditorGUILayout.BeginFoldoutHeaderGroup(bShowDetailData, "����ԲӸ��");
        // bShowDetailData = EditorGUILayout.Foldout(bShowDetailData, "�ԲӸ��");
        // if (bShowDetailData)
        // {
        //     multiReelSetupEditor.multiReelData();
        // }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }
    //-----------------------------------------------------------------------------------------
    void setupProjectData()
    {
        strProjectName = EditorGUILayout.TextField("�M�צW��", strProjectName, GUILayout.Width(300));
        GUILayout.Space(10);
        EditorGUILayout.LabelField("�M�׷��z");
        EditorGUI.indentLevel += 2;
        strProjectDescription = EditorGUILayout.TextArea(strProjectDescription, GUILayout.Width(400), GUILayout.Height(100));
        EditorGUI.indentLevel -= 2;
        GUILayout.Space(10);
    }
    //-----------------------------------------------------------------------------------------
    void generateAllData()
    {
        if (GUILayout.Button("�إߵ��c", GUILayout.Width(200)))
        {
            createEmptyStructure();
        }
        if (GUILayout.Button("����", GUILayout.Width(200)))
        {
            detectGameStructureInScene();
        }
        if (GUILayout.Button("�R��", GUILayout.Width(200)))
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
                        "Icon�ؿ�",
                        targetFolder,
                        typeof(DefaultAsset),
                        false, GUILayout.Width(len + 160));
            EditorGUI.indentLevel -= 2;

            if (GUILayout.Button("���s�פJ", GUILayout.Width(80)))
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
            str += "SlotGameManager ���s�b, \\n";
        }
        if (assetmgr == null)
        {
            str += "IconAssetManager ���s�b \\n";
        }
        if (reelLevel == null)
        {
            str += "ReelLevel ���s�b, \\n";
        }

        if (animationLevel == null)
        {
            str += "AnimationLevel ���s�b \\n";
        }
        if (winingAnimation == null)
        {
            str += "WiningAnimationLevel ���s�b \\n";
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
        //�إߪŪ���SlotGameManager��@�C���޲z�{��
        createGameManager();

        //=======================================
        //�إߪŪ���IconAssetManager��@�Ϥ��޲z�{��
        createIconAssetManager();

        //=======================================
        //�إߪŪ���ReelLevel��@����޲z�{��
        createReelManager();

        //=======================================
        //�إߪŪ���AnimationLevel��@�ʵe�h�޲z�{��
        createAnimationLevel();

        //=======================================
        //�إߪŪ���WiningAnimationLevel��@�����h�޲z�{��
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
    //�إߪŪ���SlotGameManager��@�C���޲z�{��
    void createGameManager()
    {
        manager = new GameObject("SlotGameManager");
        manager.transform.localPosition = Vector3.zero;
        manager.transform.localScale = Vector3.one;
        manager.transform.eulerAngles = Vector3.zero;
    }
    //-----------------------------------------------------------------------------------------
    //�إߪŪ���IconAssetManager �Ϯw�޲z�{��
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
    //�إߪŪ���ReelLevel��@����޲z�{��
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
    //�إߪŪ���AnimationLevel��@�ʵe�h�޲z�{��
    void createAnimationLevel()
    {
        animationLevel = new GameObject("AnimationLevel");
        animationLevel.transform.localPosition = Vector3.zero;
        animationLevel.transform.localScale = Vector3.one;
        animationLevel.transform.eulerAngles = Vector3.zero;
        animationLevel.transform.parent = manager.transform;
    }
    //-----------------------------------------------------------------------------------------
    //�إߪŪ���WiningAnimationLevel��@�����h�޲z�{��
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
