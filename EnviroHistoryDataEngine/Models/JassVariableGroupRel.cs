using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JassWeather.Models
{
    public class JassVariableGroupRel
    {
        public int JassVariableGroupRelID { get; set; }
        public int JassVariableGroupID { get; set; }
        public virtual JassVariableGroup JassVariableGroup { get; set; }
        public int JassVariableID { get; set; }
        public virtual JassVariable JassVariable { get; set; }
    }
}