using UnityEngine;

public class JointVisualizer : MonoBehaviour
{
    public PoseTracker poseTracker;
    public GameObject markerPrefab; //emission sphere

    private GameObject[] markers;

    //Head(0), L_Shoulder(11), R_Shoulder(12), L_Elbow(13), R_Elbow(14), L_Wrist(15), R_Wrist(16)
    private readonly int[] targetJoints = { 0, 11, 12, 13, 14, 15, 16 };

    void Start()
    {
        //Spawn the spheres
        markers = new GameObject[targetJoints.Length];
        for (int i = 0; i < markers.Length; i++)
        {
            markers[i] = Instantiate(markerPrefab, transform);
            markers[i].transform.localScale = Vector3.one * 0.25f;  //reduce size a bit
        }
    }

    //using lateUpdate to change pos after screen has rendered
    void LateUpdate()
    {
        if (poseTracker.detecter == null) return;

        for (int i = 0; i < targetJoints.Length; i++)
        {
            int jointIndex = targetJoints[i];

            //4th metric is confidence score
            Vector4 rawLandmark = poseTracker.detecter.GetPoseLandmark(jointIndex);

            //disable for low confidence than 0.5
            if (rawLandmark.w < 0.5f)
            {
                markers[i].SetActive(false);
                continue;
            }

            markers[i].SetActive(true);

            float mappedX = rawLandmark.x;
            float mappedY = rawLandmark.y;

            float distanceFromCamera = 4f;

            //converting to world pos
            Vector3 viewportPosition = new Vector3(mappedX, mappedY, distanceFromCamera);
            markers[i].transform.position = Camera.main.ViewportToWorldPoint(viewportPosition);
        }
    }
}