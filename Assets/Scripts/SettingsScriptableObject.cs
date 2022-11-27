using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SettingsScriptableObject", order = 1)]
public class SettingsScriptableObject : ScriptableObject
{
    [SerializeField]
    private AnimationCurve initialDelayCurve;

    public float GetDelay()
    {
        return initialDelayCurve.Evaluate(Random.value);
    }
}