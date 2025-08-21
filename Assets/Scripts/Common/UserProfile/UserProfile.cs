using Common;
using Common.Profile;
using System;
using System.Collections.Generic;
using UniRx;

namespace StorageTest.Profile
{
    [ Serializable ]
    public sealed class UserProfile : IUserProfile
    {
        public IInventory UserInventory { get; }

        public UserProfile( IMessageBroker messageBroker ) => UserInventory = new UserInventory( messageBroker );

        public void Update( IDictionary< string, object > data ) => UserInventory.Update( data );

        //public void FillData( IDictionary< string, object > data ) => UserInventory.FillData( data );
    }
}