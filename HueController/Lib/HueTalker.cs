using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using System.Runtime.Serialization.Json;
using System.Text;

namespace HueController
{
    class HueTalker
    {

        private static String _username;
        private static String _hubIp;
        private static String _apiLocation;

        public static Boolean init(string username)
        {
            Boolean retVal = false;
            List<HubInfo> hubs = HubInfo.getHubs();
            if (null != hubs && hubs.Count > 0)
            {
                _hubIp = hubs[0].internalipaddress;
                _username = username;
                _apiLocation = "http://" + _hubIp + "/api/" + _username + "/";
                retVal = true;
            }
            return retVal;
        }

        public static string apiPut(string endpoint, object payload)
        {
            return _talk("PUT", endpoint, payload);
        }

        public static string apiGet(string endpoint)
        {
            return _talk("GET", endpoint, null);
        }

        private static string _talk(string method, string endpoint, object payload)
        {
            string retVal = string.Empty;
            string json = string.Empty;
            if (null != payload)
            {
                json = JsonHelper.serializeObject(payload);
            }
            HttpWebRequest request = HttpWebRequest.Create(_apiLocation + endpoint) as HttpWebRequest;
            HttpWebResponse response = null;
            request.Method = method;
            Stream writer = null;
            StreamReader reader = null;
            try
            {
                if (null != payload) {
                    request.ContentLength = json.Length;
                    writer = request.GetRequestStream();
                    writer.Write(Encoding.UTF8.GetBytes(json), 0, json.Length);
                }
                response = request.GetResponse() as HttpWebResponse;
                reader = new StreamReader(response.GetResponseStream());
                retVal = reader.ReadToEnd();
            }
            catch { }
            finally
            {
                if (null != reader)
                {
                    reader.Close();
                    reader.Dispose();
                }

                if (null != response)
                {
                    response.Close();
                    response.Dispose();
                }

                if (null != writer)
                {
                    writer.Close();
                    writer.Dispose();
                }

            }
            return retVal;
        }

    }
}
