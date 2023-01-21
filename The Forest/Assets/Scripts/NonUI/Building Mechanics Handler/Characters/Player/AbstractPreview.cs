using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPreview : MonoBehaviour
{
    [SerializeField] private GameObject structurePrefab;
    [SerializeField] private Material validMeshColor;
    [SerializeField] private Material inValidMeshColor;

    private MeshRenderer meshRenderer;
    private BuildSystem buildSystem;

    protected string[] snappingPoints;
    protected bool snappingRequired;
    public bool isSnappingRequired
    {
        get => snappingRequired;
    }

    private bool snapped = false;

    void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        buildSystem = player.gameObject.GetComponent<BuildSystem>();
    }

    void Update()
    {
        Debug.Log(snapped);
    }

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        SetMeshColor();
    }

    public void PlaceStructure()
    {
        if (snapped || !snappingRequired)
        {
            Instantiate(structurePrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public bool GetSnapped()
    {
        return snapped;
    }

    private void SetMeshColor()
    {
        if (!snappingRequired || snapped)
        {
            meshRenderer.material = validMeshColor;
        }
        else
        {
            meshRenderer.material = inValidMeshColor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (snappingPoints.Contains(other.tag))
        {
            Debug.Log(other.tag);
            transform.position = other.transform.position;
            snapped = true;
            SetMeshColor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (snappingPoints.Contains(other.tag))
        {
            snapped = false;
            SetMeshColor();
        }
    }
}
