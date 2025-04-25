using UnityEngine;

public class TargetArea : MonoBehaviour
{
    public enum Operation
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }

    public Operation operation;
    public int value;
    public bool good;
}
