using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    qte, // 
    button, // 
    spray,
    fail,   // 
}
public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManager;
    // public List<GameObject> soundObjList = new List<GameObject>();
    public List<AudioClip> soundClipList = new List<AudioClip>();
    public GameObject SoundObj;
    // Start is called before the first frame update

    void Awake()
    {
        soundManager = this;
    }
    public void playSound(SoundType type)
    {
        GameObject soundTmp =  Instantiate(SoundObj , transform.position, Quaternion.identity);
        AudioSource audioSource = soundTmp.GetComponent<AudioSource>();
        audioSource.clip = soundClipList[(int)type];
        audioSource.Play();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
    }
}
