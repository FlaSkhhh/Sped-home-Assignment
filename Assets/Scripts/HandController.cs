using UnityEngine;

public class HandController : MonoBehaviour
{
    [Header("Tracking References")]
    public PoseTracker poseTracker;

    [Header("Physics Colliders (Rigidbodies)")]
    public Rigidbody leftHandRb;
    public Rigidbody rightHandRb;

    int leftWristIndex = 15;
    int rightWristIndex = 16;
    float distanceFromCamera = 4f;

    Vector3 leftTargetPos;
    Vector3 rightTargetPos;
    bool leftIsTracked;
    bool rightIsTracked;

    void LateUpdate()
    {
        if (poseTracker.detecter == null || Camera.main == null) { Debug.Log("ERROR in cam or posetracker"); return; }

        CalculateTarget(leftWristIndex, out leftTargetPos, out leftIsTracked);
        CalculateTarget(rightWristIndex, out rightTargetPos, out rightIsTracked);
    }

    void FixedUpdate()
    {
        //Move the objeects on fixed update for physics accuracy
        ApplyPhysicsMove(leftHandRb, leftTargetPos, leftIsTracked);
        ApplyPhysicsMove(rightHandRb, rightTargetPos, rightIsTracked);
    }

    private void CalculateTarget(int index, out Vector3 targetPos, out bool isTracked)
    {
        Vector4 rawData = poseTracker.detecter.GetPoseLandmark(index);

        //if confidence is low/or if data is not done calculating then mark it as false for tracking
        if (float.IsNaN(rawData.x) || float.IsNaN(rawData.w) || rawData.w < 0.5f)//to stop getting error from NaN value before webcam is turne on
        {
            isTracked = false;
            targetPos = Vector3.zero;   //this doesnt matter as rb movement part handles with bool
            return;
        }

        isTracked = true;

        Vector3 pos = new Vector3(rawData.x, rawData.y, distanceFromCamera);
        targetPos = Camera.main.ViewportToWorldPoint(pos);
    }

    private void ApplyPhysicsMove(Rigidbody rb, Vector3 targetPos, bool isTracked)
    {
        if (rb == null) return;

        if (isTracked)
        {
            //this handles velocity and collision better than lateupdate
            rb.MovePosition(targetPos);
        }
        else
        {
            //move the hand away if tracking is bad
            rb.MovePosition(new Vector3(0, -100f, 0));
        }
    }
}