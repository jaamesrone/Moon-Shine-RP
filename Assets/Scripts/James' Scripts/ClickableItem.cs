using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClickableItem : MonoBehaviour
{
    public LiquidEffect liquidEffect;
    public Material material;
    private bool isSelected = false;

    private void Start()
    {
        material.SetFloat("_Liquid", 0f);
    }
    void Update()
    {
        if (isSelected && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                RawImage image = hit.transform.GetComponent<RawImage>();
                if (image != null)
                {
                    liquidEffect.Top = image.color;
                    liquidEffect.Side = image.color; 
                    StartCoroutine(IncreaseLiquid());

                    // Set color properties in your material
                    material.SetColor("_Top", liquidEffect.Top);
                    material.SetColor("_Side", liquidEffect.Side);

                    isSelected = false; // Reset selection state
                }
            }
        }
    }

    IEnumerator IncreaseLiquid()
    {
        float currentValue = material.GetFloat("_Liquid");
        while (currentValue < 1f)
        {
            currentValue += 0.1f;
            material.SetFloat("_Liquid", currentValue);

            yield return new WaitForSeconds(0.8f); // Adjust the delay as needed
        }

        // Ensure the final value is exactly 1
        material.SetFloat("_Liquid", 1f);

        isSelected = false; // Reset selection state
    }
    void OnMouseDown()
    {
        isSelected = true;
    }
}
