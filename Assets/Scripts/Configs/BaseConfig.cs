using System.Diagnostics;
using UnityEngine;

namespace Common.Configs
{
    public abstract class BaseConfig : ScriptableObject
    {
        public string NameEditor;
        public string Uid => name;

        private void OnValidate() => Validate();

        [ Conditional( "UNITY_EDITOR" ) ] public virtual void Validate() {}
    }
}