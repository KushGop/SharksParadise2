using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveMission
{
  //public static readonly string path = Application.persistentDataPath + "/missionData.shp";

  //public static bool HasSave()
  //{
  //  return File.Exists(path);
  //}

  //public static void UpdateMissions()
  //{
  //  Debug.Log("Update Mission");
  //  BinaryFormatter formatter = new();
  //  FileStream stream = new(path, FileMode.Truncate);
  //  stream.Position = 0;
  //  formatter.Serialize(stream, MissionManager.GetMissions());
  //  stream.Close();
  //}

  //public static Mission[] LoadMissions()
  //{
  //  Debug.Log("load");
  //  BinaryFormatter formatter = new();
  //  FileStream stream = new(path, FileMode.Open);
  //  stream.Position = 0;


  //  Mission[] data = formatter.Deserialize(stream) as Mission[];


  //  stream.Close();
  //  return data;
  //}
}
