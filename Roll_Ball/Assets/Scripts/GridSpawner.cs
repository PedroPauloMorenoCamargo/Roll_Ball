using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public int gridSize = 12;
    public float cubeSpacing = 2f;
    public float cubeSize = 2f;

    public Material cubeMaterial;

    public string cubeTag = "Arena";

    void Start(){
        CreateGrid();
    }

    void CreateGrid(){
        //Caçula o offset da Grid
        float gridOffset = (gridSize - 1) * cubeSpacing / 2f;

        for (int x = 0; x < gridSize; x++){
            for (int y = 0; y < gridSize; y++){

                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

                cube.transform.parent = this.transform;

                // Centraliza o (0,0,0) no centro da grid
                float posX = x * cubeSpacing - gridOffset;
                float posZ = (y * cubeSpacing - gridOffset) + this.transform.position.z;


                cube.transform.position = new Vector3(posX, 0.1f, posZ);

                cube.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

                Renderer cubeRenderer = cube.GetComponent<Renderer>();
                if (cubeMaterial != null)
                {
                    cubeRenderer.material = cubeMaterial;
                }
                cube.tag = cubeTag;
            }
        }
    }
}
