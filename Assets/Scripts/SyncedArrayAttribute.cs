using UnityEngine;

public class SyncedArrayAttribute : PropertyAttribute
{
    public string sizeFieldName;

    public SyncedArrayAttribute(string sizeFieldName)
    {
        this.sizeFieldName = sizeFieldName;
    }
}
