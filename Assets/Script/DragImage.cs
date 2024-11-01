using UnityEngine;
using UnityEngine.UI;

public class ImageDragger : MonoBehaviour
{
    public float dragSpeed = 10f; // adjust the drag speed to your liking

    private Image image; // reference to the UI Image
    private Vector3 startPosition; // initial position of the image
    private Vector3 currentPosition; // current position of the image

    [SerializeField] float limitX = 0;
    [SerializeField] float limitY = 0;

    void Start()
    {
        image = GetComponent<Image>();
        startPosition = image.rectTransform.localPosition;
    }

    void Update()
    {
        // check for mouse drag input
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            // calculate the drag delta
            Vector2 dragDelta = Input.mousePosition - startPosition;

            // update the image position
            currentPosition = image.rectTransform.anchoredPosition + (dragDelta * dragSpeed);
            image.rectTransform.localPosition = currentPosition;
        }

        if (image.rectTransform.localPosition.x < -limitX || image.rectTransform.localPosition.x > limitX)
        {
            image.rectTransform.localPosition = new Vector3(0, 0, 0);
        }
        else if (image.rectTransform.localPosition.y < -limitY || image.rectTransform.localPosition.y > limitY)
        {
            image.rectTransform.localPosition = new Vector3(0, 0, 0);
        }
    }
}