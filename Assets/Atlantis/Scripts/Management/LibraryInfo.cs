using UnityEngine;
using System;

[Serializable]
public class LibraryInfo<Model>
    where Model : ILibraryModel
{
    public Model model
    {
        get { return _model; }
    }

    [SerializeField] Model _model;
}
