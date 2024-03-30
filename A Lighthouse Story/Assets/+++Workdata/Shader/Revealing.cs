using UnityEngine;

[ExecuteInEditMode]
public class Revealing : MonoBehaviour
{
    [SerializeField] private Material mat;

    [SerializeField] private Light spotLight;

    private void Update()
    {
        if (mat && spotLight)
        {
            mat.SetVector("MyLightPosition",  spotLight.transform.position);
            mat.SetVector("MyLightDirection", -spotLight.transform.forward);
            mat.SetFloat ("MyLightAngle", spotLight.spotAngle);
        }
    }
}
