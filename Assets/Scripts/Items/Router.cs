using UnityEngine;

public class Router : MonoBehaviour
{
    private GameObject[] ports;
    private int maxPorts;

    void Start()
    {
        maxPorts = transform.childCount;
        ports = new GameObject[maxPorts];
        for (int i = 0; i < maxPorts; i++)
        {
            ports[i] = transform.GetChild(i).gameObject;
        }
    }


}
