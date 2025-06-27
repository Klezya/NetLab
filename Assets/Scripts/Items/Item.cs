using UnityEngine;

public class Item : MonoBehaviour
{
    public int id;
    public string type, description;
    public Sprite icon;
    public int cantidad;

    // Propiedades
    private int maxPorts, currentEnabledPorts;

    [HideInInspector] public bool isHanded;
}
