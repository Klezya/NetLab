using UnityEngine;

public class DeviceManager : MonoBehaviour
{
    
    [SerializeField] private GameObject hand;
    [SerializeField] private GameObject handPoint;
    private GameObject grabbedObject;
    private GameObject[] devices;
    private int totalDevices;

    void Awake()
    {
        totalDevices = transform.childCount;
        devices = new GameObject[totalDevices];
        for (int i = 0; i < totalDevices; i++)
        {
            devices[i] = transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        grabbedObject = hand.GetComponent<PlayerGrab>().grabbedObject;
        if (grabbedObject == null) {
            handPoint.SetActive(true);
            for (int i = 0; i < totalDevices; i++)
            {
                devices[i].SetActive(false);
            }
            return;
        }

        for (int i = 0; i < totalDevices; i++)
        {
            if (grabbedObject.GetComponent<Item>().type == devices[i].GetComponent<ItemOnlyView>().type)
            {
                devices[i].SetActive(true);
                handPoint.SetActive(false);
                return;
            }
        }
    }
}
