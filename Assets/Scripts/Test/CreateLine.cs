using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreateLine : MonoBehaviour
{

    public Transform leftCorner;
    public Transform rightCorner;
    public GameObject tape;

    public float frequency;

    public GameObject testMesh;

    Tween t;

    List<Vector3> vertices;
    List<Vector2> UVs;
    List<int> triangles;

    Mesh mesh;

    bool shouldPlay = true;


    // Start is called before the first frame update
    void Start()
    {

        shouldPlay = false;

        vertices = new List<Vector3>();
        triangles = new List<int>();


        t = tape.GetComponent<DOTweenPath>().GetTween();
        t.Pause();
        mesh = GetComponent<MeshFilter>().mesh;
        //print(transform.position);
        StartCoroutine(DrawLine());
    }

    private void Update()
    {

    }

    public void ShouldPlay()
    {
        print("should play now");
        shouldPlay = true;
        t.Play();
    }

    public void ShouldPause()
    {
        print("should pause now");
        shouldPlay = false;
        t.Pause();
    }

    public float getAnimProgress()
    {
        return t.ElapsedPercentage();
    }

    IEnumerator DrawLine()
    {
        while (true)
        {
            if (shouldPlay)
            {
                vertices.Add(leftCorner.position);
                vertices.Add(rightCorner.position);

                int quad = 1;

                if (vertices.Count > 3)
                {
                    // Строим меш
                    mesh.Clear();
                    int total = vertices.Count();

                    for (int i = 0; i < quad; i++)
                    {
                        //1st tri
                        int t1 = total - 4 + i * 2;
                        int t2 = total - 3 + i * 2;
                        int t3 = total - 2 + i * 2;

                        triangles.Add(t1);
                        triangles.Add(t2);
                        triangles.Add(t3);

                        //2nd tri
                        t1 = total - 2 + i * 2;
                        t2 = total - 3 + i * 2;
                        t3 = total - 1 + i * 2;

                        triangles.Add(t1);
                        triangles.Add(t2);
                        triangles.Add(t3);
                    }

                    mesh.vertices = vertices.ToArray();
                    mesh.triangles = triangles.ToArray();



                    quad += 1;

                }
            }
            yield return new WaitForSeconds(frequency);
            //yield return null;
        }


    }


}
