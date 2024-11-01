using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityEngine.Video;

public class Simulation2 : MonoBehaviour
{
    [Header("TextMeshPro")]
    public TextMeshProUGUI elec_In;
    public TextMeshProUGUI elec_Out;
    public TextMeshProUGUI light_In;
    public TextMeshProUGUI light_F1;
    public TextMeshProUGUI light_F2;
    public TextMeshProUGUI cond_F1;
    public TextMeshProUGUI cond_F2;

    [Header("Slider And Value")]
    public Slider lightInSlider;
    public TextMeshProUGUI lightTextValue;

    [Header("Simulation Button")]
    public GameObject buttonObj;

    [Header("Light GameObject")]
    public GameObject F2Light;

    public GameObject F1Light_01;
    public GameObject F1Light_02;
    public GameObject F1Light_03;
    public GameObject F1Light_04;
    public GameObject F1Light_05;

    [Header("Material")]
    public Material _normal;
    public Material _glow;

    private float lightningValue;
    private float F1CondValue;
    private bool F2CondValue;

    [Header("Wire")]
    public List<GameObject> wireObjs = new List<GameObject>();

    [Header("Path")]
    public List<GameObject> startPath = new List<GameObject>();

    public List<GameObject> objs1 = new List<GameObject>();

    public List<GameObject> objs2 = new List<GameObject>();

    [Header("Mixer")]
    public AudioMixer mixer;

    [Header("Post Volume")]
    public PostProcessVolume volume;
    private Bloom bloom;

    [Header("VideoPlayer")]
    public VideoPlayer vp;
    public GameObject videoObj;

    private HandleSound sound;
    public void GetLightningValue()
    {
        lightningValue = lightInSlider.value;
        lightTextValue.text = "I<size=10>set</size> = (<color=#0000FF>" + lightningValue + "kA</color>)";
        float dbValue = (lightningValue - 20) / 4;
        mixer.SetFloat("Master", dbValue);
        float iValue = 10 + (lightningValue - 20) / 8;
        bloom.intensity.value = iValue;
    }

    // Start is called before the first frame update
    void Start()
    {
        lightInSlider.minValue = 20f;
        lightInSlider.maxValue = 100f;
        lightningValue = 20f;
        float dbValue = (lightningValue - 20) / 4;
        mixer.SetFloat("Master", dbValue);

        float iValue = 10 + (lightningValue - 20) / 8;
        if (volume.profile.TryGetSettings(out bloom)){
            bloom.intensity.value = iValue;
        }
        F1CondValue = 100f;
        F2CondValue = true;

        lightTextValue.text = "I<size=25>set</size> = (<color=#0000FF>" + lightningValue + "kA</color>)";

        elec_In.text = "Dòng điện đầu vào: <color=#0000FF>32A</color>";
        elec_Out.text = "Dòng điện đầu ra: <color=#0000FF>32A</color>";
        light_In.text = "Dòng sét đầu vào: <color=#0000FF>0kA</color>";
        light_F1.text = "Dòng sét sau khi qua F1: <color=#0000FF>0kA</color>";
        light_F2.text = "Dòng sét sau khi qua F2: <color=#0000FF>0kA</color>";

        cond_F1.text = "Khả năng hoạt động của F1 còn lại: <color=#008000>" + F1CondValue + "%</color>";
        cond_F2.text = "Khả năng sử dụng của F2: <color=#008000>Hoạt động tốt</color>";

        F2Light.GetComponent<MeshRenderer>().material = _glow;

        F1Light_01.GetComponent<MeshRenderer>().material = _glow;
        F1Light_02.GetComponent<MeshRenderer>().material = _glow;
        F1Light_03.GetComponent<MeshRenderer>().material = _glow;
        F1Light_04.GetComponent<MeshRenderer>().material = _glow;
        F1Light_05.GetComponent<MeshRenderer>().material = _glow;

        sound = GetComponent<HandleSound>();
    }

