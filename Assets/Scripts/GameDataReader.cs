using UnityEngine;
using System.IO;

public class GameDataReader {
    BinaryReader reader;

    public GameDataReader(BinaryReader reader) {
        this.reader = reader;
    }

    public float readFloat() {
        return reader.ReadSingle();
    }

    public int readInt() {
        return reader.ReadInt32();
    }

    public Quaternion readQuaternion() {
        Quaternion value;
        value.x = reader.ReadSingle();
        value.y = reader.ReadSingle();
        value.z = reader.ReadSingle();
        value.w = reader.ReadSingle();
        return value;
    }

    public Vector3 readVector3() {
        Vector3 value;
        value.x = reader.ReadSingle();
        value.y = reader.ReadSingle();
        value.z = reader.ReadSingle();
        return value;
    }
}
