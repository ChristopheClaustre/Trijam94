using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RackEventManager : MonoBehaviour
{
    private AudioSource source;
    private CMYKMerger merger;

    public UIUpdate uIUpdateScript = null;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        merger = GetComponentInChildren<CMYKMerger>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayClip(AudioClip clip)
    {
        Debug.Assert(clip);
        source.PlayOneShot(clip);
    }

    public void Paint()
    {
        merger.Paint();
    }

    public void Clear()
    {
        merger.Clear();
    }

    public void UpdateOrders()
    {
        uIUpdateScript.UpdateOrders();
    }

    public void UpdateLayerCount()
    {
        uIUpdateScript.UpdateLayerCount();
    }
}
