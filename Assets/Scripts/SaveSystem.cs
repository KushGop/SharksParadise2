using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

  //public static readonly string path = Application.persistentDataPath + "/playerData.shp";

  //public static bool HasSave()
  //{
  //  return File.Exists(path);
  //}

  //public static void SaveData(PlayerData player)
  //{
  //  Debug.Log("save");
  //  //PlayerData load = LoadPlayer();
  //  BinaryFormatter formatter = new();
  //  FileStream stream = new(path, FileMode.Create);
  //  formatter.Serialize(stream, player);
  //  //check highscore
  //  //if (player.score > load.score || load == null)
  //  //{
  //  //  formatter.Serialize(stream, player);
  //  //}
  //  //else
  //  //{
  //  //  formatter.Serialize(stream, new PlayerData(player.coins, load.score));
  //  //}
  //  stream.Close();
  //}

  //public static PlayerData LoadPlayer()
  //{
  //  Debug.Log("load");
  //  if (HasSave())
  //  {
  //    BinaryFormatter formatter = new();
  //    FileStream stream = new(path, FileMode.Open);

  //    PlayerData data = (PlayerData)formatter.Deserialize(stream);
  //    stream.Close();
  //    return data;
  //  }
  //  else
  //  {
  //    return null;
  //  }
  //}
}
