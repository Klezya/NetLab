using UnityEngine;

public class CableHolder : MonoBehaviour
{
    [SerializeField] private GameObject cableStart;
    [SerializeField] private GameObject cableEnd;
    [SerializeField] private GameObject cableBody;

    public void DestroyCable()
    {
        cableStart.GetComponent<CableRJ45>().DestroyCable();
        cableEnd.GetComponent<CableRJ45>().DestroyCable();
        Destroy(cableBody);
    }
}
