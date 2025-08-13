using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMusicOnPlay : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.PlayMusic(MusicPiece.Natural, gameObject);
    }
}
