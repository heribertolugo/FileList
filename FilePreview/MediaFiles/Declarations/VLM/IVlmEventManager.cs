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

namespace Declarations.VLM
{
    /// <summary>
    /// Gets events raised by the VLM object
    /// </summary>
    public interface IVlmEventManager
    {
        event EventHandler<VlmEvent> MediaAdded;
        event EventHandler<VlmEvent> MediaChanged;
        event EventHandler<VlmEvent> MediaInstanceEnd;
        event EventHandler<VlmEvent> MediaInstanceError;
        event EventHandler<VlmEvent> MediaInstanceInit;
        event EventHandler<VlmEvent> MediaInstanceOpening;
        event EventHandler<VlmEvent> MediaInstancePause;
        event EventHandler<VlmEvent> MediaInstancePlaying;
        event EventHandler<VlmEvent> MediaInstanceStarted;
        event EventHandler<VlmEvent> MediaInstanceStopped;
        event EventHandler<VlmEvent> MediaRemoved;
    }
}
