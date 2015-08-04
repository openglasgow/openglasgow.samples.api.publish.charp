using GCC.API.ConnectorLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCC.API.ClientDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //AddExternalFile();
            //AddExternalFileVersion();

            AddFile();
            //AddNewFileVersion();

            Console.ReadKey();
        }

        static Publisher GetPublisher()
        {
            return new Publisher(ConfigurationManager.AppSettings["AuthBase"],
                ConfigurationManager.AppSettings["TenantId"],
                ConfigurationManager.AppSettings["DataCollectionResourceId"],
                ConfigurationManager.AppSettings["ClientId"],
                ConfigurationManager.AppSettings["ClientKey"],                
                ConfigurationManager.AppSettings["SubscriptionKey"],
                Guid.Parse(ConfigurationManager.AppSettings["OrgId"]),
                Guid.Parse(ConfigurationManager.AppSettings["DatasetId"]));
        }

        /// <summary>
        /// Adds an external Url to a Dataset  in the City Data Hub
        /// </summary>
        private static void AddExternalFile()
        {
            Publisher publisher = GetPublisher();

            // get the json to send in the request
            string json = System.IO.File.ReadAllText(ConfigurationManager.AppSettings["PathToJsonNewFileRequestBodyExternal"]);

            Guid id = publisher.AddExternalFile(json);

            Console.WriteLine("Request Identifier is " + id);
        }


        /// <summary>
        /// Adds an external Url to a Dataset  in the City Data Hub
        /// </summary>
        private static void AddExternalFileVersion()
        {
            Publisher publisher = GetPublisher();

            // get the json to send in the request
            string json = System.IO.File.ReadAllText(ConfigurationManager.AppSettings["PathToJsonNewFileRequestBodyExternal"]);

            Guid id = publisher.AddExternalFileVersion(Guid.Parse(ConfigurationManager.AppSettings["FileId"]), json);

            Console.WriteLine("Request Identifier is " + id);
        }

        /// <summary>
        /// Adds a file to a Dataset in the City Data Hub
        /// </summary>
        private static void AddFile()
        {
            Publisher publisher = GetPublisher();

            // get the json to send in the request
            string json = System.IO.File.ReadAllText(ConfigurationManager.AppSettings["PathToJsonNewFileRequestBody"]);

            // get the file to send
            byte[] filedata = System.IO.File.ReadAllBytes(ConfigurationManager.AppSettings["PathToNewFile"]);

            Guid id = publisher.AddFile(json, filedata, ConfigurationManager.AppSettings["UploadedFileName"]);

            Console.WriteLine("Request Identifier is " + id);
        }

        /// <summary>
        /// Adds a new version of a file.
        /// </summary>
        private static void AddNewFileVersion()
        {
            Publisher publisher = GetPublisher();

            // get the json to send in the request
            string json = System.IO.File.ReadAllText(ConfigurationManager.AppSettings["PathToJsonNewFileRequestBody"]);

            // get the file to send
            byte[] filedata = System.IO.File.ReadAllBytes(ConfigurationManager.AppSettings["PathToNewFile"]);

            Guid id = publisher.AddFileVersion(Guid.Parse(ConfigurationManager.AppSettings["FileId"]), json, filedata, 
                ConfigurationManager.AppSettings["UploadedFileName"]);

            Console.WriteLine("Request Identifier is " + id);
        }
    }
}
