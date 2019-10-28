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
// ===============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Declarations.Enums
{
    [Flags]
    public enum MediaParseType
    {
        /**
         * Parse media if it's a local file
         */
        ParseLocal = 0x00,
        /**
         * Parse media even if it's a network file
         */
        ParseNetwork = 0x01,
        /**
         * Fetch meta and covert art using local resources
         */
        FetchLocal = 0x02,
        /**
         * Fetch meta and covert art using network resources
         */
        FetchNetwork = 0x04,

        /**
         * Interact with the user (via libvlc_dialog_cbs) when preparsing this item
         * (and not its sub items). Set this flag in order to receive a callback
         * when the input is asking for credentials.
         */
        DoInteract = 0x08,
    }
}
