using UnityEngine;
using System.Linq;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
public class ReverseNormals : MonoBehaviour
{

    void Start()
    {

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.triangles = mesh.triangles.Reverse().ToArray();
    }
}
