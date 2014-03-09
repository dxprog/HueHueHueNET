using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace HueController
{
    [DataContract]
    class HubInfo
    {
        [DataMember]
        public string id;
        [DataMember]
        public string internalipaddress;
        [DataMember]
        public string macaddress;

        public static List<HubInfo> getHubs()
        {
            List<HubInfo> retVal = null;
            try
            {
                WebClient client = new WebClient();
                string json = client.DownloadString("http://www.meethue.com/api/nupnp");
                if (!string.IsNullOrEmpty(json))
                {
                    retVal = JsonHelper.deserializeObject<List<HubInfo>>(json);
                }
            }
            catch { }

            return retVal;
        }

    }

}
