using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Property : ScriptableObject
{
    public int Value;
    public int baseValue;
    public int runtimeBaseValue;
    public bool isState;

    public List<Modifier> Modifiers = new List<Modifier>();

    public void Decrease(int amount)
    {
        Value -= amount;
        if(Value <=0)
        {
            Value = 0;
        }
    }

    public void Increase(int amount)
    {
         Value += amount;
    }

    public void IncreaseRuntimeBaseValue(int amount)
    {
        runtimeBaseValue += amount;
        Value = runtimeBaseValue;
    }

    public void RestoreBackToRuntimeBaseValue()
    {
        Value = runtimeBaseValue;
    }

    public void RemoveModifier(Modifier modifier)
    {
        Modifiers.Remove(modifier);
    }

    public void ApplyModifier(Modifier modifier)
    {
        Modifiers.Add(modifier);
    }

    #region reset_on_unity_editor
#if UNITY_EDITOR
    private void OnEnable()
    {
        UnityEditor.EditorApplication.playModeStateChanged += EditorApplication_playModeStateChanged;
    }

    private void OnDisable()
    {
        UnityEditor.EditorApplication.playModeStateChanged -= EditorApplication_playModeStateChanged;
    }

    private void EditorApplication_playModeStateChanged(UnityEditor.PlayModeStateChange state)
    {
        if (state == UnityEditor.PlayModeStateChange.EnteredEditMode || state == UnityEditor.PlayModeStateChange.EnteredPlayMode)
        {
            runtimeBaseValue = baseValue;
            Value = baseValue;
            Modifiers.Clear();
        }
    }
#endif
    #endregion
}
