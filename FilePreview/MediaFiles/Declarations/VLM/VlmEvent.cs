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
using System.Text;

namespace Declarations.VLM
{
    /// <summary>
    /// Represents VLM events 
    /// </summary>
    [Serializable]
    public class VlmEvent : EventArgs
    {
        /// <summary>
        /// Gets the VLM media name
        /// </summary>
        public string MediaName { get; private set; }

        /// <summary>
        /// Gets the VLM media instance name
        /// </summary>
        public string InstanceName { get; private set; }

        /// <summary>
        /// Initializes new intance of VlmEvent
        /// </summary>
        /// <param name="instanceName"></param>
        /// <param name="mediaName"></param>
        public VlmEvent(string instanceName, string mediaName)
        {
            InstanceName = instanceName;
            MediaName = mediaName;
        }
    }
}
