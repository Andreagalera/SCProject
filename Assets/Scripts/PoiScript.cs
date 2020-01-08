using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PoiScript : MonoBehaviour
{
    [SerializeField]
    public double latObject;
    [SerializeField]
    public double lonObject;
    [SerializeField]
    public string textDescription;


    bool located;

    // Busca l'objecte que es diu description
    GameObject description;

    // Start is called before the first frame update
    void Start()
    {
        description = GameObject.Find("Description");
        located = false;
        if (this.gameObject.tag == "poiUser")
        {
            StartCoroutine("UpdateLocation");
        }

    }

    IEnumerator UpdateLocation()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(3);
            MapLocation();
        }
    }
    private void Update()
    {
        if((this.transform.position.x == 0) && (located == false))
        {
            MapLocation();
        }
        else
        {
            located = true;

        }
           
    }
    public void MapLocation()
    {
        int x = MapHandlerScript.centerTileX;
        int y = MapHandlerScript.centerTileY;
        int zoom = MapHandlerScript.zoom;
        double a, b;
        if (this.gameObject.tag== "poiUser")
        {
            // a = DrawCubeX(Input.location.lastData.longitude, TileToWorldPos(x, y, zoom).X, TileToWorldPos(x + 1, y, zoom).X);
            // b = DrawCubeY(Input.location.lastData.latitude, TileToWorldPos(x, y + 1, zoom).Y, TileToWorldPos(x, y, zoom).Y);
            a = DrawCubeX(lonObject, TileToWorldPos(x, y, zoom).X, TileToWorldPos(x + 1, y, zoom).X);
            b = DrawCubeY(latObject, TileToWorldPos(x, y + 1, zoom).Y, TileToWorldPos(x, y, zoom).Y);
        }
        else
        {
            a = DrawCubeX(lonObject, TileToWorldPos(x, y, zoom).X, TileToWorldPos(x + 1, y, zoom).X);
            b = DrawCubeY(latObject, TileToWorldPos(x, y + 1, zoom).Y, TileToWorldPos(x, y, zoom).Y);
        }
        


        Debug.Log(" Pos" + a + "/" + b);
        // Si l'escala és 1 és 0.5, si fos 2 seria 1
        this.transform.position = new Vector3((float)a - 0.5f, (float)b - 0.5f, 0.0f);
    }
    public struct Point
    {
        public double X;
        public double Y;
    }


    // p.X -> longitud
    // p.Y -> latitud
    // left upper corner


    public Point TileToWorldPos(double tile_x, double tile_y, int zoom)
    {
        Point p = new Point();
        double n = System.Math.PI - ((2.0 * System.Math.PI * tile_y) / System.Math.Pow(2.0, zoom));

        p.X = ((tile_x / System.Math.Pow(2.0, zoom) * 360.0) - 180.0);
        p.Y = (180.0 / System.Math.PI * System.Math.Atan(System.Math.Sinh(n)));

        return p;
    }


    public double DrawCubeY(double targetLat, double minLat, double maxLat)
    {
        double pixelY = ((targetLat - minLat) / (maxLat - minLat));
        return pixelY;
    }

    // Si l'escala de l'objecte no fos 1 s'hauria de multiplicar pixelX i pixelY per l'escala
    public double DrawCubeX(double targetLong, double minLong, double maxLong)
    {
        double pixelX = ((targetLong - minLong) / (maxLong - minLong));
        return pixelX;
    }

    // Si cliquem un cub, el quadrat es torna blau i cambia el nom a el nom de l'objecte
    public void SetUnpressedColor()
    {
        if (this.GetComponent<MeshRenderer>().material.color == Color.red)
        {
            this.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else {
            this.GetComponent<MeshRenderer>().material.color = Color.green;
        }
    }
    public void OnMouseDown()
    {
        GameObject[] poiList = GameObject.FindGameObjectsWithTag("poi");
        foreach (GameObject o in poiList)
        {
      
            o.SendMessage("SetUnpressedColor");
        }
        this.GetComponent<MeshRenderer>().material.color = Color.blue;
        description.GetComponent<Text>().text = textDescription;
    }
}
