using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleTurk.Core.Auth;
using TeleTurk.Core.MTProto;
using TeleTurk.Core.MTProto.Crypto;
using TeleTurk.Core.Network;

namespace TeleTurk.Core.Handlers
{
    class HelpHandlers
    {
        private MtProtoSender _sender;

        public async Task<TL.ConfigType> getConfig()
        {
            var request = new TL.HelpGetConfigRequest();

            await _sender.Send(request);
            await _sender.Receive(request);

            return (TL.ConfigType)request.Result;
        }

        public async Task<TL.NearestDcType> getNearestDc()
        {
            var request = new TL.HelpGetNearestDcRequest();

            await _sender.Send(request);
            await _sender.Receive(request);

            return (TL.NearestDcType)request.Result;
        }

        public async Task<HelpAppUpdate> getAppUpdate(string device_model, string system_version, string app_version, string lang_code)
        {
            var request = new TL.HelpGetAppUpdateRequest(device_model, system_version, app_version, lang_code);

            await _sender.Send(request);
            await _sender.Receive(request);

            return request.Result;
        }

        public async Task<bool> saveAppLog(List<InputAppEvent> events)
        {
            var request = new TL.HelpSaveAppLogRequest(events);

            await _sender.Send(request);
            await _sender.Receive(request);

            return request.Result is TL.BoolTrueType ? true : false;
        }

        public async Task<string> getInviteText(string lang_code)
        {
            var request = new TL.HelpGetInviteTextRequest(lang_code);

            await _sender.Send(request);
            await _sender.Receive(request);

            var element = (TL.HelpInviteTextType)(request.Result);

            return element.Message;
        }

        public async Task<Tuple<string , User>> getSupport()
        {
            var request = new TL.HelpGetSupportRequest();

            await _sender.Send(request);
            await _sender.Receive(request);

            var elemnt = (TL.HelpSupportType)request.Result;

            return new Tuple<string, User>(elemnt.PhoneNumber, elemnt.User);
        }

        public async Task<HelpAppChangelog> getAppChangelog(string device_model, string system_version, string app_version, string lang_code)
        {
            var request = new TL.HelpGetAppChangelogRequest(device_model, system_version, app_version, lang_code);

            await _sender.Send(request);
            await _sender.Receive(request);

            return request.Result;
        }

        public async Task<string> getTermsOfService()
        {
            var request = new TL.HelpGetTermsOfServiceRequest();

            await _sender.Send(request);
            await _sender.Receive(request);

            var elemnt = (TL.HelpTermsOfServiceType)request.Result;

            return elemnt.Text;
        }
    }
}
