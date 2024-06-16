namespace DiggerFlight.PcnAcnCalculator.FileManagement
{
    using System.IO;
    using System.Threading;
    using System.Reflection;

    internal class FileControl
    {
        private static string aircraftAsns = @"Configuration\Acn.json";
        private static string airfieldOperation = @"Configuration\AirfieldOps.json";

        public enum ConfigFileToFetch
        {
            AirCraftAcns,
            AirfieldOperations
        }

        public static string GetJsonFile(ConfigFileToFetch configFileToFetch)
        {
            var currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var configFileName = aircraftAsns;
            if (configFileToFetch == ConfigFileToFetch.AirfieldOperations) configFileName = airfieldOperation;
            var fileName = Path.Combine(currentDirectory, configFileName);

            FileInfo file = new FileInfo(fileName);
            if (file.Exists) 
            {
                try
                {
                    WaitTillFileIsntLocked(file);
                    var fileData = File.ReadAllText(fileName);
                    return fileData;
                }
                catch { return null; }
            }
            return null;
        }

        internal static void WaitTillFileIsntLocked(FileInfo file, int timeoutInSec = 20)
        {
            while(timeoutInSec > 0)
            {
                if (!IsFileLocked(file)) break;
                else
                {
                    Thread.Sleep(1000);
                    timeoutInSec--;
                }
            }
        }

        protected static bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;
            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                if (stream != null) stream.Close();
            }
            return false;
        }
    }
}
