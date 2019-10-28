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

using Declarations.Attributes;
using Implementation.Exceptions;
using Implementation.Utils;
using System;

namespace Implementation
{
    class VersionCompatibilityVerifier
    {
        private readonly Version m_currentVersion;

        public VersionCompatibilityVerifier(string currentVersion)
        {
            m_currentVersion = MiscUtils.ConvertToVersion(currentVersion);
        }

        internal void HandleDeprecatedApi(MaxLibVlcVersion maxVer)
        {
            Version lastSupported = new Version(maxVer.MaxVersion);
            if (m_currentVersion > lastSupported)
            {
                string msg = string.Format("{0} is obsolete since libVLC version {1}. {2} should be used instead",
                    maxVer.LibVlcModuleName, maxVer.MaxVersion, maxVer.Replacement);
                throw new LibVlcDepricatedApiException(msg);
            }
        }

        internal void HandleMissingApi(MinimalLibVlcVersion minVer)
        {
            Version supportedVersion = new Version(minVer.MinimalVersion);
            if (m_currentVersion < supportedVersion)
            {
                string msg = string.Format("Using libVLC {0}, while requested API available with libVLC {1} or higher",
                    m_currentVersion, minVer.MinimalVersion);
                throw new LibVlcFutureVersionException(msg);
            }
        }
    }
}
