using UnityEngine;

public class Port : MonoBehaviour
{
    [SerializeField] public Material portTransparentMaterial;
    [SerializeField] public Material portHighlightMaterial;
    [SerializeField] public Material portDeleteMaterial;

    public bool isBusy = false;

    // Conector RJ45
    public GameObject conector;

    void Update()
    {
        

    }
}
