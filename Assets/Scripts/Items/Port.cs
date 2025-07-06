using UnityEngine;

public class Port : MonoBehaviour
{
    [SerializeField] public Material portTransparentMaterial;
    [SerializeField] public Material portHighlightMaterial;
    [SerializeField] public Material portDeleteMaterial;

    private GameObject childHighlight;
    public bool isBusy = false;

    // Conector RJ45
    public GameObject rj45Connector;

    

}
