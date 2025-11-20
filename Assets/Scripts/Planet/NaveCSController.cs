using UnityEngine;

public class NaveCSController : MonoBehaviour
{
    [System.Serializable]
    public struct ShipData
    {
        public Vector2 position;
        public Vector2 velocity;
        public float mass;
    }

    [System.Serializable]
    public struct PlanetData
    {
        public Vector2 position;
        public float mass;
        public float detectionRadius;
    }

    [Header("Compute Shaders")]
    public ComputeShader shipGravityCS;

    [Header("Simulation Data")]
    public ShipData[] shipsArray;
    public PlanetData[] planetsArray;

    public GameObject[] shipGameObjects;

    ComputeBuffer shipBuffer;
    ComputeBuffer planetBuffer;

    int shipCount;
    int planetCount;

    void Start()
    {
        // Detectar todas las naves en la escena
        shipGameObjects = GameObject.FindGameObjectsWithTag("Nave");

        // Crear ShipData[] del mismo tamaño
        shipsArray = new ShipData[shipGameObjects.Length];

        for (int i = 0; i < shipGameObjects.Length; i++)
        {
            Vector3 pos = shipGameObjects[i].transform.position;

            shipsArray[i] = new ShipData
            {
                position = new Vector2(pos.x, pos.y),
                velocity = Vector2.zero,
                mass = 1f
            };
        }

        shipCount = shipsArray.Length;

        // Lo mismo para planetas con tag "Planeta"
        var planetObjects = GameObject.FindGameObjectsWithTag("Planeta");
        planetsArray = new PlanetData[planetObjects.Length];

        for (int i = 0; i < planetObjects.Length; i++)
        {
            Vector3 pos = planetObjects[i].transform.position;

            planetsArray[i] = new PlanetData
            {
                position = new Vector2(pos.x, pos.y),
                mass = 10f,
                detectionRadius = 5f
            };
        }

        planetCount = planetsArray.Length;

        // Ahora que tienes todo → crear buffers
        shipBuffer = new ComputeBuffer(shipCount, sizeof(float) * 5);
        planetBuffer = new ComputeBuffer(planetCount, sizeof(float) * 4);

        shipBuffer.SetData(shipsArray);
        planetBuffer.SetData(planetsArray);
    }

    void Update()
    {
        int kernel = shipGravityCS.FindKernel("CSMain");

        shipGravityCS.SetBuffer(kernel, "_Ships", shipBuffer);
        shipGravityCS.SetBuffer(kernel, "_Planets", planetBuffer);

        shipGravityCS.SetFloat("_DeltaTime", Time.deltaTime);
        shipGravityCS.SetInt("_ShipCount", shipCount);
        shipGravityCS.SetInt("_PlanetCount", planetCount);
        shipGravityCS.SetFloat("_GravityConstant", 1.0f);

        shipGravityCS.Dispatch(kernel, Mathf.CeilToInt(shipCount / 64f), 1, 1);

        // Recuperar posiciones de las naves
        shipBuffer.GetData(shipsArray);

        // Actualizar sus Transform
        for (int i = 0; i < shipCount; i++)
        {
            // OJO: aquí necesitas referencias a cada GameObject nave
            shipGameObjects[i].transform.position = shipsArray[i].position;
        }
    }

    void OnDestroy()
    {
        shipBuffer?.Release();
        planetBuffer?.Release();
    }
}
