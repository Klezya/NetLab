using UnityEngine;

public class CableRJ45 : MonoBehaviour
{
    [SerializeField] public GameObject cableParent;
    public GameObject port;

    public void DestroyCable()
    {
        port.GetComponent<Port>().isBusy = false;
        port.GetComponent<Port>().conector = null;
        Destroy(gameObject);   
    }
}
