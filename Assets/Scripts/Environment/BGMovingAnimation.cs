using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float parallaxSpeed = 0.1f; // Geschwindigkeit des Parallax-Effekts
    private Renderer rend;
    private Vector2 offset;

    void Start()
    {
        rend = GetComponent<Renderer>(); // Called den Renderer der Plane
        offset = rend.material.mainTextureOffset;
    }

    void Update()
    {
        // Verschiebt die Textur basierend auf der Zeit und Geschwindigkeit
        offset.x += parallaxSpeed * Time.deltaTime;
        rend.material.mainTextureOffset = offset;
    }
}
