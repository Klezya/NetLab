using UnityEngine;

public class Item : MonoBehaviour
{
    public string type;
    public bool placed = false;
    public GameObject placedInto = null;
    public Quaternion idealRotation = Quaternion.identity;
    public Vector3 idealRelativePosition = Vector3.zero;


    // Propiedades
    private int maxPorts, currentEnabledPorts;

    [HideInInspector] public bool isHanded;
}
