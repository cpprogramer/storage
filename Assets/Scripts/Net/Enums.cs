namespace StorageTest.Net
{
    public enum DisconnectCause
    {
        /// <summary>No error was tracked.</summary>
        None,

        /// <summary>
        ///     OnStatusChanged: The server is not available or the address is wrong. Make sure the port is provided and the
        ///     server is up.
        /// </summary>
        ExceptionOnConnect,

        /// <summary>
        ///     OnStatusChanged: Dns resolution for a hostname failed. The exception for this is being caught and logged with
        ///     error level.
        /// </summary>
        DnsExceptionOnConnect,

        /// <summary>
        ///     OnStatusChanged: The server address was parsed as IPv4 illegally. An illegal address would be e.g.
        ///     192.168.1.300. IPAddress.TryParse() will let this pass but our check won't.
        /// </summary>
        ServerAddressInvalid,

        /// <summary>
        ///     OnStatusChanged: Some internal exception caused the socket code to fail. This may happen if you attempt to
        ///     connect locally but the server is not available. In doubt: Contact Exit Games.
        /// </summary>
        Exception,

        /// <summary>
        ///     OnStatusChanged: The server disconnected this client due to timing out (missing acknowledgement from the
        ///     client).
        /// </summary>
        ServerTimeout,

        /// <summary>OnStatusChanged: This client detected that the server's responses are not received in due time.</summary>
        ClientTimeout,

        /// <summary>OnStatusChanged: The server disconnected this client from within the room's logic (the C# code).</summary>
        DisconnectByServerLogic,

        /// <summary>OnStatusChanged: The server disconnected this client for unknown reasons.</summary>
        DisconnectByServerReasonUnknown,

        /// <summary>
        ///     OnOperationResponse: Authenticate in the Photon Cloud with invalid AppId. Update your subscription or contact
        ///     Exit Games.
        /// </summary>
        InvalidAuthentication,

        /// <summary>
        ///     OnOperationResponse: Authenticate in the Photon Cloud with invalid client values or custom authentication
        ///     setup in Cloud Dashboard.
        /// </summary>
        CustomAuthenticationFailed,

        /// <summary>
        ///     The authentication ticket should provide access to any Photon Cloud server without doing another
        ///     authentication-service call. However, the ticket expired.
        /// </summary>
        AuthenticationTicketExpired,

        /// <summary>
        ///     OnOperationResponse: Authenticate (temporarily) failed when using a Photon Cloud subscription without CCU
        ///     Burst. Update your subscription.
        /// </summary>
        MaxCcuReached,

        /// <summary>
        ///     OnOperationResponse: Authenticate when the app's Photon Cloud subscription is locked to some (other)
        ///     region(s). Update your subscription or master server address.
        /// </summary>
        InvalidRegion,

        /// <summary>
        ///     OnOperationResponse: Operation that's (currently) not available for this client (not authorized usually). Only
        ///     tracked for op Authenticate.
        /// </summary>
        OperationNotAllowedInCurrentState,

        /// <summary>OnStatusChanged: The client disconnected from within the logic (the C# code).</summary>
        DisconnectByClientLogic,

        /// <summary>
        ///     The client called an operation too frequently and got disconnected due to hitting the OperationLimit. This
        ///     triggers a client-side disconnect, too.
        /// </summary>
        /// <remarks>
        ///     To protect the server, some operations have a limit. When an OperationResponse fails with
        ///     ErrorCode.OperationLimitReached, the client disconnects.
        /// </remarks>
        DisconnectByOperationLimit,

        /// <summary>The client received a "Disconnect Message" from the server. Check the debug logs for details.</summary>
        DisconnectByDisconnectMessage,

        /// <summary>Used in case the application quits. Can be useful to not load new scenes or re-connect in OnDisconnected.</summary>
        /// <remarks>
        ///     ConnectionHandler.OnDisable() will use this, if the Unity engine already called OnApplicationQuit
        ///     (ConnectionHandler.AppQuits = true).
        /// </remarks>
        ApplicationQuit,

        /// <summary>Used by the ConnectionHandler to end a connection for lack of RealtimeClient.Service calls.</summary>
        /// <remarks>
        ///     Without calling Service (or SendOutgoingCommands and DispatchIncomingCommands), the network connection does not
        ///     process
        ///     messages and events. This can happen if apps are in the background or the main loop is paused (due to loading
        ///     assets, etc).
        ///     ConnectionHandler.KeepAliveInBackground defines how long the connection is kept alive without calls to Service.
        ///     Closing such connections prevents clients from staying connected "forever" without actually processing network
        ///     updates
        ///     (which would waste CCUs and connections).
        /// </remarks>
        ClientServiceInactivity
    }
}