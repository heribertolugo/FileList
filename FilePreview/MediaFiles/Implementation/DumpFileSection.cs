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
using System.Configuration;
using System.Linq;
using System.Text;

namespace Implementation
{
    internal class DumpFiles : ConfigurationSection
    {
        [ConfigurationProperty("generateDumpFileOnCrash", IsRequired = true)]
        public bool GenerateDumpFileOnCrash
        {
            get
            {
                return (bool)this["generateDumpFileOnCrash"];
            }
            set
            {
                this["generateDumpFileOnCrash"] = value;
            }
        }

        [ConfigurationProperty("directory", IsRequired = true)]
        public string Directory
        {
            get
            {
                return (string)this["directory"];
            }
            set
            {
                this["directory"] = value;
            }
        }

        [ConfigurationProperty("maxdumps", IsRequired = true)]
        public int MaxDumps
        {
            get
            {
                return (int)this["maxdumps"];
            }
            set
            {
                this["maxdumps"] = value;
            }
        }
    }
}
