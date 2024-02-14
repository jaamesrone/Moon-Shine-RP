using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour
{
    public LiquidEffects liquid;
    public Material jarMaterial;

    void Start()
    {
        // Example of using the MasonJarSettings
        SetLiquidLevel(liquid.liquidLevel);
        SetJarColors(liquid.Side, liquid.Top);
    }

    public void SetLiquidLevel(float level)
    {

        // Set liquid level using shader properties
        jarMaterial.SetFloat("_Liquid", level);
    }

    public void IncreaseLiquidLevel()
    {
        // Increase liquid level by 0.1 each time the button is clicked
        liquid.liquidLevel += 0.1f;

        // Clamp the value between 0 and 1
        liquid.liquidLevel = Mathf.Clamp(liquid.liquidLevel, 0, 1);

        // Update the liquid level in the shader
        SetLiquidLevel(liquid.liquidLevel);
    }

    public void SetJarColors(Color sideColor, Color topColor)
    {

        // Set side and top colors using shader properties
        jarMaterial.SetColor("_Side", sideColor);
        jarMaterial.SetColor("_Top", topColor);
    }
}

