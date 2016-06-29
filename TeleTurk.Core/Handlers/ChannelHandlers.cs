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
    class ChannelHandlers
    {
        private MtProtoSender _sender;

        public ChannelHandlers(MtProtoSender sender)
        {
            _sender = sender;
        }

        public async Task<bool> readHistory(InputChannel channel, int max_id)
        {
            return await _sender.SendReceive<bool>(new TL.ChannelsReadHistoryRequest(channel, max_id));
        }

        public async Task<MessagesAffectedMessages> deleteMessages(InputChannel channel, List<int> id)
        {
            return await _sender.SendReceive<MessagesAffectedMessages>(new TL.ChannelsDeleteMessagesRequest(channel, id));
        }

        public async Task<MessagesAffectedHistory> deleteUserHistory(InputChannel channel, InputUser user_id)
        {
            return await _sender.SendReceive<MessagesAffectedHistory>(new TL.ChannelsDeleteUserHistoryRequest(channel, user_id));
        }

        public async Task<bool> reportSpam(InputChannel channel, InputUser user_id, List<int> id)
        {
            return await _sender.SendReceive<bool>(new TL.ChannelsReportSpamRequest(channel, user_id, id));
        }

        public async Task<MessagesMessages> getMessages(InputChannel channel, List<int> id)
        {
            return await _sender.SendReceive<MessagesMessages>(new TL.ChannelsGetMessagesRequest(channel, id));
        }

        public async Task<ChannelsChannelParticipants> getParticipants(InputChannel channel, ChannelParticipantsFilter filter, int offset, int limit)
        {
            return await _sender.SendReceive<ChannelsChannelParticipants>(new TL.ChannelsGetParticipantsRequest(channel, filter, offset, limit));
        }

        public async Task<ChannelsChannelParticipant> getParticipant(InputChannel channel, InputUser user_id)
        {
            return await _sender.SendReceive<ChannelsChannelParticipant>(new TL.ChannelsGetParticipantRequest(channel, user_id));
        }

        public async Task<MessagesChats> getChannels(List<InputChannel> id)
        {
            return await _sender.SendReceive<MessagesChats>(new TL.ChannelsGetChannelsRequest(id));
        }

        public async Task<MessagesChatFull> getFullChannel(InputChannel channel)
        {
            return await _sender.SendReceive<MessagesChatFull>(new TL.ChannelsGetFullChannelRequest(channel));
        }

        public async Task<Updates> createChannel(True broadcast, True megagroup, string title, string about)
        {
            return await _sender.SendReceive<Updates>(new TL.ChannelsCreateChannelRequest(broadcast, megagroup, title, about));
        }

        public async Task<bool> editAbout(InputChannel channel, string about)
        {
            return await _sender.SendReceive<bool>(new TL.ChannelsEditAboutRequest(channel, about));
        }

        public async Task<Updates> editAdmin(InputChannel channel, InputUser user_id, ChannelParticipantRole role)
        {
            return await _sender.SendReceive<Updates>(new TL.ChannelsEditAdminRequest(channel, user_id, role));
        }

        public async Task<Updates> editTitle(InputChannel channel, string title)
        {
            return await _sender.SendReceive<Updates>(new TL.ChannelsEditTitleRequest(channel, title));
        }

        public async Task<Updates> editPhoto(InputChannel channel,InputChatPhoto photo)
        {
            return await _sender.SendReceive<Updates>(new TL.ChannelsEditPhotoRequest(channel, photo));
        }

        public async Task<bool> checkUsername(InputChannel channel, string username)
        {
            return await _sender.SendReceive<bool>(new TL.ChannelsCheckUsernameRequest(channel, username));
        }

        public async Task<bool> updateUsername(InputChannel channel, string username)
        {
            return await _sender.SendReceive<bool>(new TL.ChannelsUpdateUsernameRequest(channel, username));
        }

        public async Task<Updates> joinChannel(InputChannel channel)
        {
            return await _sender.SendReceive<Updates>(new TL.ChannelsJoinChannelRequest(channel));
        }

        public async Task<Updates> leaveChannel(InputChannel channel)
        {
            return await _sender.SendReceive<Updates>(new TL.ChannelsLeaveChannelRequest(channel));
        }

        public async Task<Updates> inviteToChannel(InputChannel channel, List<InputUser> users)
        {
            return await _sender.SendReceive<Updates>(new TL.ChannelsInviteToChannelRequest(channel, users));
        }

        public async Task<Updates> kickFromChannel(InputChannel channel, InputUser user_id, bool kicked)
        {
            return await _sender.SendReceive<Updates>(new TL.ChannelsKickFromChannelRequest(channel, user_id, kicked));
        }

        public async Task<ExportedChatInvite> exportInvite(InputChannel channel)
        {
            return await _sender.SendReceive<ExportedChatInvite>(new TL.ChannelsExportInviteRequest(channel));
        }

        public async Task<Updates> deleteChannel(InputChannel channel)
        {
            return await _sender.SendReceive<Updates>(new TL.ChannelsDeleteChannelRequest(channel));
        }

        public async Task<Updates> toggleInvites(InputChannel channel, bool enabled)
        {
            return await _sender.SendReceive<Updates>(new TL.ChannelsToggleInvitesRequest(channel, enabled));
        }

        public async Task<ExportedMessageLink> exportMessageLink(InputChannel channel, int id)
        {
            return await _sender.SendReceive<ExportedMessageLink>(new TL.ChannelsExportMessageLinkRequest(channel, id));
        }

        public async Task<Updates> toggleSignatures(InputChannel channel, bool enabled)
        {
            return await _sender.SendReceive<Updates>(new TL.ChannelsToggleSignaturesRequest(channel, enabled));
        }

        public async Task<Updates> updatePinnedMessage(InputChannel channel, True silent, int id)
        {
            return await _sender.SendReceive<Updates>(new TL.ChannelsUpdatePinnedMessageRequest(silent, channel, id));
        }
    }
}
