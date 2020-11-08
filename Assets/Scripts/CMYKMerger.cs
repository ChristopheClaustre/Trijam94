using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class CMYKMerger : MonoBehaviour
{
    public List<Texture2D> orignalMasks;
    public List<Color> orignalColors;

    private List<UpholsteringData> datum;

    private Texture2D selectedMask;
    private Color selectedColor;

    public int width = 100;
    public int height = 100;
    protected Texture2D result;

    private RawImage rawImage;

    public List<UpholsteringData> Datum
    {
        get => datum;
        set
        {
            datum = value;
            ApplyTexture();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        datum = new List<UpholsteringData>();
        rawImage = GetComponent<RawImage>();
        
        result = new Texture2D(width, height, TextureFormat.RGBA32, false);
        result.name = "GeneratedTexture";
        ApplyTexture();

        rawImage.texture = result;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ApplyTexture()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                SColor res = new SColor();
                for (int k = 0; k < datum.Count; k++)
                {
                    var data = datum[k];
                    if (data._mask.GetPixel(i, j).a == 1)
                    {
                        res = res + data._color;
                    }
                }

                result.SetPixel(i,j, res.toColor());
            }
        }

        result.Apply();
    }

    public void SetSelectedColor(int index)
    {
        selectedColor = orignalColors[index];
    }

    public void SetSelectedMask(int index)
    {
        selectedMask = orignalMasks[index];
    }

    public void Paint()
    {
        var data = new UpholsteringData(selectedMask, selectedColor);
        datum.Add(data);

        ApplyTexture();
    }

    public void Clear()
    {
        datum.Clear();
        
        ApplyTexture();
    }
}
