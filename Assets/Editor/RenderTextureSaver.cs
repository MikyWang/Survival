using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class RenderTextureSaver : EditorWindow
{
    public RenderTexture texture;
    static GenericMenu menu;
    public string fileName;
    [MenuItem("Tools/RenderTextureToPng")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        GetWindow(typeof(RenderTextureSaver));   //获取到窗口，并且显示
        //实例化并且设置menu
        menu = new GenericMenu();   //初始化menu，但是没有显示
    }

    void OnGUI()
    {
        //创建要显示在编辑窗口的内容
        texture = (RenderTexture)EditorGUILayout.ObjectField("选择Texture:", texture, typeof(RenderTexture), true);
        fileName = EditorGUILayout.TextField("保存文件名:", fileName);
        if (GUILayout.Button("保存"))
        {
            SaveRenderTexture(texture);
        }
    }

    private void SaveRenderTexture(RenderTexture rt)
    {
        RenderTexture active = RenderTexture.active;
        RenderTexture.active = rt;
        Texture2D png = new Texture2D(rt.width, rt.height, TextureFormat.ARGB32, false);
        png.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        png.Apply();
        RenderTexture.active = active;
        byte[] bytes = png.EncodeToPNG();
        string path = $"Assets/UI/{fileName}.png";
        FileStream fs = File.Open(path, FileMode.Create);
        BinaryWriter writer = new BinaryWriter(fs);
        writer.Write(bytes);
        writer.Flush();
        writer.Close();
        fs.Close();
        DestroyImmediate(png);
        png = null;
        Debug.Log("保存成功！" + path);
    }
}
