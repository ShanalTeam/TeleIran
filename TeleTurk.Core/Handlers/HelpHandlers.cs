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

        public HelpHandlers(MtProtoSender sender)
        {
            _sender = sender;
        }

        public async Task<Config> getConfig()
        {
            return await _sender.SendReceive<Config>(new TL.HelpGetConfigRequest());
        }

        public async Task<NearestDc> getNearestDc()
        {
            return await _sender.SendReceive<NearestDc>(new TL.HelpGetNearestDcRequest());
        }

        public async Task<HelpAppUpdate> getAppUpdate()
        {
            return await _sender.SendReceive<HelpAppUpdate>(new TL.HelpGetAppUpdateRequest());
        }

        public async Task<bool> saveAppLog(List<InputAppEvent> events)
        {
            return await _sender.SendReceive<bool>(new TL.HelpSaveAppLogRequest(events));
        }

        public async Task<string> getInviteText()
        {
            return await _sender.SendReceive<string>(new TL.HelpGetInviteTextRequest());
        }

        public async Task<HelpSupport> getSupport()
        {
            return await _sender.SendReceive<HelpSupport>(new TL.HelpGetSupportRequest());
        }

        public async Task<HelpAppChangelog> getAppChangelog()
        {
            return await _sender.SendReceive<HelpAppChangelog>(new TL.HelpGetAppChangelogRequest());
        }

        public async Task<string> getTermsOfService()
        {
            return await _sender.SendReceive<string>(new TL.HelpGetTermsOfServiceRequest());
        }
    }
}
