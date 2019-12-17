using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;

public class Bicing : MonoBehaviour
{
    public GameObject Poi;
    GameObject description;

    // Start is called before the first frame update
    void Start()
    {
        // Empezamos prerutina
        StartCoroutine("Bici");
        description = GameObject.Find("Description");

    }


    IEnumerator Bici()
    {
        // Ens conectem al servidor d'OpenData
        WWW www = new WWW("https://api.bsmsa.eu/ext/api/bsm/gbfs/v2/en/station_status");
        yield return www;
        WWW wwwInfo = new WWW("https://api.bsmsa.eu/ext/api/bsm/gbfs/v2/en/station_information");
        yield return wwwInfo;

        // Crear json object
        JObject obj = JObject.Parse(www.text);
        JObject objInfo = JObject.Parse(wwwInfo.text);
        Debug.Log("Object" + obj);
        Debug.Log("ObjectInfo" + objInfo);
        // Crear array "chargePoints" que conte cada chargepoint
        JArray stations = (JArray)obj["data"]["stations"];
        JArray stationsInfo = (JArray)objInfo["data"]["stations"];
        Debug.Log("Jarray" + stations);
        Debug.Log("JarrayInfo" + stationsInfo);
        // Miro el numero
        Debug.Log("Number stations: " + stations.Count);
        Debug.Log("Number stationsInfo: " + stationsInfo.Count);
        JObject stationPoint2 = (JObject)stationsInfo.GetItem(0);
        Debug.Log("MAPA" + stationPoint2["lat"].ToString());
        MapHandlerScript.lat = (float)stationPoint2["lat"];
        MapHandlerScript.lon = (float)stationPoint2["lon"];
        for (var i = 0; i < stationsInfo.Count; i++)
        {
            JObject stationPoint = (JObject)stationsInfo.GetItem(i);
            float lat = (float)stationPoint["lat"];
            float lon = (float)stationPoint["lon"];


            int bikesAvailable = (int)stations[i]["num_bikes_available"];
            Debug.Log("Charge Bicing info lat: " + lat.ToString() + ", lon:" + lon.ToString() + ", bikes:" + bikesAvailable.ToString());


            // Creo un objecte "o" i li aplico longitud i latitud
            GameObject o = Instantiate(Poi);
            if (bikesAvailable == 0)
            {
                o.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
            }
            else {
                o.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
            }
            o.GetComponent<PoiScript>().latObject = lat;
            o.GetComponent<PoiScript>().lonObject = lon;
            o.GetComponent<PoiScript>().textDescription = "Available: " + bikesAvailable.ToString();
            //description.GetComponent<Text>().text = bikesAvailable.ToString();

        }


    }
}
