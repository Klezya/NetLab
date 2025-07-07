using TMPro;
using UnityEngine;

public class WarningText : MonoBehaviour
{
    [SerializeField] private TMP_Text warningTextObject;
    [SerializeField] private Color textMaterial;
    [SerializeField] private Color defaultColor;
    [SerializeField] private float fadeSpeed = 0.6f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        textMaterial = warningTextObject.color; // Get the initial color of the text
        defaultColor = warningTextObject.color;
        textMaterial.a = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (textMaterial.a <= 0) {
            return;
        }
        
        textMaterial.a -= Time.deltaTime * fadeSpeed; // Decrease alpha over time
        warningTextObject.color = textMaterial; // Apply the updated color to the text
    }
    
    public void InitFade()
    {
        textMaterial = defaultColor; // Reset to default color
    }
}
