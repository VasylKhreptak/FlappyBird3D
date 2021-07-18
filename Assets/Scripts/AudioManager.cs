using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSourceScore;
    [SerializeField] private AudioSource _audioSourceJump;
    [SerializeField] private AudioSource _audioSourcePunch;

    public void PlayPunchClip()
    {
        PlaySound(_audioSourcePunch, 1f, 3f);
    }
    public void PlayScoreClip()
    {
        PlaySound(_audioSourceScore, 1f, 1.1f);
    }
    public void PlayJumpClip()
    {
        PlaySound(_audioSourceJump, 1f, 2f);
    }
    private void PlaySound(AudioSource audioSource, float minPitch, float maxPitch)
    {
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.Play();
    }
}
