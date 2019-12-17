using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class MapHandlerScript : MonoBehaviour
{
    [SerializeField]
    GameObject Tile22;
    [SerializeField]
    GameObject Tile11;
    [SerializeField]
    GameObject Tile12;
    [SerializeField]
    GameObject Tile13;
    [SerializeField]
    GameObject Tile21;
    [SerializeField]
    GameObject Tile23;
    [SerializeField]
    GameObject Tile31;
    [SerializeField]
    GameObject Tile32;
    [SerializeField]
    GameObject Tile33;

    public static int centerTileX, centerTileY;
    public static int zoom;
    // Start is called before the first frame updat
    public static float lon;
    public static float lat;

  
    void Start()
    {
        
        zoom = 15;
        // GPS 
        WorldToTilePos(2.17706f, 41.39553f, zoom);

        // Faig una llista de tots els objectes que tenen el tag poi, per enviar un missatge de la localització
        GameObject[] poiList = GameObject.FindGameObjectsWithTag("poi");
        
        foreach (GameObject o in poiList)
        {
            o.SendMessage("MapLocation");
        }
        StartCoroutine(LoadTile(centerTileX, centerTileY, Tile22));
        
    }


    public void DownLoadCenterMapTileGps()
    {
        // One or more tiles
        Debug.Log("Mensaje");
        WorldToTilePos(Input.location.lastData.longitude, Input.location.lastData.latitude, zoom);
        LoadTile(centerTileX, centerTileY, Tile22);
        //Debug.Log("Mensaje");
    }

    public void WorldToTilePos(float lon, float lat, int zoom)
    {
        double tileX, tileY;
        tileX = (double)((lon + 180.0f) / 360.0f * (1 << zoom));
        tileY = (double)((1.0f - Mathf.Log(Mathf.Tan((float)lat * Mathf.PI / 180.0f) + 1.0f / Mathf.Cos((float)lat * Mathf.PI / 180.0f)) / Mathf.PI) / 2.0f * (1 << zoom));
        centerTileX = Mathf.FloorToInt((float)tileX);
        centerTileY = Mathf.FloorToInt((float)tileY);
        Debug.Log("X:" + tileX + "Y" + tileY);
    }

    // Función si tu pares en un punt, després tornes en aquest punt
    IEnumerator LoadTile(int x, int y, GameObject quadTile)
    {
        Debug.Log("loadTileX" + x + "-" + y);
        int originalX = x;
        int originalY = y;
        string uri = "https://a.tile.openstreetmap.org/" + zoom + "/" + x + "/" + y + ".png";

        //CustomCertificateHandler certHandler = new CustomCertificateHandler();
        UnityWebRequest www = UnityWebRequestTexture.GetTexture("https://a.tile.openstreetmap.org/" + zoom + "/" + x + "/" + y + ".png");
        //www.certificateHandler = certHandler;

        // Enviar petició i esperem resposta
        yield return www.SendWebRequest();

        Debug.Log("server");
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Received");
            Tile22.GetComponent<MeshRenderer>().material.mainTexture = DownloadHandlerTexture.GetContent(www);
        }

        x = originalX + 1;
        www = UnityWebRequestTexture.GetTexture("https://a.tile.openstreetmap.org/" + zoom + "/" + x + "/" + y + ".png");
        //www.certificateHandler = certHandler;

        // Enviar petició i esperem resposta
        yield return www.SendWebRequest();

        Debug.Log("server");
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Received");
            Tile23.GetComponent<MeshRenderer>().material.mainTexture = DownloadHandlerTexture.GetContent(www);
        }

        x = originalX - 1;
        www = UnityWebRequestTexture.GetTexture("https://a.tile.openstreetmap.org/" + zoom + "/" + x + "/" + y + ".png");
        //www.certificateHandler = certHandler;

        // Enviar petició i esperem resposta
        yield return www.SendWebRequest();

        Debug.Log("server");
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Received");
            Tile21.GetComponent<MeshRenderer>().material.mainTexture = DownloadHandlerTexture.GetContent(www);
        }

        y = originalY - 1;
        x = originalX - 1;
        www = UnityWebRequestTexture.GetTexture("https://a.tile.openstreetmap.org/" + zoom + "/" + x + "/" + y + ".png");
        //www.certificateHandler = certHandler;

        // Enviar petició i esperem resposta
        yield return www.SendWebRequest();

        Debug.Log("server");
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Received");
            Tile11.GetComponent<MeshRenderer>().material.mainTexture = DownloadHandlerTexture.GetContent(www);
        }

        y = originalY - 1;
        www = UnityWebRequestTexture.GetTexture("https://a.tile.openstreetmap.org/" + zoom + "/" + originalX + "/" + y + ".png");
        //www.certificateHandler = certHandler;

        // Enviar petició i esperem resposta
        yield return www.SendWebRequest();

        Debug.Log("server");
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Received");
            Tile12.GetComponent<MeshRenderer>().material.mainTexture = DownloadHandlerTexture.GetContent(www);
        }
        www = UnityWebRequestTexture.GetTexture("https://a.tile.openstreetmap.org/" + zoom + "/" + (originalX + 1) + "/" + (originalY - 1) + ".png");
        //www.certificateHandler = certHandler;

        // Enviar petició i esperem resposta
        yield return www.SendWebRequest();

        Debug.Log("server");
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Received");
            Tile13.GetComponent<MeshRenderer>().material.mainTexture = DownloadHandlerTexture.GetContent(www);
        }
        y = originalY + 1;
        x = originalX + 1;
        www = UnityWebRequestTexture.GetTexture("https://a.tile.openstreetmap.org/" + zoom + "/" + x + "/" + y + ".png");
        //www.certificateHandler = certHandler;

        // Enviar petició i esperem resposta
        yield return www.SendWebRequest();

        Debug.Log("server");
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Received");
            Tile33.GetComponent<MeshRenderer>().material.mainTexture = DownloadHandlerTexture.GetContent(www);
        }
        www = UnityWebRequestTexture.GetTexture("https://a.tile.openstreetmap.org/" + zoom + "/" + originalX + "/" + (originalY + 1) + ".png");
        //www.certificateHandler = certHandler;

        // Enviar petició i esperem resposta
        yield return www.SendWebRequest();

        Debug.Log("server");
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Received");
            Tile32.GetComponent<MeshRenderer>().material.mainTexture = DownloadHandlerTexture.GetContent(www);
        }
        x = originalX - 1;
        y = originalY + 1;
        www = UnityWebRequestTexture.GetTexture("https://a.tile.openstreetmap.org/" + zoom + "/" + x + "/" + y + ".png");
        //www.certificateHandler = certHandler;

        // Enviar petició i esperem resposta
        yield return www.SendWebRequest();

        Debug.Log("server");
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Received");
            Tile31.GetComponent<MeshRenderer>().material.mainTexture = DownloadHandlerTexture.GetContent(www);
        }
    }


    // Funció zoom in and zoom out
    public void ZoomIn() {
        zoom++;
        if (zoom > 18)
            zoom = 18;
        WorldToTilePos(2.17706f, 41.39553f, zoom);
        Debug.Log("ZOOM: " + zoom);

        // Faig una llista de tots els objectes que tenen el tag poi, per enviar un missatge de la localització
        GameObject[] poiList = GameObject.FindGameObjectsWithTag("poi");

        foreach (GameObject o in poiList)
        {
            o.SendMessage("MapLocation");
        }
        StartCoroutine(LoadTile(centerTileX, centerTileY, Tile22));
    }

    public void ZoomOut()
    {
        zoom--;
        if (zoom < 10)
            zoom = 10;
        WorldToTilePos(2.17706f, 41.39553f, zoom);
        Debug.Log("ZOOM: " + zoom);

        // Faig una llista de tots els objectes que tenen el tag poi, per enviar un missatge de la localització
        GameObject[] poiList = GameObject.FindGameObjectsWithTag("poi");

        foreach (GameObject o in poiList)
        {
            o.SendMessage("MapLocation");
        }
        StartCoroutine(LoadTile(centerTileX, centerTileY, Tile22));
    }
}
