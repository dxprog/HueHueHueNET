using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace HueController
{
    class JsonHelper
    {

        public static T deserializeObject<T>(string json)
        {
            T retVal = default(T);
            MemoryStream stream = null;
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
                retVal = (T)ser.ReadObject(stream);
            }
            catch { }
            finally
            {
                if (null != stream)
                {
                    stream.Close();
                    stream.Dispose();
                }
            }

            return retVal;
        }

        public static string serializeObject(object payload)
        {
            string retVal = string.Empty;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(payload.GetType());
            MemoryStream memStream = new MemoryStream();
            StreamReader reader = null;
            try
            {
                ser.WriteObject(memStream, payload);
                memStream.Position = 0;
                reader = new StreamReader(memStream);
                retVal = reader.ReadToEnd();
            }
            catch { }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
                memStream.Close();
                memStream.Dispose();
            }

            return retVal;
        }

    }
}
