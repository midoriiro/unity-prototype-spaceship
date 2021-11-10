using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Helper;

public class StardustField : MonoBehaviour
{
    public int amount = 1;
    public new Camera camera;
    private CameraCircleBounds bounds;  
    private Rigidbody player;
    private ShipController ship;
    private ParticleSystem.Particle[] points;
    private ParticleSystem system;
    private List<LineRenderer> lines_renderer;
    private Vector3 trail;
    private AssetBundle bundle;
    private Material material;

    void Start()
    {
        bundle = BundleHelper.GetBundle("spaceship.materials");
        //material = BundleHelper.GetMaterial(bundle, "trail");

        camera = Camera.main;
        bounds = camera.transform.GetComponent<CameraCircleBounds>();

        lines_renderer = new List<LineRenderer>();

        CreatePoints();

        system = gameObject.GetComponent<ParticleSystem>();
        //system.GetComponent<Renderer>().sortingLayerName = "Background";

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        ship = GameObject.FindGameObjectWithTag("Player").GetComponent<ShipController>();
    }

    void Update()
    {
        for (int i = 0 ; i < points.Length ; i++)
        {
            Vector3 point = points[i].position;
            LineRenderer renderer = lines_renderer[i];

            if (IsOutOfBounds(point))
            {
                points[i].position = GeneratePosition();
            }

            if (points[i].startColor.a < 1f)
            {
                points[i].startColor = new Color(1, 1, 1, points[i].startColor.a + 0.1f);
            }  
            else if(points[i].startColor.a > 1f)
            {
                points[i].startColor = new Color(1, 1, 1, 1);
            }

            renderer.positionCount = 2;

            trail = player.velocity * Time.smoothDeltaTime;

            renderer.endWidth = player.velocity.normalized.magnitude * renderer.startWidth;
            renderer.startColor = points[i].startColor;
            renderer.endColor = points[i].startColor;

            renderer.SetPosition(0, point);
            renderer.SetPosition(1, point + trail);
        }

        system.SetParticles(points, points.Length);
    }

    bool IsOnScreen(Vector3 vector)
    {
        Vector3 position = camera.WorldToViewportPoint(vector);
        return position.z > 0 && position.x > 0 && position.x < 1 && position.y > 0 && position.y < 1;
    }

    bool IsOutOfBounds(Vector3 vector)
    {
        return Vector3.Distance(vector, camera.transform.position) > bounds.radius;
    }

    void CreatePoints()
    {
        points = new ParticleSystem.Particle[amount];

        for (int i = 0; i < amount; i++)
        {
            points[i].position = Random.insideUnitSphere * bounds.radius;
            points[i].startSize = Random.Range(0.1f, 2.5f);
            points[i].startColor = new Color(1, 1, 1, 0);

            GameObject go = new GameObject();
            go.transform.SetParent(transform);

            LineRenderer renderer = go.AddComponent<LineRenderer>(); 
            renderer.startWidth = points[i].startSize / 4;
            renderer.endWidth = 0f;
            //renderer.material = material;

            lines_renderer.Insert(i, renderer);
        }
    }

    Vector3 GeneratePosition()
    {
        Vector3 center = camera.transform.position;

        Vector3 position = Random.insideUnitSphere * Random.Range(bounds.radius, bounds.radius * 4);

        return position + center;
    }
}
