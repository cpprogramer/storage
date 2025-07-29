using UnityEngine;

namespace MonopolySpace.Net
{
    public sealed class VersionNumberProvider : IVersionNumberProvider
    {
        public string VersionNumber => Application.version;
    }
}