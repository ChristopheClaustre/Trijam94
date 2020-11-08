using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Toggle))]
public class ToggleManager : MonoBehaviour
{
    public Sprite on;
    public Sprite off;

    private Image image;
    private Toggle toggle;

    public UnityEvent onToggleOn;

    private bool isOn = false;

    void Start()
    {
        toggle = GetComponent<Toggle>();
        image = GetComponent<Image>();

        image.sprite = isOn ? off : on;
    }

    void Update()
    {
        if (isOn != toggle.isOn)
        {
            isOn = toggle.isOn;
            image.sprite = isOn ? off : on;
            if (isOn)
                onToggleOn.Invoke();
        }
    }
}
