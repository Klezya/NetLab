using UnityEngine;

public class Router : MonoBehaviour
{

    public int id;
    public string type, description;
    public Sprite icon;

    // Propiedades
    private int maxPorts, currentEnabledPorts;

    [HideInInspector] public bool isHanded;



}
