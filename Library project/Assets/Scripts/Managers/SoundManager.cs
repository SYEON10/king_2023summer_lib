using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Sound
//Player(AudioSource), Source(AudioClip), Listener(AudioListener)
public class SoundManager
{
    private AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];
    private Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();


    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);
            
            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
            for(int i = 0; i < soundNames.Length -1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)Define.Sound.Bgm].loop = true;
        }
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClips.Clear();
    }
    
    public void Play(string path, Define.Sound type, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch);
    }
    
    public void Play(AudioClip audioClip, Define.Sound type, float pitch = 1.0f)
    {
        if (audioClip == null)
        {
            Debug.Log($"@WARNING@ AudioClip is missing. ");
            return;
        }
        
        if (type == Define.Sound.Bgm)
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];

            if (audioSource.isPlaying)
                audioSource.Stop();
            
            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else if (type == Define.Sound.Effect)
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    AudioClip GetOrAddAudioClip(string path, Define.Sound type)
    {
        if (path.Contains("Sound/") == false)
            path = $"Sound/{path}";
        
        AudioClip audioClip = null;
        
        if (type == Define.Sound.Bgm)
        {
            audioClip = GameManager.Resources.Load<AudioClip>(path);
        }
        else if (type == Define.Sound.Effect)
        {
            if (_audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = GameManager.Resources.Load<AudioClip>(path);
                _audioClips.Add(path, audioClip);
            }
        }
        
        if (audioClip == null)
            Debug.Log($"@WARNING@ AudioClip is missing. path : {path}");
        
        return audioClip;
    }
}
