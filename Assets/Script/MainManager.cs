using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MainManager : MonoBehaviour
{
    [SerializeField] Button main_Button;
    [SerializeField] Button exit_Button;

    [SerializeField] GameObject main_Home;
    [SerializeField] GameObject main_App;

    [SerializeField] Vector3 Scale_A;
    [SerializeField] Vector3 Scale_B;

    [SerializeField] Camera mainCamera;
    public List<Camera> cameras = new List<Camera>();
    [SerializeField] List<GameObject> canvasObjs = new List<GameObject> ();
    [SerializeField] List<GameObject> collisionObjs = new List<GameObject>() ;

    [SerializeField] Material Default_White;
    [SerializeField] Material Default_Black;
    [SerializeField] Material Glow_Yellow;

    [SerializeField] float duration = 3;

    public GameObject mainModel_04;

    private GameObject lastOpened;
    private bool isOnCamera = false;
    public void OnHover(Button button)
    {
        StartCoroutine(LerpValue(Scale_A, Scale_B, button));
    }

    public void OffHover(Button button)
    {
        StartCoroutine(LerpValue(Scale_B, Scale_A, button));
    }

    public void EnterMain()
    {
        main_Home.SetActive(false);
        main_App.SetActive(true);
    }
    public void EnterHome()
    {
        main_Home.SetActive(true);
        main_App.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenChapter(GameObject obj)
    {
        if (lastOpened != null)
        {
            lastOpened.SetActive(false);
        }
        obj.SetActive(true);
        lastOpened = obj;
    }

    public void MouseOnObject(GameObject obj)
    {
        obj.GetComponent<MeshRenderer>().material = Glow_Yellow;
    }

    public void MouseExitObject(GameObject obj)
    {
        obj.GetComponent<MeshRenderer>().material = Default_White;
    }

    public void MouseExitObject2(GameObject obj)
    {
        obj.GetComponent<MeshRenderer>().material = Default_Black;
    }
    public void ActiveSwitch(List<GameObject> objs)
    {
        objs[0].SetActive(false);
        objs[1].SetActive(true);
    }
    public void MouseClickObjectActiveCamera(int index)
    {
        if (isOnCamera) return;
        cameras[index].gameObject.SetActive(true);
        canvasObjs[index].SetActive(true);
        mainCamera.gameObject.SetActive(false);
        isOnCamera = true;
        for (int i = 0; i < collisionObjs.Count; i++)
        {
            collisionObjs[i].GetComponent<BoxCollider>().enabled = false;
        }
        mainModel_04.GetComponent<RotationObject>().enabled = false;
    }
    public void BackToDefaultCamera()
    {
        for (int i = 0; i < cameras.Count; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < canvasObjs.Count; i++)
        {
            canvasObjs[i].SetActive(false);
        }
        for (int i = 0; i < collisionObjs.Count; i++)
        {
            collisionObjs[i].GetComponent<BoxCollider>().enabled = true;
        }
        mainCamera.gameObject.SetActive(true);
        isOnCamera = false;
        mainModel_04.GetComponent<RotationObject>().enabled = true;
    }

    IEnumerator LerpValue(Vector3 start, Vector3 end, Button button)
    {
        float timeElapse = 0;

        while (timeElapse < duration)
        {
            float t = timeElapse / duration;
            button.transform.localScale = Vector3.Lerp(start, end, t);
            timeElapse += Time.deltaTime;

            yield return null;
        }

        button.transform.localScale = end;
    }
}
