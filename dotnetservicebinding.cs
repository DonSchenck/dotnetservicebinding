using System;
using System.Collections.Generic;
using System.IO;

namespace KubeServiceBinding
{
    public class DotnetServiceBinding
    {
        private static Dictionary<string, string> _bindingsDictionary;

        public Dictionary<string, string> GetBindings(string type)
        {
            try
            {
                var bindingDirectory =
                    Environment.GetEnvironmentVariable("SERVICE_BINDING_ROOT");
                _bindingsDictionary = new Dictionary<string, string>();
                ProcessDirectoryTree (bindingDirectory, type);
                return _bindingsDictionary;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void ProcessDirectoryTree(string directory, string type)
        {
            // Walk down the directory tree and include ONLY the files
            // in the directory where the file named "type" exists
            // AND the contents of that file matches the type of binding
            // requested, e.g. "kafka".
            // For each file, use the filename
            // as the key and the contents as the value, and add this
            // key-value pair to a list of key-value pairs.
            // At the end, return the list.
            if (
                File.Exists(directory + "/type") &&
                System.IO.File.ReadAllText(directory + "/type") == type
            )
            {
                foreach (string f in Directory.GetFiles(directory))
                {
                    GetFileContents (f);
                }
            }

            foreach (string d in Directory.GetDirectories(directory))
            {
                ProcessDirectoryTree (d, type);
            }
        }

        private static void GetFileContents(string filename)
        {
            // Get contents of file
            string value = System.IO.File.ReadAllText(filename);
            string key = Path.GetFileName(filename);
            Dictionary<string, string> d = new Dictionary<string, string>();
            if (_bindingsDictionary.ContainsKey(key))
            {
                _bindingsDictionary["key"] = value;
            }
            else
            {
                _bindingsDictionary.Add (key, value);
            }
        }
    }
}
