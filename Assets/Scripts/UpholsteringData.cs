using UnityEngine;
using System;
using System.Collections.Generic;

[System.Serializable]
public class UpholsteringData : IEquatable<UpholsteringData>
{
    public Texture2D _mask;
    public Color _color;

    public UpholsteringData(Texture2D mask, Color color)
    {
        _mask = mask;
        _color = color;
    }

    public static bool operator==(UpholsteringData lh, UpholsteringData rh)
    {
        return ! (lh != rh);
    }

    public static bool operator!=(UpholsteringData lh, UpholsteringData rh)
    {
        return lh._color != rh._color || lh._mask != rh._mask;
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;
        UpholsteringData objAsData = obj as UpholsteringData;
        if (objAsData == null)
            return false;
        else
            return Equals(objAsData);
    }

    public bool Equals(UpholsteringData other)
    {
        return this == other;
    }
}

[Serializable]
public class Order
{
    public List<UpholsteringData> datum;

    public bool IsCandidateGood(List<UpholsteringData> candidate)
    {
        if (candidate.Count != datum.Count)
        {
            return false;
        }

        for (int i = 0; i < candidate.Count; i++)
        {
            if (! Contains(candidate[i]))
                return false;
        }

        return true;
    }
    
    public bool Contains(UpholsteringData candidate)
    {
        return datum.Contains(candidate);
    }
}