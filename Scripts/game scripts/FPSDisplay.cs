using UnityEngine;
using System.Collections;
/// <summary>
/// Display FPS in a GUI label
/// Author: Dave Hampson, wiki.unity3d.com/index.php?title=FramesPerSecond
/// </summary>
public class FPSDisplay : MonoBehaviour
{
    float deltaTime = 0.0f;
    public static bool Showfps { get; set; }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(205, 44, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = 36;
        style.fontStyle = FontStyle.Bold;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        if (Showfps)
        {
            GUI.Label(rect, text, style);
        }
        
    }
}