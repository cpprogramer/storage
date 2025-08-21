using UnityEngine;

namespace StorageTest.Net
{
    public sealed class VersionNumberProvider : IVersionNumberProvider
    {
        public string VersionNumber => Application.version;
    }
}