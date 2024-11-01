using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI header;

    public TextMeshProUGUI content;

    public LayoutElement layoutElement;

    public int characterWarpLimit;

    public RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetText(string contentText, string headerText = "")
    {
        if (string.IsNullOrEmpty(headerText))
        {
            header.gameObject.SetActive(false);
        }
        else
        {
            header.gameObject.SetActive(true);
            header.text = headerText;
        }
        content.text = contentText;

        int headerLength = header.text.Length;
        int contentLength = content.text.Length;

        layoutElement.enabled = (headerLength > characterWarpLimit || contentLength > characterWarpLimit) ? true : false;
    }

    private void Update()
    {
        if (Application.isEditor)
        {
            int headerLength = header.text.Length;
            int contentLength = content.text.Length;

            layoutElement.enabled = (headerLength > characterWarpLimit || contentLength > characterWarpLimit) ? true : false;
        }

        Vector2 position = Input.mousePosition;

        Vector2 offset = new Vector2(50, -50);
        //float pivotX = position.x / Screen.width;
        //float pivotY = position.y / Screen.height;

        //rectTransform.pivot = new Vector2(pivotX, pivotY);

        transform.position = position + offset;
    }
}
