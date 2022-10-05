using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] _sounds;
    private AudioSource _audioSource;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
    }
    public void PlaySound()
    {
        _audioSource.clip = _sounds[Random.Range(0, _sounds.Length)];
        _audioSource.Play();
    }
}
