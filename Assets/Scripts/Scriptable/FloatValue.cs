using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] private float initialValue;
    public float RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = initialValue;
    }

    public void OnBeforeSerialize() 
    {
        initialValue = RuntimeValue;
    }

    public void LoadValue()
    {
        RuntimeValue = initialValue;
    }
}
