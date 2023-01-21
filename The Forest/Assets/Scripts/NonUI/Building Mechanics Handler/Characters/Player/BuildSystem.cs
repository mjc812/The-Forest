using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour {

    [SerializeField] private LayerMask layer;
    [SerializeField] private GameObject foundationPreview;
    [SerializeField] private GameObject wallPreview;
    [SerializeField] private GameObject celingPreview;

    private GameObject previewPrefab = null;
    private AbstractPreview previewScript = null;

    private float snapTolerance = 0.1f;

    private bool building = false;
    //private bool pauseBuilding = false;
    private float mouseXAcc = 0;
    private float mouseYAcc = 0;


    void Update() {
        BuildRayCast();
    }

    public bool IsBuilding()
    {
        return building;
    }

    public void SnapEscaped()
    {
        mouseXAcc = 0;
        mouseYAcc = 0;
    }

    public void UpdateSnapThresholdAccumulators(float mouseX, float mouseY)
    {
        if (IsBuilding() && previewScript.GetSnapped())
        {
            mouseXAcc += mouseX;
            mouseYAcc += mouseY;
        }
    }

    private bool CanExcapeSnap()
    {
        return (mouseXAcc >= snapTolerance || mouseYAcc >= snapTolerance);
    }

    public void BuildFoundation()
    {
        previewPrefab = Instantiate(foundationPreview, Vector3.zero, Quaternion.identity);
        previewScript = previewPrefab.GetComponent<AbstractPreview>();
        building = true;
        SnapEscaped();
    }

    public void BuildWall()
    {
        previewPrefab = Instantiate(wallPreview, Vector3.zero, Quaternion.identity);
        previewScript = previewPrefab.GetComponent<AbstractPreview>();
        building = true;
        SnapEscaped();
    }

    public void CancelBuild()
    {
        Destroy(previewPrefab);
        previewPrefab = null;
        previewScript = null;
        building = false;
        SnapEscaped();
    }

    public void DoBuild()
    {
        previewScript.PlaceStructure();
        previewPrefab = null;
        previewScript = null;
        building = false; 
    }

    private void BuildRayCast()
    {
        if (IsBuilding() && (!previewScript.GetSnapped() || CanExcapeSnap()))
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 40f, 7))
            {
                SnapEscaped();
                float y = hit.point.y + (previewPrefab.transform.localScale.y / 2f);
                Vector3 pos = new Vector3(hit.point.x, y, hit.point.z);
                previewPrefab.transform.position = pos;
            }
        }
    }
}
