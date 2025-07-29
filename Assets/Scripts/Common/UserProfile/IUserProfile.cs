using System.Collections.Generic;

namespace Common.Profile
{
    public interface IUserProfile
    {
        IInventory UserInventory { get; }

        void Update( IDictionary< string, object > data );

        //void FillData( IDictionary< string, object > data );
    }
}