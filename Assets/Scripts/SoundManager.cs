using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    public void playClipAtPosition(Vector3 position, AudioClip clip)
    {
        GameObject sound = ObjectPooler.Instance.GetOrCreateGameObjectFromPool(ObjectPooler.PoolObject.Sound);
        sound.transform.position = position;

        AudioSource audio = sound.GetComponent<AudioSource>();
        audio.clip = clip;
        audio.Play();
        StartCoroutine(DisableAudioSource(audio, clip.length));
    }

    IEnumerator DisableAudioSource(AudioSource audioSource, float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        audioSource.gameObject.SetActive(false);
    }
}
