using UnityEngine;
using UnityEngine.UI;

public class ImageZoomer : MonoBehaviour
{
    public float zoomSpeed = 10f; // adjust the zoom speed to your liking
    public float minZoom = 0.5f; // minimum zoom level
    public float maxZoom = 2f; // maximum zoom level
    public float smoothTime = 0.3f; // adjust the smoothness of the zooming effect

    private Image image; // reference to the UI Image
    private float targetScale; // target scale for the interpolation
    private float currentScale; // current scale of the UI Image

    void Start()
    {
        image = GetComponent<Image>();
        currentScale = image.rectTransform.localScale.x;
    }

    void Update()
    {
        // check for mouse scroll input
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // zoom in or out based on scroll input
        if (scrollInput != 0f)
        {
            float currentScale = image.rectTransform.localScale.x;
            float newScale = currentScale + (scrollInput * zoomSpeed);

            // clamp the scale to the minimum and maximum zoom levels
            newScale = Mathf.Clamp(newScale, minZoom, maxZoom);

            image.rectTransform.localScale = new Vector3(newScale, newScale, 1f);
        }
    }
}