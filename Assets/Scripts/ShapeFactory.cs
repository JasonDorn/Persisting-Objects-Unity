using UnityEngine;

[CreateAssetMenu]
public class ShapeFactory : ScriptableObject {
    [SerializeField]
    Shape[] prefabs;

    public Shape Get (int shapeIndex) {
        return Instantiate(prefabs[shapeId]);
    }

    public Shape Random () {
        return Get(Random.Range(0, prefabs.Length));
    }
}
