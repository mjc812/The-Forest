using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class FoundationPreview : AbstractPreview
{
    void Awake()
    {
        snappingRequired = false;
        snappingPoints = new string[] { "FoundationSnapPoint" };
    }
}
