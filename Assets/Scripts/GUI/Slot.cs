using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{


    [SerializeField] private GameObject item;
    [SerializeField] private int id;
    [SerializeField] private string type;
    [SerializeField] private Transform spawnPoint;



    void Awake()
    {

    }



    public void OnClick()
    {
        Debug.Log("Slot clicked: " + item);
        GameObject newItem = Instantiate(item, spawnPoint.position, spawnPoint.rotation);
    }
}
