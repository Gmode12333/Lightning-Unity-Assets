using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Simulation1 : MonoBehaviour
{
    [Header("UI And TextMeshPro")]
    [SerializeField] Button Add;
    [SerializeField] Button Dev;

    [SerializeField] TextMeshProUGUI VoltIn;
    [SerializeField] TextMeshProUGUI VoltOut;

    [SerializeField] Slider slider1;
    [SerializeField] TextMeshProUGUI text1;


    [SerializeField] Slider slider2;
    [SerializeField] TextMeshProUGUI text2;


    [SerializeField] Slider slider3;
    [SerializeField] TextMeshProUGUI text3;


    private float currentVolt = 0;

    private float pressure1CurrentValue;
    private float pressure2CurrentValue;
    private float pressure3CurrentValue;

    [Header("Pressure Value")]

    [SerializeField, Range(0, 10)]
    public int pressure1BeginValue = 0;

    [SerializeField, Range(220, 330)]
    public int pressure2BeginValue = 260;

    [SerializeField, Range(0, 30)]
    public int pressure3BeginValue = 10;

    [Header("GameObjects")]
    [SerializeField]
    private GameObject redLight;

    [SerializeField]
    private GameObject blackSwitch;

    [SerializeField]
    private GameObject pressure1Obj;

    [SerializeField]
    private GameObject pressure2Obj;

    [SerializeField]
    private GameObject pressure3Obj;

    [Header("Materials")]
    public Material red_Neon;
    public Material blue_Neon;
    public Material white_Neon;

    [Header("Image")]
    public GameObject imageObj;
    public Sprite image1;
    public Sprite image2;

    [Header("Wire")]
    public List<GameObject> wireObjs = new List<GameObject>();

    private HandleSound handleSound;
    public void GetSilder1Value()
    {
        text1.text = "Thời gian phản ứng: " + slider1.value + "s";
        pressure1CurrentValue = slider1.value;
        pressure1Obj.transform.rotation = Quaternion.Euler(-130f + (18f * slider1.value), 90f, -90f);
    }

    public void GetSilder2Value()
    {
        text2.text = "Điện áp cắt: " + slider2.value + "V";
        pressure2CurrentValue = slider2.value;
        float mappedValue = (slider2.value - 220) / (330 - 220) * 11;
        pressure2Obj.transform.rotation = Quaternion.Euler(-110f + (27f * mappedValue), 90f, -90f);
    }

    public void GetSilder3Value()
    {
        text3.text = "Thời gian nối thông: " + slider3.value + "s";
        pressure3CurrentValue = slider3.value;
        pressure3Obj.transform.rotation = Quaternion.Euler(-180f + (9f * slider3.value), 90f, -90f);
    }

    public void AddVolt()
    {
        currentVolt += 1;
        VoltIn.text = currentVolt.ToString() + "V";
    }

    public void DevVolt()
    {
        currentVolt -= 1;
        if (currentVolt <= 0)
        {
            currentVolt = 0;
            return;
        }
        VoltIn.text = currentVolt.ToString() + "V";
    }
    void Start()
    {
        handleSound = GetComponent<HandleSound>();

        slider1.minValue = 0;
        slider1.maxValue = 10;
        slider2.minValue = 220;
        slider2.maxValue = 330;
        slider3.minValue = 0;
        slider3.maxValue = 30;

        currentVolt = 220;

        pressure1CurrentValue = pressure1BeginValue;
        pressure2CurrentValue = pressure2BeginValue;
        pressure3CurrentValue = pressure3BeginValue;

        slider1.value = pressure1CurrentValue;
        slider2.value = pressure2CurrentValue;
        slider3.value = pressure3CurrentValue;

        text1.text = "Thời gian phản ứng: " + pressure1CurrentValue + "s";
        text2.text = "Điện áp cắt: " + pressure2CurrentValue + "V";
        text3.text = "Thời gian nối thông: " + pressure3CurrentValue + "s";

        StartCoroutine(UpdateVoltValue());

        foreach (GameObject wire in wireObjs)
        {
            wire.GetComponent<GetMaterials>().ChangeMaterials();
        }

        imageObj.GetComponent<Image>().sprite = image2;
    }

    public bool CheckingVoltInAndOut()
    {
        if (currentVolt <= pressure2CurrentValue)
        {
            VoltIn.text = currentVolt.ToString() + "V";
            VoltOut.text = currentVolt.ToString() + "V";
            return false;
        }
        else
        {
            VoltIn.text = currentVolt.ToString() + "V";
            VoltOut.text = "0V";
            return true;
        }
    }

    IEnumerator UpdateVoltValue()
    {
        while (true)
        {
            if (CheckingVoltInAndOut())
            {
                redLight.GetComponent<MeshRenderer>().enabled = true;
                blackSwitch.SetActive(true);
                foreach (GameObject wire in wireObjs)
                {
                    if (wire.CompareTag("2"))
                    {
                        wire.GetComponent<GetMaterials>().DefaultMaterials();
                    }
                }
                imageObj.GetComponent<Image>().sprite = image1;
                handleSound.PlayAudio(0, 1);
                while (currentVolt > pressure2CurrentValue)
                {
                    yield return new WaitForSeconds(pressure1CurrentValue + 0.1f);
                }
                yield return new WaitForSeconds(pressure3CurrentValue + 0.1f);
                if (CheckingVoltInAndOut())
                {
                    redLight.GetComponent<MeshRenderer>().enabled = true;
                    blackSwitch.SetActive(true);
                    foreach (GameObject wire in wireObjs)
                    {
                        if (wire.CompareTag("2"))
                        {
                            wire.GetComponent<GetMaterials>().DefaultMaterials();
                        }
                    }
                    imageObj.GetComponent<Image>().sprite = image1;
                    handleSound.PlayAudio(0, 1);
                }
                else
                {
                    redLight.GetComponent<MeshRenderer>().enabled = false;
                    blackSwitch.SetActive(false);
                    foreach (GameObject wire in wireObjs)
                    {
                        wire.GetComponent<GetMaterials>().ChangeMaterials();
                    }
                    imageObj.GetComponent<Image>().sprite = image2;
                    handleSound.PlayAudio(0, 1);
                }
            }
            yield return new WaitForSeconds(pressure1CurrentValue + 0.1f);
        }
    }
}
