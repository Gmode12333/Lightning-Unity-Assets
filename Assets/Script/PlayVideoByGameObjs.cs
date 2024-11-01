using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayVideoByGameObjs : MonoBehaviour
{
    public VideoPlayer player;
    public VideoClip clip;
    public RenderTexture texture;
    public GameObject endedOpenObjs;
    public bool canLoop;

    private void OnEnable()
    {
        player.isLooping = canLoop;
        player.clip = clip;
        player.targetTexture = texture;
        player.loopPointReached += VideoEneded;
    }

    void VideoEneded(VideoPlayer vp)
    {
        vp.isLooping = true;
        transform.gameObject.SetActive(false);
        endedOpenObjs.SetActive(true);
    }
}
