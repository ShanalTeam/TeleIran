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

        public MessagesHandlers(MtProtoSender sender)
        {
            _sender = sender;
        }

        public async Task<MessagesMessages> getMessages(List<int> id)
        {
            return await _sender.SendReceive<MessagesMessages>(new TL.MessagesGetMessagesRequest(id));
        }

        public async Task<MessagesMessages> getDialogs(int offset_date, int offset_id, InputPeer offset_peer, int limit)
        {
            return await _sender.SendReceive<MessagesMessages>(new TL.MessagesGetDialogsRequest(offset_date, offset_id, offset_peer, limit));
        }

        public async Task<MessagesMessages> getHistory(InputPeer peer, int offset_id, int offset_date, 
            int add_offset, int limit, int max_id, int min_id)
        {
            return await _sender.SendReceive<MessagesMessages>(new 
                TL.MessagesGetHistoryRequest(peer, offset_id, offset_date, add_offset, limit, max_id, min_id));
        }

        /*
        public async Task<MessagesMessages> search(True important_only, InputPeer peer, string q, MessagesFilter filter, int min_date, 
            int max_date, int offset, int max_id, int limit)
        {
            return await _sender.SendReceive<MessagesMessages>(new 
                TL.MessagesSearchRequest(important_only, peer, q, filter, min_date, max_date, offset, max_id, limit));
        }
        */

        public async Task<MessagesAffectedMessages> readHistory(InputPeer peer, int max_id)
        {
            return await _sender.SendReceive<MessagesAffectedMessages>(new TL.MessagesReadHistoryRequest(peer, max_id));
        }

        public async Task<MessagesAffectedMessages> deleteHistory(True JustClear, InputPeer peer, int max_id)
        {
            return await _sender.SendReceive<MessagesAffectedMessages>(new TL.MessagesDeleteHistoryRequest(JustClear, peer, max_id));
        }
        




    }
}
