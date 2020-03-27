using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Modifier : ScriptableObject
{
    #region Info
    public string Name;
    [TextArea]
    public string Description;
    #endregion

    public GameObject effect;

    #region property
    //public PropertyType PropertyType;
    public Property property;
    #endregion

    #region Affect
    protected bool InAffect = false;
    public bool Acted = false;
    public bool FinishedActing = false;
    #endregion

    public List<ModifierBehavior> Behaivors;
    private List<ModifierBehavior> BehaivorsClones = new List<ModifierBehavior>();

    //Insert behaivor stuff
    public void Act()
    {
        if(!Acted)
        {
            Acted = true;

            foreach (ModifierBehavior behaivor in Behaivors)
            {
                ModifierBehavior ClonedBehaiver = Instantiate(behaivor);

                BehaivorsClones.Add(ClonedBehaiver);
            }
        }


        InAffect = true;

        if (InAffect)
        {
            foreach (ModifierBehavior behaivor in BehaivorsClones)
            {
                behaivor.Act();
            }

            CheckAndSetEnded();
        }
    }

    protected void CheckAndSetEnded()
    {
        bool ended = true;
        foreach(ModifierBehavior behaivor in BehaivorsClones)
        {
            if(!behaivor.FinishedActing)
            {
                ended = false;
            }
        }

        if(ended)
        {
            FinishedActing = true;
        }
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
            BehaivorsClones.Clear();
        }
    }
#endif
    #endregion
}
