using Common;
using StorageTest.Net;
using System.Collections.Generic;
using UnityEngine;

namespace StorageTest.UI.View
{
    public sealed class UIRoomListView : MonoBehaviour
    {
        [ SerializeField ] private UIRoomInfo _roomInfoTemplate;
        [ SerializeField ] private Transform _parentForRooms;
        private readonly Dictionary< string, UIRoomInfo > _rooms = new();

        public void Setup( IRoomInfo[] rooms )
        {
            Debug.LogError( $"ROOMS COUNT {rooms.Length}" );
            CleanUp();
            _roomInfoTemplate.gameObject.SetActive( true );
            rooms.ForEach( item =>
            {
                UIRoomInfo inst = Instantiate( _roomInfoTemplate );
                inst.transform.SetParent( _parentForRooms, false );
                inst.Setup( item );
                _rooms.Add( item.Name, inst );
            } );

            _roomInfoTemplate.gameObject.SetActive( false );
        }

        private void CleanUp()
        {
            _rooms.ForEach( item =>
            {
                item.Value.transform.SetParent( null, false );
                Utils.Destroy( item.Value.gameObject );
            } );

            _rooms.Clear();
        }
    }
}