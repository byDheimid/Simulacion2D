using UnityEngine;

public class SmogGPU : MonoBehaviour
{
    public int particleCount = 1000;
    public ComputeShader compute;
    public Mesh mesh;
    public Material material;
    public float speed = 1f;

    ComputeBuffer particleBuffer;
    ComputeBuffer positionBuffer;

    int kernel;

    void Start()
    {
        kernel = compute.FindKernel("CSMain");

        FireParticle[] particles = new FireParticle[particleCount];

        for (int i = 0; i < particleCount; i++)
        {
            particles[i] = new FireParticle
            {
                startPos = new Vector2(
    Random.Range(-1f, 1f) * transform.localScale.x,
    Random.Range(0f, 1f)  * transform.localScale.y
),

                amplitudeX = Random.Range(0.15f, 0.35f),
                amplitudeY = Random.Range(0.08f, 0.15f),

                frequencyX = Random.Range(1.5f, 2.5f),
                frequencyY = Random.Range(2.5f, 3.5f),

                offsetX = Random.Range(0f, Mathf.PI * 2f),
                offsetY = Random.Range(0f, Mathf.PI * 2f)
            };
        }

        particleBuffer = new ComputeBuffer(particleCount, sizeof(float) * 8);
        particleBuffer.SetData(particles);

        positionBuffer = new ComputeBuffer(particleCount, sizeof(float) * 4);

        compute.SetBuffer(kernel, "particles", particleBuffer);
        compute.SetBuffer(kernel, "positions", positionBuffer);
        compute.SetFloats("_ParentScale", transform.localScale.x, transform.localScale.y);

        material.SetBuffer("positions", positionBuffer);
    }

    void Update()
    {
        compute.SetFloat("_Time", Time.time);
        compute.SetFloat("_Speed", speed);

        compute.Dispatch(kernel, particleCount / 256 + 1, 1, 1);

        Graphics.DrawMeshInstancedProcedural(
            mesh, 0, material,
            new Bounds(transform.position, Vector3.one * 50f),
            particleCount
        );
    }

    void OnDestroy()
    {
        particleBuffer?.Release();
        positionBuffer?.Release();
    }
}

struct FireParticle
{
    public Vector2 startPos;
    public float amplitudeX;
    public float amplitudeY;
    public float frequencyX;
    public float frequencyY;
    public float offsetX;
    public float offsetY;
}
