using System;
using UnityEngine;

[Serializable]
public struct EventTag
{
    // String reference: Event Tag Drawer
    [SerializeField] public int Id;

    public static bool operator ==(EventTag a, EventTag b) => a.Id == b.Id;
    public static bool operator !=(EventTag a, EventTag b) => a.Id != b.Id;
    public override bool Equals(object obj) => Id == ((EventTag)obj).Id;
    public override int GetHashCode() => Id.GetHashCode();
}