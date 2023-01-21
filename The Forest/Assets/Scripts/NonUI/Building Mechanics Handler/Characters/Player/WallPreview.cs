using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using UnityEngine;

public class WallPreview : AbstractPreview
{
    void Awake()
    {
        snappingRequired = true;
        snappingPoints = new string[] { "WallSnapPoint" };
    }
}
