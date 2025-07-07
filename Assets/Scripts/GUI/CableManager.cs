using UnityEngine;

public class CableManager : MonoBehaviour
{
    [SerializeField] private GameObject cableHeadPrefab;

    [SerializeField] private GameObject hand;
    [SerializeField] private GameObject inspectorMode;

    private GameObject cableTemp;


    public void Use()
    {
        if (inspectorMode.GetComponent<InspectorMode>().isConnecting)
        {
            Debug.Log("You are already connecting a cable");
            return;
        }
        if (!inspectorMode.GetComponent<InspectorMode>().isEditingPorts)
        {
            inspectorMode.GetComponent<InspectorMode>().isEditingPorts = true;
            cableTemp = Instantiate(cableHeadPrefab, hand.transform.position, hand.transform.rotation);
            hand.GetComponent<PlayerGrab>().grabbedObject = cableTemp;
            cableTemp.SetActive(false);

            // Set isEditingPorts to true
            inspectorMode.GetComponent<InspectorMode>().isEditingPorts = true;
        }
        else
        {
            inspectorMode.GetComponent<InspectorMode>().isEditingPorts = false;
            hand.GetComponent<PlayerGrab>().grabbedObject = null;
            Destroy(cableTemp);
            cableTemp = null;

            // Reset isEditingPorts to false
            inspectorMode.GetComponent<InspectorMode>().isEditingPorts = false;
        }
    }

}
