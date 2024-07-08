using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class generation1 : MonoBehaviour
{
    public Transform cube;
    public int resolution;

  
    [SerializeField, Range(0, 224)]
    public int picker;
    public float[] block_pos_x;
    public float[] block_pos_y;
    public float[] block_pos_z;
   // public Transform player;
    public Vector3 position = Vector3.zero;
    public Transform point;

    public float czytnik;
 
   //public Color white = Color.white;
    private void Start()
    {
      block_pos_x =new float [resolution * resolution];
        block_pos_z=new float[resolution * resolution];
      
     
        int i = 0;
        for (int x=0;x<resolution; x++)
        {
            for(int z=0;z<resolution; z++) 
            {



             point = Instantiate(cube);
            //    cube.AddComponent<MeshRenderer>().material.color = redcolor;
                //point.localPosition = Vector3.right * ((i + 0.5f) / 5f - 1f);
                position.x = 0.5f*48*x;
                position.z =0.5f* 48 * z ;
              
                point.position = position;
                point.SetParent(transform, false);
                block_pos_x[i] = point.position.x+28;
                block_pos_z[i]  = point.position.z+21;
        
                i++;
              //  Debug.Log(point.position);


            }
        }
      
    }
    private void Update()
    {
      
        //distance = Mathf.Sqrt(Mathf.Pow(player.position.x - block_pos_x[picker], 2) + Mathf.Pow(player.position.z - block_pos_z[picker], 2) );
    }
}
