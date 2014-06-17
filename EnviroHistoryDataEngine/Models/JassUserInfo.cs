using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JassWeather.Models
{
    public class JassUserInfo
    {
        public int JassUserInfoID { get; set; }
        public string JassUserInfoUserName { get; set; }

        public int JassVariableGroupID { get; set; }
        public virtual JassVariableGroup JassVariableGroup { get; set; }

        public int JassLatLonGroupID { get; set; }
        public virtual JassLatLonGroup JassLatLonGroup { get; set; }


    }
}