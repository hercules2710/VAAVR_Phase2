using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;

public class VideoPlay : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public VideoPlayer videoPlay;
    public AudioSource audio;
    public Slider audioVolume;
    Slider tracking;
    bool slide = false;

    // Start is called before the first frame update
    void Start()
    {
        tracking = GetComponent<Slider>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        slide = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        float frame = (float)tracking.value * (float)videoPlay.frameCount;
        videoPlay.frame = (long)frame;
        slide = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!slide)
        {
            tracking.value = (float)videoPlay.frame / (float)videoPlay.frameCount;
        }
    }

    public void volumeBtn()
    {
        audio.volume = audioVolume.value;
    }

    public void PlayBtn()
    {
        videoPlay.Play();
    }
    public void PauseBtn()
    {
        videoPlay.Pause();
    }
}
