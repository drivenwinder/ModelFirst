﻿
namespace EAP.Entity
{
    public enum DataState
    {
        /// <summary>
        /// Default state.
        /// </summary>
        None,
        /// <summary>
        /// The object was created by the client and then need to be inserting into database.
        /// </summary>
        Created,
        /// <summary>
        /// The object was deleted by the client and then need to be deleting from database.
        /// </summary>
        Deleted,
        /// <summary>
        /// The object has been modified by the client and then need to be updating from database.
        /// </summary>
        Modified,
        /// <summary>
        /// The object is initializing, and the event will not be fired.
        /// </summary>
        Initializing,
    }
}
