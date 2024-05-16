using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

[CreateAssetMenu(
    fileName = "Player Progress", 
    menuName = "Game Kuis/Player Progress")]
public class PlayerProgress : ScriptableObject 
{
    [System.Serializable]
    public struct MainData
    {
        public int koin;
        public Dictionary<string,int> progresLevel;
    }

    [SerializeField]
    private string _filename = "contoh.txt";

    public MainData progresData = new MainData();

    public void SimpanProgres()
    {
        progresData.koin = 200;
        if (progresData.progresLevel == null)
            progresData.progresLevel = new();
        progresData.progresLevel.Add("Level Pack 1", 3);
        progresData.progresLevel.Add("Level Pack 3", 5);

        var filename = "contoh.txt";
        var directory = Application.dataPath + "/Temporary";
        var path = directory + "/" + filename;

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            Debug.Log("Directory has been Created: " + directory);
        }

        if (File.Exists(path))
        {
            File.Create(path).Dispose();
            Debug.Log("File created: " + path);
        }

        var fileStream = File.Open(path, FileMode.Open);
        //var formatter = new binaryformatter();

        fileStream.Flush();
        //formatter.serialize(filestream, progresdata);

        var writer = new BinaryWriter(fileStream);
        //var konten = $"{progresData.koin}\n";

        writer.Write(progresData.koin);
        foreach(var i in progresData.progresLevel)
        {
            writer.Write(i.Key);
            writer.Write(i.Value);
        }

         writer.Dispose();

        //foreach( var i  in progresData.progresLevel)
        //{
        //    konten += $"{i.Key} {i.Value}\n";
        //}
        // File.WriteAllText(path, konten);

        fileStream.Dispose();

        Debug.Log($"{filename} Berhasil Disimpan");
    }

    public bool MuatProgres()
    {
        var directory = Application.dataPath + "/Temporary";
        var path = directory + "/" + _filename;

        var fileStream = File.Open(path, FileMode.OpenOrCreate);

        try
        {

            var reader = new BinaryReader(fileStream);
            try
            {
                progresData.koin = reader.ReadInt32();
                if (progresData.progresLevel == null)
                    progresData.progresLevel = new();
                while (reader.PeekChar() != -1)
                {
                    var namaLevelPack = reader.ReadString();
                    var levelKe = reader.ReadInt32();
                    progresData.progresLevel.Add(namaLevelPack, levelKe);
                    Debug.Log($"{namaLevelPack}:{levelKe}");
                }

                reader.Dispose();
            }
            catch(System.Exception e)
            {
                Debug.Log($"ERROR : Kesalahan muat progres\n{e.Message}");
                reader.Dispose();
                fileStream.Dispose();
                return false;
            }
            //var formatter = new BinaryFormatter();

            //progresData = (MainData)formatter.Deserialize(fileStream);

            fileStream.Dispose();

            Debug.Log($"{progresData.koin}; {progresData.progresLevel.Count}");
            return true;
        }
        catch (System.Exception e)
        {
            
            Debug.Log($"ERROR : Kesalahan muat progres\n{e.Message}");
            fileStream.Dispose();
            return false;
        }

    }
}