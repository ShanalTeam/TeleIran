using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleTurk.Core
{
    public class Configuration
    {
        /// <apiID>
        /// Please insert your api ID here , you can get your api ID from telegram.org
        /// </apiID>
        public int apiId = 0;
        /// <apiHash>
        /// Please insert your api Hash here , you can get your api Hash from telegram.org
        /// </apiHash>
        public string apiHash = "Insert your api hash here";
        /// <phoneNumber>
        /// The phone number that will register to telegram
        /// </phoneNumber>
        public string phoneNumber = "***REMOVED***";
        /// <currentLayer>
        /// Current Layer we are invoking with telegram servers.
        /// </currentLayer>
        public int currentLayer = 52;
        /// <LangCode>
        /// The code of specific language we want to stablish, 
        /// Defualt : English -> en
        /// </LangCode>
        public string LangCode = "en";
        /// <DeviceModel>
        /// The device model that app runs on it 
        /// Defualt : 80EU
        /// </DeviceModel>
        public string DeviceModel = "80EU";
        /// <systemVersion>
        /// The Os version of the device
        /// Defualt : Windows
        /// </systemVersion>
        public string SystemVersion = "Windows 10 Home";
        /// <AppVersion>
        /// The version of application
        /// Defualt : Windows
        /// </AppVersion>
        public string AppVersion = "0.9-BETA";
        public Configuration()
        {

        }
    }
}
