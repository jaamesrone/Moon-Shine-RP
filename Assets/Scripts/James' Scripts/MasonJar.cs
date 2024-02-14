using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasonJar : MonoBehaviour
{
        public LiquidEffects liquid;
        private Renderer renderer;

        private void Start()
        {
            renderer = GetComponent<Renderer>();
        }

        private void Update()
        {
            renderer.material.SetFloat("Liquid", 10);
        }
    
}
