using UnityEngine;
using UnityEngine.UI;

public class WebcamManager : MonoBehaviour
{
    [SerializeField] RawImage displayImage;
    WebCamTexture webcamTexture;

    void Start()
    {
        //same size as canvas pixels
        Application.targetFrameRate = 60;
        webcamTexture = new WebCamTexture(1920, 1080, 30);
        displayImage.texture = webcamTexture;
        webcamTexture.Play();
    }

    void OnDestroy()
    {
        if (webcamTexture != null && webcamTexture.isPlaying) webcamTexture.Stop();
        if (webcamTexture != null) Destroy(webcamTexture);
    }

    public void DisableWebCam()
    {
        OnDestroy();
    }

    public WebCamTexture GetWebcamTexture()
    {
        return webcamTexture;
    }
}