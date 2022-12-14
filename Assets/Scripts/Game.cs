using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Game : PersistableObject
{
    public PersistableObject prefab;
    List<PersistableObject> objects;
    public PersistentStorage storage;

    public KeyCode createKey = KeyCode.C;
    public KeyCode newGameKey = KeyCode.N;
    public KeyCode saveKey = KeyCode.S;
    public KeyCode loadKey = KeyCode.L;

    public override void Save (GameDataWriter writer) {
        writer.Write(objects.Count);
        for (int i = 0; i < objects.Count; i++) {
            objects[i].Save(writer);
        }
    }

    public override void Load (GameDataReader reader) {
        int count = reader.readInt();
        for (int i = 0; i < count; i++) {
            PersistableObject o = Instantiate(prefab);
            o.Load(reader);
            objects.Add(o);
        }
    }

    void Update() {
        if (Input.GetKeyDown(createKey)) {
            CreateObject();
        }
        else if (Input.GetKeyDown(newGameKey)) {
            BeginNewGame();
        }
        else if (Input.GetKeyDown(saveKey)) {
            storage.Save(this);
        }
        else if (Input.GetKeyDown(loadKey)) {
            BeginNewGame();
            storage.Load(this);
        }
    }

    void Awake() {
        objects = new List<PersistableObject>();
    }

    void CreateObject () {
        PersistableObject o = Instantiate(prefab);
        Transform t = o.transform;
        t.localPosition = Random.insideUnitSphere * 5f;
        t.localRotation = Random.rotation;
        t.localScale = Vector3.one * Random.Range(0.1f, 1f);
        objects.Add(o);
    }

    void BeginNewGame () {
        for (int i = 0; i < objects.Count; i++) {
            Destroy(objects[i].gameObject);
        }
        objects.Clear();
    }
}
