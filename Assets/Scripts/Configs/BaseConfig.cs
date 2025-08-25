using System.Diagnostics;
using UnityEngine;

namespace Common.Configs
{
    public abstract class BaseConfig : ScriptableObject
    {
        public string NameEditor;
        public string Uid => name;

        [ Conditional( "UNITY_EDITOR" ) ] public virtual void Validate() {}

        private void OnValidate() => Validate();
    }
}