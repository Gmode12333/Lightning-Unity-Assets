using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonColor : MonoBehaviour
{
    public Color color;

    public Color default_Color; 

    public List<Button> buttons = new List<Button>();

    public void DisableButtonOnClick(int index)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].interactable = true;
        }

        if (buttons[index])
        {
            buttons[index].interactable = false;
        }
    }

    public void ResetButtonColor()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].interactable = true;
        }
    }

    public void ChangeColorOnClickButton(Button button)
    {
        button.GetComponent<Image>().color = color;
    }

    public void ChangeColorOffClickButton(Button button)
    {
        button.GetComponent<Image>().color = default_Color;
    }
}
