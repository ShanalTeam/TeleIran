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
    class MessagesHandlers
    {
        private MtProtoSender _sender;

        public async Task<MessagesMessages> getMessages(List<int> id)
        {
            var request = new TL.MessagesGetMessagesRequest(id);

            await _sender.Send(request);
            await _sender.Receive(request);

            return request.Result;
        }

        public async Task<MessagesDialogs> getDialogs(int offset_date, int offset_id, InputPeer offset_peer, int limit)
        {
            var request = new TL.MessagesGetDialogsRequest(offset_date, offset_id, offset_peer, limit);

            await _sender.Send(request);
            await _sender.Receive(request);

            return request.Result;
        }

        public async Task<MessagesMessages> getHistory(InputPeer peer, int offset_id, int offset_date, int add_offset, int limit, int max_id, int min_id)
        {
            var request = new TL.MessagesGetHistoryRequest(peer, offset_id, offset_date, add_offset, limit, max_id, min_id);

            await _sender.Send(request);
            await _sender.Receive(request);

            return request.Result;
        }

        public async Task<MessagesMessages> search(True important_only, InputPeer peer, string q, MessagesFilter filter, int min_date, int max_date,
            int offset, int max_id, int limit)
        {
            var request = new TL.MessagesSearchRequest(important_only, peer, q, filter, min_date, max_date, offset, max_id, limit);

            await _sender.Send(request);
            await _sender.Receive(request);

            return request.Result;
        }

        public async Task<MessagesAffectedMessages> readHistory(InputPeer peer, int max_id)
        {
            var request = new TL.MessagesReadHistoryRequest(peer, max_id);

            await _sender.Send(request);
            await _sender.Receive(request);

            return request.Result;
        }
    }
}
