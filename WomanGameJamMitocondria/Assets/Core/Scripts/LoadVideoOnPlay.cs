using System.IO;
using UnityEngine;
using UnityEngine.Video;

public class LoadVideoOnPLay : MonoBehaviour
{
    void Start()
    {
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.clip = Resources.Load<VideoClip>("Yuko_Cute_w_Audio");
        videoPlayer.Play();
    }
}
