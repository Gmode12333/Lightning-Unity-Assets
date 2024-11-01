using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class Chap3_Handle : MonoBehaviour
{
    public List<Button> buttons;
    public List<GameObject> gameObjects;

    private GameObject lastOpened;
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
}