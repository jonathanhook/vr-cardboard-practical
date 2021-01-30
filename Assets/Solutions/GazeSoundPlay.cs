using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GazeSoundPlay : MonoBehaviour
{
    public string targetTag = "GazeSoundObject";
    public float gazeTimeSeconds = 1.0f;
    public Color playingColor = Color.green;
    public Color playedColor = Color.grey;

    private AudioSource currentSound = null;
    private GameObject lookingAt = null;
    private float startedLooking;

    void Update()
    {
        if (currentSound == null)
        {
            RaycastHit thingHit;
            bool hasHit = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out thingHit, Mathf.Infinity);

            if (hasHit)
            {
                GameObject target = thingHit.collider.gameObject;
                if (target.tag == targetTag)
                {
                    if(lookingAt == target && Time.timeSinceLevelLoad - startedLooking > gazeTimeSeconds)
                    {
                        AudioSource sound = target.GetComponent<AudioSource>();
                        sound.Play();

                        Image sprite = target.GetComponentInChildren<Image>();
                        sprite.color = playingColor;

                        currentSound = sound;
                    }
                    else if(lookingAt == null)
                    {
                        lookingAt = target;
                        startedLooking = Time.timeSinceLevelLoad;
                    }
                }

            }
        }
        else if(!currentSound.isPlaying)
        {
            Image sprite = lookingAt.GetComponentInChildren<Image>();
            sprite.color = playedColor;

            currentSound = null;
            lookingAt = null;
        }
    }

}
