using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace SLBMVC.Models
{
    public class TableAlbumJsonModel
    {
        public static string WriteFromObject()
        {
            //Create User object.  
            string[] Album = {"kaczmarski", "gintrowski", "łapiński" };

            //Create a stream to serialize the object to.  
            MemoryStream ms = new MemoryStream();

            // Serializer the User object to the stream.  
            DataContractJsonSerializer ser = new DataContractJsonSerializer(Album.GetType());
            ser.WriteObject(ms, Album);
            byte[] json = ms.ToArray();
            ms.Close();
            return Encoding.UTF8.GetString(json, 0, json.Length);
        }
    }
}
