using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Game : MonoBehaviour
{
    public Transform prefab;
    public KeyCode createKey = KeyCode.C;
    public KeyCode newGameKey = KeyCode.N;
    public KeyCode saveKey = KeyCode.S;
    public KeyCode loadKey = KeyCode.L;
    string savePath;
    List<Transform> objects;

    void Update() {
        if (Input.GetKeyDown(createKey)) {
            CreateObject();
        }
        else if (Input.GetKeyDown(newGameKey)) {
            BeginNewGame();
        }
        else if (Input.GetKeyDown(saveKey)) {
            Save();
        }
        else if (Input.GetKeyDown(loadKey)) {
            Load();
        }
    }

    void Awake() {
        objects = new List<Transform>();
        savePath = Path.Combine(Application.persistentDataPath, "saveFile");
    }

    void Save () {
        using (
                var writer = new BinaryWriter(File.Open(savePath, FileMode.Create))
        ) {
            writer.Write(objects.Count);
            for (int i = 0; i < objects.Count; i++) {
                Transform t = objects[i];
                writer.Write(t.localPosition.x);
                writer.Write(t.localPosition.y);
                writer.Write(t.localPosition.z);
            }
        }
    }

    void Load () {
        BeginNewGame();
        using (
                var reader = new BinaryReader(File.Open(savePath, FileMode.Open))
                ) {
            int count = reader.ReadInt32();
            for (int i = 0; i < objects.Count; i++) {
                Vector3 p;
                p.x = reader.ReadSingle();
                p.y = reader.ReadSingle();
                p.z = reader.ReadSingle();
                Transform t = Instantiate(prefab);
                t.localPosition = p;
                objects.Add(t);
            }
        }
    }

    void CreateObject () {
        Transform t = Instantiate(prefab);
        t.localPosition = Random.insideUnitSphere * 5f;
        t.localRotation = Random.rotation;
        t.localScale = Vector3.one * Random.Range(0.1f, 1f);
        objects.Add(t);
    }

    void BeginNewGame () {
        for (int i = 0; i < objects.Count; i++) {
            Destroy(objects[i].gameObject);
        }
        objects.Clear();
    }
}