    public void StartSimulation()
    {
        StartCoroutine(StartLight12());
        sound.PlayAudio(1.9f, 3f);
        elec_In.text = "Dòng điện đầu vào: <color=#0000FF>32A";
        elec_Out.text = "Dòng điện đầu ra: <color=#0000FF>32A";
        light_In.text = "Dòng sét đầu vào: <color=#0000FF>" + lightningValue.ToString() + "kA</color>";
        light_F1.text = "Dòng sét sau khi qua F1: <color=#0000FF>" + (lightInSlider.value - lightInSlider.value * 0.7f).ToString() + "kA</color>";
        light_F2.text = "Dòng sét sau khi qua F2: <color=#0000FF>0kA</color>";

        F1CondValue = F1CondValue - lightInSlider.value * 0.1f;
        F2CondValue = false;
        F2Light.GetComponent<MeshRenderer>().material = _normal;

        cond_F1.text = "Khả năng hoạt động của F1 còn lại: <color=#008000>" + F1CondValue + "%</color>";

        if (F2CondValue)
        {
            cond_F2.text = "Khả năng sử dụng của F2: <color=#008000>Hoạt động tốt</color>";
        }
        else
        {
            cond_F2.text = "Khả năng sử dụng của F2: <color=#FF0000>Cần thay thế</color>";
        }

        if (F1CondValue / 20f <= 5 && F1CondValue / 20f > 4)
        {
            F1Light_01.GetComponent<MeshRenderer>().material = _glow;
            F1Light_02.GetComponent<MeshRenderer>().material = _glow;
            F1Light_03.GetComponent<MeshRenderer>().material = _glow;
            F1Light_04.GetComponent<MeshRenderer>().material = _glow;
            F1Light_05.GetComponent<MeshRenderer>().material = _glow;
        }
        else if (F1CondValue / 20f <= 4 && F1CondValue / 20f > 3)
        {
            F1Light_01.GetComponent<MeshRenderer>().material = _normal;
            F1Light_02.GetComponent<MeshRenderer>().material = _glow;
            F1Light_03.GetComponent<MeshRenderer>().material = _glow;
            F1Light_04.GetComponent<MeshRenderer>().material = _glow;
            F1Light_05.GetComponent<MeshRenderer>().material = _glow;
        }
        else if (F1CondValue / 20f <= 3 && F1CondValue / 20f > 2)
        {
            F1Light_01.GetComponent<MeshRenderer>().material = _normal;
            F1Light_02.GetComponent<MeshRenderer>().material = _normal;
            F1Light_03.GetComponent<MeshRenderer>().material = _glow;
            F1Light_04.GetComponent<MeshRenderer>().material = _glow;
            F1Light_05.GetComponent<MeshRenderer>().material = _glow;
        }
        else if (F1CondValue / 20f <= 2 && F1CondValue / 20f > 1)
        {
            F1Light_01.GetComponent<MeshRenderer>().material = _normal;
            F1Light_02.GetComponent<MeshRenderer>().material = _normal;
            F1Light_03.GetComponent<MeshRenderer>().material = _normal;
            F1Light_04.GetComponent<MeshRenderer>().material = _glow;
            F1Light_05.GetComponent<MeshRenderer>().material = _glow;
        }
        else if (F1CondValue / 20f <= 1 && F1CondValue / 20f > 0)
        {
            F1Light_01.GetComponent<MeshRenderer>().material = _normal;
            F1Light_02.GetComponent<MeshRenderer>().material = _normal;
            F1Light_03.GetComponent<MeshRenderer>().material = _normal;
            F1Light_04.GetComponent<MeshRenderer>().material = _normal;
            F1Light_05.GetComponent<MeshRenderer>().material = _glow;
        }
        else
        {
            F1Light_01.GetComponent<MeshRenderer>().material = _normal;
            F1Light_02.GetComponent<MeshRenderer>().material = _normal;
            F1Light_03.GetComponent<MeshRenderer>().material = _normal;
            F1Light_04.GetComponent<MeshRenderer>().material = _normal;
            F1Light_05.GetComponent<MeshRenderer>().material = _normal;
        }
    }

    public void ResetF2Condition()
    {
        if (F2CondValue == true) return;
        F2CondValue = true;
        buttonObj.SetActive(true);
        lightInSlider.transform.parent.gameObject.SetActive(true);
        cond_F2.text = "Khả năng sử dụng của F2: <color=#008000>Hoạt động tốt</color>";
        F2Light.GetComponent<MeshRenderer>().material = _glow;
        videoObj.SetActive(false);
        vp.clip = null;

        StopAllCoroutines();

        for (int i = 0; i < startPath.Count; i++)
        {
            startPath[i].GetComponent<GetMaterials>().DefaultMaterials();
        }

        for (int i = 0; i < objs2.Count; i++)
        {
            objs2[i].GetComponent<GetMaterials>().DefaultMaterials();
        }

        for (int i = 0; i < objs1.Count; i++)
        {
            objs1[i].GetComponent<GetMaterials>().DefaultMaterials();
        }
    }

    public void ResetF1Condition()
    {
        if (F1CondValue > 40f) return;
        F1CondValue = 100f;
        buttonObj.SetActive(true);
        lightInSlider.transform.parent.gameObject.SetActive(true);
        cond_F1.text = "Khả năng hoạt động của F1 còn lại: <color=#008000>" + F1CondValue + "%</color>";
        F1Light_01.GetComponent<MeshRenderer>().material = _glow;
        F1Light_02.GetComponent<MeshRenderer>().material = _glow;
        F1Light_03.GetComponent<MeshRenderer>().material = _glow;
        F1Light_04.GetComponent<MeshRenderer>().material = _glow;
        F1Light_05.GetComponent<MeshRenderer>().material = _glow;
    }

    private void Update()
    {
        if (!F2CondValue)
        {
            buttonObj.SetActive(false);
            lightInSlider.transform.parent.gameObject.SetActive(false);
            F2Light.GetComponent<MeshRenderer>().material = _normal;
        }

        if (F1CondValue <= 40f)
        {
            cond_F1.text = "Khả năng hoạt động của F1 còn lại: <color=#FF0000>" + F1CondValue + "% (Cần thay thế)</color>";
            buttonObj.SetActive(false);
            lightInSlider.transform.parent.gameObject.SetActive(false);
        }
    }

    IEnumerator StartLight12()
    {
        vp.isLooping = false;
        for (int i = 0; i < startPath.Count; i++)
        {
            startPath[i].GetComponent<GetMaterials>().ChangeMaterials();
            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < startPath.Count; i++)
        {
            startPath[i].GetComponent<GetMaterials>().DefaultMaterials();
        }

        StartCoroutine(StartLight2());
        yield return new WaitForSeconds(2f);
        StartCoroutine(StartLight3());
    }

    IEnumerator StartLight2()
    {
        for (int i = 0; i < objs1.Count; i++)
        {
            objs1[i].GetComponent<GetMaterials>().ChangeMaterials();
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator StartLight3()
    {
        for (int i = 0; i < objs2.Count; i++)
        {
            objs2[i].GetComponent<GetMaterials>().ChangeMaterials();
            yield return new WaitForSeconds(1.75f);
        }

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < objs2.Count; i++)
        {
            objs2[i].GetComponent<GetMaterials>().DefaultMaterials();
        }

        for (int i = 0; i < objs1.Count; i++)
        {
            objs1[i].GetComponent<GetMaterials>().DefaultMaterials();
        }

        vp.time = 0;
        vp.Pause();
    }
}
