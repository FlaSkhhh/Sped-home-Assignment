using Mediapipe.BlazePose;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PoseTracker : MonoBehaviour
{
    public WebcamManager webcamManager;

    public BlazePoseDetecter detecter;

    void Start()
    {
        detecter = new BlazePoseDetecter();
    }

    void Update()
    {
        //escape to main menu code here as this is in all task scenes and most important part
        if (Keyboard.current.escapeKey.wasPressedThisFrame) { webcamManager.DisableWebCam(); SceneManager.LoadScene(0); return; }
        if (webcamManager == null || detecter == null) return;

        var texture = webcamManager.GetWebcamTexture();
        //the width part is if webcam starts late and unity sets default tex of 16x16 instead of 1080p we want
        if (texture == null || texture.width < 100 || !texture.didUpdateThisFrame) return;  

        //process the image for joint data
        detecter.ProcessImage(texture);
    }

    void OnDestroy()
    {
        detecter?.Dispose();
    }
}