using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;

public class Bicing : MonoBehaviour
{
    public GameObject Poi;
    GameObject description;
    GameObject nameStationNear;
    GameObject GPSPointName;
    JObject nearStation = null;
    JObject GPSPoint = null;


    // Start is called before the first frame update
    void Start()
    {
        // Empezamos prerutina
        StartCoroutine("Bici");
        description = GameObject.Find("Description");
        nameStationNear = GameObject.Find("nameStationNear");
        GPSPointName = GameObject.Find("GPSPointName");

    }

    public void refreshInfo()
    {
        StartCoroutine("Bici");
        description = GameObject.Find("Description");
        nameStationNear = GameObject.Find("nameStationNear");
        GPSPointName = GameObject.Find("GPSPoint");
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
        // Miro el numero
        JObject stationPoint2 = (JObject)stationsInfo.GetItem(0);
        MapHandlerScript.lat = (float)stationPoint2["lat"];
        MapHandlerScript.lon = (float)stationPoint2["lon"];
        double diffLat = 100;
        double diffLon = 100;
        for (var i = 0; i < stationsInfo.Count; i++)
        {
            JObject stationPoint = (JObject)stationsInfo.GetItem(i);
            float lat = (float)stationPoint["lat"];
            float lon = (float)stationPoint["lon"];
            nearStation = stationPoint;

            int bikesAvailable = (int)stations[i]["num_bikes_available"];
            Debug.Log("Charge Bicing info lat: " + lat.ToString() + ", lon:" + lon.ToString() + ", bikes:" + bikesAvailable.ToString());

            if(diffLat > System.Math.Abs(Input.location.lastData.latitude - lat) && diffLon > System.Math.Abs(Input.location.lastData.longitude - lon))
            {
                diffLat = System.Math.Abs(Input.location.lastData.latitude - lat);
                diffLon = System.Math.Abs(Input.location.lastData.longitude - lon);
                
            }
            

            /*if (diffLat > System.Math.Abs(41.379152 - lat) && diffLon > System.Math.Abs(2.154581 - lon))
            {
                diffLat = System.Math.Abs(41.379152 - lat);
                diffLon = System.Math.Abs(2.154581 - lon);
                nearStation = stationPoint;
            }*/

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
            o.GetComponent<PoiScript>().GPSPointName = "GPS:" + Input.location.lastData.latitude + "," + Input.location.lastData.longitude;
        }
        Debug.Log("Near station, lat: " + diffLat.ToString() + ", lon:" + diffLon.ToString() + "station name:" + (string)nearStation["name"]);

    }

    public void NearMe()
    {
        GameObject o = Instantiate(Poi);
        o.GetComponent<PoiScript>().nameStationNear = "Station street: " + (string)nearStation["name"];
    }
}
