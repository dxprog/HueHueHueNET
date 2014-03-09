using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace HueController
{
    [DataContract]
    class Light
    {

        public int id;

        [DataMember]
        public LightState state;

        [DataMember]
        public String type;

        [DataMember]
        public String name;

        [DataMember]
        public String modelid;

        [DataMember]
        public String swversion;

        public static Light getLight(int id)
        {
            Light retVal = null;

            string json = HueTalker.apiGet("lights/" + id);
            if (!string.IsNullOrEmpty(json))
            {
                retVal = JsonHelper.deserializeObject<Light>(json);
                if (null != retVal)
                {
                    retVal.id = id;
                }
            }

            return retVal;
        }

        public void updateState()
        {
            HueTalker.apiPut("lights/" + this.id + "/state", this.state);
        }

    }
}
