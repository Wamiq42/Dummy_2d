using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingScript : MonoBehaviour
{
    [SerializeField] public AudioSource playerAudioSource;

    private SpriteRenderer spriteRenderer;
    //private int sampleRate = 64;
    private PlayerState playerState = PlayerState.idle;
    private float[] clipSampleData = new float[2048];

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        Idle();
    }
    private void Update()
    {
        if (playerState == PlayerState.idle)
        {
            SwitchStates();

           
        }
      
      
   
    }

    private void Idle()
    {
       
        if (playerAudioSource.clip != null && IsVolumeAboveThreshold())
        {
            playerAudioSource.Stop();
            playerAudioSource.clip = null;

        }
        spriteRenderer.color = Color.blue;
        playerAudioSource.clip = Microphone.Start(null, true, 1, 44100);
    }

    private void Listen()
    {
        playerAudioSource.clip = Microphone.Start(null, true, 5, 44100);
        spriteRenderer.color = Color.red;
        Invoke("SwitchStates", 5);

    }

    private void Talk()
    {
        Microphone.End(null);
        if (playerAudioSource.clip != null)
        {
            spriteRenderer.color = Color.green;            
            playerAudioSource.Play();
          
        }
        Invoke("SwitchStates", 5);
    }

    bool IsVolumeAboveThreshold()
    {
        if (playerAudioSource.clip !=null)
        {
            return false;
        }
        playerAudioSource.clip.GetData(clipSampleData, playerAudioSource.timeSamples);
        var clipLoudness = 0f;
        foreach (var item in clipSampleData)
        {
            clipLoudness += Mathf.Abs(item);
        }
        clipLoudness /= 1024;

        return clipLoudness > 0.025f;
    }
    void SwitchStates()
    {
        switch (playerState)
        {
            case PlayerState.idle:
                playerState = PlayerState.listen;
                Listen();
                break;
            case PlayerState.listen:
                playerState = PlayerState.talk;
                Talk();
                break;
            case PlayerState.talk:
                playerState = PlayerState.idle;
                Idle();
                break;
            
        }
    }
    public void LowPitch()
    {
        playerAudioSource.pitch = 0.8f;
    }
    public void NormalPitch()
    {
        playerAudioSource.pitch = 1;
    }
    public void HighPitch()
    {
        playerAudioSource.pitch = 1.5f;
    }
    enum PlayerState { idle,listen,talk}
}
