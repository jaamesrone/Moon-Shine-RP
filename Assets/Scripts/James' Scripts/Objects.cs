using System.Collections;
using UnityEngine;

public class Objects : MonoBehaviour
{
    public LiquidEffects liquid;
    public Material jarMaterial;

    void Start()
    {
        // Set the liquid level to 0 at the start
        liquid.liquidLevel = 0;
        SetLiquidLevel(liquid.liquidLevel);

        // Set the jar colors
        SetJarColors(liquid.Side, liquid.Top);

        /*// Start the IncreaseLiquidLevel coroutine
        StartCoroutine(IncreaseLiquidLevel());*/
    }

    IEnumerator IncreaseLiquidLevel()
    {
        // While the liquid level is less than 1
        while (liquid.liquidLevel < 1)
        {
            // Increase the liquid level by 0.1
            liquid.liquidLevel += 0.1f;

            // Update the liquid level in the shader
            SetLiquidLevel(liquid.liquidLevel);

            // Wait for 1 second before the next increase
            yield return new WaitForSeconds(0.3f);
        }
    }

    void SetLiquidLevel(float level)
    {
        // Set liquid level using shader properties
        jarMaterial.SetFloat("_Liquid", level);
    }

    void SetJarColors(Color sideColor, Color topColor)
    {
        // Set side and top colors using shader properties
        jarMaterial.SetColor("_Side", sideColor);
        jarMaterial.SetColor("_Top", topColor);
    }
}
