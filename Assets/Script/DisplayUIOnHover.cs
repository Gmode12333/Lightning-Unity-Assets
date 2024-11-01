using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DisplayUIOnHover : MonoBehaviour
{
    [SerializeField]
    private GameObject _displayObject;

    [SerializeField]
    private Vector3 _defaultPos;

    [SerializeField]
    private Vector3 _targetPos;

    [SerializeField]
    private Vector3 _defaultScale;

    [SerializeField]
    private Vector3 _targetScale;
    public void OnClick()
    {
        //if (_isDebouncing) return;
        //_isDebouncing = true;
        //Invoke(nameof(OnDebounceTimeout), _debounceTime);
        StartCoroutine(LerpValue(_defaultPos, _targetPos, _targetScale, _displayObject, true));
    }

    public void OffClick()
    {
        //if (_isDebouncing) return;
        //_isDebouncing = true;
        //Invoke(nameof(OnDebounceTimeout), _debounceTime);
        StartCoroutine(LerpValue(_targetPos, _defaultPos, _defaultScale, _displayObject, true));
    }

    IEnumerator LerpValue(Vector3 start, Vector3 end, Vector3 scale, GameObject obj, bool enable)
    {
        float timeElapse = 0;
        obj.gameObject.SetActive(enable);
        while (timeElapse < 0.2)
        {
            float t = timeElapse / 0.2f;
            obj.transform.localPosition = Vector3.Lerp(start, end, t);
            obj.transform.localScale = Vector3.Lerp(obj.transform.localScale, scale, t);
            timeElapse += Time.deltaTime;

            yield return null;
        }

        obj.transform.localPosition = end;
        obj.transform.localScale = scale;
    }
}
