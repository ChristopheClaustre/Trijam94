using UnityEngine;
using System;

public class SColor
{
    public float Cyan;
    public float Magenta;
    public float Yellow;
    public float Black;
    public float Alpha;

    public SColor()
    {
        Cyan = 0;
        Magenta = 0;
        Yellow = 0;
        Black = 0;
        Alpha = 0;
    }

    public SColor(Color rgb)
    {
        Black = 1- Mathf.Max(rgb.r, rgb.g, rgb.b);
        Cyan = (1 - rgb.r - Black) / (1 - Black);
        Magenta = (1 - rgb.g - Black) / (1 - Black);
        Yellow = (1 - rgb.b - Black) / (1 - Black);
        Alpha = rgb.a;
    }

    public static SColor operator+(SColor lh, SColor rh)
    {
        SColor res = new SColor();
        res.Cyan = Mathf.Clamp(lh.Cyan + rh.Cyan, 0, 1);
        res.Magenta = Mathf.Clamp(lh.Magenta + rh.Magenta, 0, 1);
        res.Yellow = Mathf.Clamp(lh.Yellow + rh.Yellow, 0, 1);
        res.Black = Mathf.Clamp(lh.Black + rh.Black, 0, 1);
        res.Alpha = Mathf.Clamp(lh.Alpha + rh.Alpha, 0, 1);

        return res;
    }

    public static SColor operator+(SColor lh, Color rh)
    {
        var neorh = new SColor(rh);
        return lh + neorh;
    }

    public Color toColor()
    {
        Color rgb = new Color();
        rgb.r = ( 1 - Cyan ) * ( 1 - Black );
        rgb.g = ( 1 - Magenta ) * ( 1 - Black );
        rgb.b = ( 1 - Yellow ) * ( 1 - Black );
        rgb.a = Alpha;

        return rgb;
    }
}
