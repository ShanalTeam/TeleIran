namespace TeleTurk.Core
{
    public class Configuration
    {
        /// <summary>
        /// Please insert your API ID here, you can get your own API ID from http://my.telegram.org
        /// </summary>
        public int apiId = 0;

        /// <summary>
        /// Please insert your API Hash here, you can get your api Hash from http://my.telegram.org
        /// </summary>
        public string apiHash = "Insert your API Hash here";

        /// <summary>
        /// The phone number that will register/login/connect to Telegram
        /// </summary>
        public string phoneNumber = "Insert your phone number here";

        /// <summary>
        /// Current Layer we are invoking with Telegram servers.
        /// </summary>
        public int currentLayer = 53;

        /// <summary>
        /// The code of specific language we want to stablish
        /// Default: English -> en
        /// </summary>
        public string LangCode = "en";

        /// <summary>
        /// The device model that app runs on it 
        /// Default: Retrieved automatically
        /// </summary>
        public string DeviceModel => SystemInformation.GetModel();

        /// <summary>
        /// The OS version of the device
        /// Default: Retrieved automatically
        /// </summary>
        public string SystemVersion => SystemInformation.GetOSName();

        /// <summary>
        /// The version of application
        /// Default: 0.9-BETA
        /// </summary>
        public string AppVersion = "0.9-BETA";
        
        public Configuration() { }
    }
}
