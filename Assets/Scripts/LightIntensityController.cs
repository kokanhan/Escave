using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIntensityController : MonoBehaviour
{
    public GameObject lightGlass;
    public GameObject light;

    private float initGlassIntensity;
    private float initLightIntensity;

    private float time;

    // Start is called before the first frame update
    void Start()
    {
      initGlassIntensity = lightGlass.GetComponent<Renderer>().material.GetFloat("_strength");
      initLightIntensity = light.GetComponent<Light>().intensity;
    }

    // Update is called once per frame
    void Update()
    {
      lightGlass.GetComponent<Renderer>().material.SetFloat("_strength", initGlassIntensity + initGlassIntensity * Mathf.Sin(time * 0.8f) * 0.3f);
      light.GetComponent<Light>().intensity = initLightIntensity + initLightIntensity * Mathf.Sin(time * 0.8f) * 0.3f;

      time += Time.deltaTime;
    }
}
