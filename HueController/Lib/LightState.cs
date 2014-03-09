using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HueController
{
    [DataContract]
    class LightState
    {
        [DataMember (EmitDefaultValue=false)]
        public UInt32 hue;
        [DataMember]
        public Boolean on;
        [DataMember]
        public String effect;
        [DataMember]
        public String alert;
        [DataMember]
        public byte bri;
        [DataMember]
        public byte sat;
        [DataMember]
        public int ct;
        [DataMember]
        public List<float> xy;
        [DataMember]
        public Boolean reachable;
        [DataMember]
        public String colormode;
    }
}
