//    nVLC
//    
//    Author:  Roman Ginzburg
//
//    nVLC is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    nVLC is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//    GNU General Public License for more details.
//     
// ========================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace Implementation.Exceptions
{
    /// <summary>
    /// Represents error that occured due to removal of libVLC module
    /// </summary>
    [Serializable]
    public class LibVlcRemovedModuleException : Exception
    {
        /// <summary>
        /// Initializes new instance of the class
        /// </summary>
        /// <param name="libVlcModuleName"></param>
        /// <param name="nVlcModuleName"></param>
        /// <param name="lastWorkingVersion"></param>
        public LibVlcRemovedModuleException(string libVlcModuleName, string nVlcModuleName, string lastWorkingVersion)
        {
            LibVlcModuleName = libVlcModuleName;
            LastWorkingVersion = lastWorkingVersion;
            this.nVlcModuleName = nVlcModuleName;
        }

        /// <summary>
        /// Name of the libVLC module
        /// </summary>
        public string LibVlcModuleName { get; private set; }

        /// <summary>
        /// Last version where module was operational
        /// </summary>
        public string LastWorkingVersion { get; private set; }

        /// <summary>
        /// Name of the nVLC object using removed module
        /// </summary>
        public string nVlcModuleName { get; private set; }

        /// <summary>
        /// Gets a message that describes the current exception
        /// </summary>
        public override string Message
        {
            get
            {
                return string.Format("{0} module based on {1} supported up to libVLC version {2}", nVlcModuleName, LibVlcModuleName, LastWorkingVersion);
            }
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            base.GetObjectData(info, context);

            info.AddValue("LibVlcModuleName", LibVlcModuleName);
            info.AddValue("LastWorkingVersion", LastWorkingVersion);
            info.AddValue("nVlcModuleName", nVlcModuleName);
        }
    }
}
