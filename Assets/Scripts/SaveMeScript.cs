using System.Collections.Generic;
using UnityEngine;

public class PersistentObject : MonoBehaviour
{
    [SerializeField] private string uniqueID;

    private static HashSet<string> existingIDs = new HashSet<string>();

    void Awake()
    {
        if (existingIDs.Contains(uniqueID))
        {
            Destroy(gameObject);
        }
        else
        {
            existingIDs.Add(uniqueID);
            DontDestroyOnLoad(gameObject);
        }
    }
}