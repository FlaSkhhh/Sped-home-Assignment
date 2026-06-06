using System;
using System.Collections;
using UnityEngine;

public class FallingItem : MonoBehaviour
{
    public GameSettingsSO settings;

    SpawnManager spawnManager;
    Collider col;
    bool caught;

    public static event Action<int> OnCaught;

    void Start()
    {
        spawnManager = FindFirstObjectByType<SpawnManager>();
        col = GetComponent<Collider>();
    }

    void OnEnable()
    {
        caught = false;
        if(col) col.enabled = true; //for startup
    }

    void Update()
    {
        //stay put where object us caught for shrinking visual feedback
        if (caught) return;
        //Move downwards based on the ScriptableObject speed and not gravity
        transform.Translate(Vector3.down * settings.fallSpeed * Time.deltaTime, Space.World);

        //reset if reaches the bottom of the screen
        if (Camera.main != null)
        {
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
            if (viewportPos.y < -0.1f)
            {
                spawnManager.ReturnToPool(this.gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //when hand catches the falling item reset it and add score
            caught = true;
            col.enabled = false;       //for not getting double points
            //OnCaught?.Invoke(settings.pointsPerCatch);    //trigger after mini animation is completed
            StartCoroutine(FlyToScoreUI());
        }
    }

    IEnumerator FlyToScoreUI()
    {
        float duration = 0.5f; //complete this in half sec 
        float elapsed = 0f;

        Vector3 startPos = transform.position;
        Vector3 startScale = transform.localScale;

        //score ui is top right so we use viewport (1,1) to get on screen pos
        Vector3 targetViewportPos = new Vector3(0.9f, 0.9f, 4f);
        Vector3 targetPos = Camera.main.ViewportToWorldPoint(targetViewportPos);

        //slight shrink before flying
        Vector3 targetScale = startScale * 0.5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / duration;
            float smoothStep = Mathf.SmoothStep(0f, 1f, t);

            //lerp position and scale at same time
            transform.position = Vector3.Lerp(startPos, targetPos, smoothStep);
            transform.localScale = Vector3.Lerp(startScale, targetScale, smoothStep);

            yield return null;
        }
        OnCaught?.Invoke(settings.pointsPerCatch);

        //reset item
        transform.localScale = startScale;
        spawnManager.ReturnToPool(this.gameObject);
    }
}