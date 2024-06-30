using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ResultUi : MonoBehaviour
{
    static public ResultUi Instance;
    [SerializeField] TMP_Text winNameText;
    [SerializeField] GameObject resultUI;

    private void Awake()
    {
        Instance = this;
    }

    private IEnumerator TakeScreenshotAndShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // To avoid memory leaks
        Destroy(ss);

        new NativeShare().AddFile(filePath)
            .SetSubject("Zai Test").SetText("Hello world!")
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();

        // File.Delete(filePath);
    }

    public void ShareBTN()
    {
        StartCoroutine(TakeScreenshotAndShare());
    }

    public void ShowWinUI(string winPlayer)
    {
        winNameText.text = winPlayer;
        resultUI.SetActive(true);
    }
}
