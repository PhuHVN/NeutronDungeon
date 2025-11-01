using Unity.VisualScripting;
using UnityEngine;

public class parallaxScript : MonoBehaviour
{
    private Material mat;
    [SerializeField] private float scrollSpeed = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = new Vector2(Time.time * -scrollSpeed, 0);
        mat.mainTextureOffset = offset;
    }
}
