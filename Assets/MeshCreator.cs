using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]

public class MeshCreator : MonoBehaviour{
    public Mesh mesh;
    Vector3[] verts;
    int[] tris;
    void Start(){
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateMesh();
    }

    void Update(){

    }

    void CreateMesh(){
        verts = new Vector3[]{
            new Vector3(0,0,0),
            new Vector3(3,0,0),
            new Vector3(3,3,0),
            new Vector3(0,3,0)
        };

        tris = new int []{
            0,2,1,0,3,2
        };

        Vector2[] uvs = new Vector2[verts.Length];

        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(verts[i].x, verts[i].y);
        }

        mesh.Clear();
        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.uv = uvs;
    }
}