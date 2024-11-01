using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chap5_Handle : MonoBehaviour
{
    public List<Button> buttons;
    public List<GameObject> gameObjects;

    public GameObject canvasCamera1;
    public GameObject canvasCamera2;
    public void ClickHandle(Button button)
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            if (gameObjects[i].name == button.name)
            {
                gameObjects[i].SetActive(true);
            }
            else
            {
                gameObjects[i].SetActive(false);
            }
        }
    }
    private void OnDisable()
    {
        if (canvasCamera1 && canvasCamera2)
        {
            canvasCamera1.SetActive(false);
            canvasCamera2.SetActive(false);
        }
    }
}
