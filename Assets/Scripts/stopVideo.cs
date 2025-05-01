using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroPlayer : MonoBehaviour
{
    public GameObject Video;
    [SerializeField]
    VideoPlayer myVideoPlayer;

    void Start()
    {
        //when video is finished call StopVideo function
        myVideoPlayer.loopPointReached += StopVideo;
   
    }
    void StopVideo(VideoPlayer videoPlayer)
    {
        Debug.Log("yay video is done");
        Video.SetActive(false);
    }
   
}
