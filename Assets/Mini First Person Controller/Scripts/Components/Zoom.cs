using UnityEngine;

[ExecuteInEditMode]
public class Zoom : MonoBehaviour
{
    private Camera cam; // ✅ Renamed to 'cam' to avoid conflicts
    public float defaultFOV = 60;
    public float maxZoomFOV = 15;
    [Range(0, 1)] public float currentZoom;
    public float sensitivity = 1;

    void Awake()
    {
        cam = GetComponent<Camera>(); // ✅ Assign cam once here

        if (cam == null)
        {
            Debug.LogError("Zoom script is missing a Camera reference! Assign one in the Inspector.");
            return;
        }

        defaultFOV = cam.fieldOfView;
    }

    void Update()
    {
        if (cam == null) return; // ✅ Prevents NullReferenceException

        // Update zoom level
        currentZoom += Input.mouseScrollDelta.y * sensitivity * 0.05f;
        currentZoom = Mathf.Clamp01(currentZoom);
        cam.fieldOfView = Mathf.Lerp(defaultFOV, maxZoomFOV, currentZoom);
    }
}
