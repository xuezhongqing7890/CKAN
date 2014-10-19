namespace CKAN.KerbalStuff {

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System.Net;
    using System;
    using log4net;
    using CKAN.KerbalStuff;

    public class KSMod
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(KSMod));

        // These get filled in from JSON deserialisation.
        public KSVersion[] versions;
        public string name;
        public string license;
        public string short_description;

        public override string ToString ()
        {
            return string.Format ("{0}", name);
        }

        /// <summary>
        /// Takes a JObject and inflates it with KS metadata.
        /// This will not overwrite fields that already exist.
        /// </summary>

        public void InflateMetadata(JObject metadata, KSVersion version)
        {
            Inflate(metadata, "spec_version", "1"); // CKAN spec version
            Inflate(metadata, "name", name);
            Inflate(metadata, "license", license);
            Inflate(metadata, "abstract", short_description);
            Inflate(metadata, "version", version.friendly_version.ToString());
            Inflate(metadata, "download", Uri.EscapeUriString(version.download_path) );
            Inflate(metadata, "comment", "Generated by ks2ckan");
            Inflate(metadata, "ksp_version", version.KSP_version.ToString());
        }

        static internal void Inflate(JObject metadata, string key, string value)
        {
            if (metadata[key] == null)
            {
                log.DebugFormat("Setting {0} to {1}", key, value);
                metadata[key] = value;
            }
            else
            {
                log.DebugFormat("Leaving {0} as {1}", key, metadata[key]);
            }

        }

    }
}

