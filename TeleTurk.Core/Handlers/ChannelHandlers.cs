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
        private List<Dialog> dialogs;
        private List<Message> messages;
        private List<Chat> chats;
        private List<User> users;
        private List<MessageGroup> messageGroup;
        private List<ChannelParticipant> participants;
        private MtProtoSender _sender;
        private int pts;
        private int count;
        private int pts_count;

        public async Task<Tuple<List<Dialog>, List<Message>, List<Chat>, List<User>>> getDialogs(int offset, int limit)
        {
            var request = new TL.ChannelsGetDialogsRequest(offset, limit);
            await _sender.Send(request);
            await _sender.Receive(request);

            if (request.Result is TL.MessagesDialogsType)
            {
                var result = (TL.MessagesDialogsType)request.Result;
                dialogs = result.Dialogs;
                messages = result.Messages;
                chats = result.Chats;
                users = result.Users;
            }
            else
            {
                var result = (TL.MessagesDialogsSliceType)request.Result;
                dialogs = result.Dialogs;
                messages = result.Messages;
                chats = result.Chats;
                users = result.Users;
            }

            return new Tuple<List<Dialog>, List<Message>, List<Chat>, List<User>>(dialogs, messages, chats, users);
        }

        public async Task<Tuple<List<Message>, List<Chat>, List<User>, List<MessageGroup>, int, int>>
            getImportantHistory(InputChannel input, int offset_id, int offset_date, int add_offset, int limit, int max_id, int min_id)
        {
            var request = new TL.ChannelsGetImportantHistoryRequest(input, offset_id, offset_date, add_offset, limit, max_id, min_id);

            await _sender.Send(request);
            await _sender.Receive(request);

            if (request.Result is TL.MessagesMessagesType)
            {
                var result = (TL.MessagesMessagesType)request.Result;
                messages = result.Messages;
                chats = result.Chats;
                users = result.Users;
            }
            else if (request.Result is TL.MessagesMessagesSliceType)
            {
                var result = (TL.MessagesMessagesSliceType)request.Result;
                messages = result.Messages;
                chats = result.Chats;
                users = result.Users;
            }
            else
            {
                var result = (TL.MessagesChannelMessagesType)request.Result;
                messages = result.Messages;
                chats = result.Chats;
                users = result.Users;
                messageGroup = result.Collapsed;
                pts = result.Pts;
                count = result.Count;
            }

            return new Tuple<List<Message>, List<Chat>, List<User>, List<MessageGroup>, int, int>
                (messages, chats, users, messageGroup, pts, count);
        }

        public async Task<bool> readHistory(InputChannel channel, int max_id)
        {
            var request = new TL.ChannelsReadHistoryRequest(channel, max_id);

            await _sender.Send(request);
            await _sender.Receive(request);

            return request.Result;
        }

        public async Task<Tuple<int, int>> deleteMessages(InputChannel channel, List<int> id)
        {
            var request = new TL.ChannelsDeleteMessagesRequest(channel, id);

            await _sender.Send(request);
            await _sender.Receive(request);

            var result = (TL.MessagesAffectedMessagesType)request.Result;

            return new Tuple<int, int>(result.Pts, result.PtsCount);
        }

        public async Task<Tuple<int, int>> deleteUserHistory(InputChannel channel, InputUser user_id)
        {
            var request = new TL.ChannelsDeleteUserHistoryRequest(channel, user_id);

            await _sender.Send(request);
            await _sender.Receive(request);

            var result = (TL.MessagesAffectedHistoryType)request.Result;

            return new Tuple<int, int>(result.Pts, result.PtsCount);
        }

        public async Task<bool> reportSpam(InputChannel channel, InputUser user_id, List<int> id)
        {
            var request = new TL.ChannelsReportSpamRequest(channel, user_id, id);

            await _sender.Send(request);
            await _sender.Receive(request);

            return request.Result;
        }

        public async Task<Tuple<List<Message>, List<Chat>, List<User>, List<MessageGroup>, int, int>> getMessages(InputChannel channel, List<int> id)
        {
            var request = new TL.ChannelsGetMessagesRequest(channel, id);

            await _sender.Send(request);
            await _sender.Receive(request);

            if (request.Result is TL.MessagesMessagesType)
            {
                var result = (TL.MessagesMessagesType)request.Result;
                messages = result.Messages;
                chats = result.Chats;
                users = result.Users;
            }
            else if (request.Result is TL.MessagesMessagesSliceType)
            {
                var result = (TL.MessagesMessagesSliceType)request.Result;
                messages = result.Messages;
                chats = result.Chats;
                users = result.Users;
            }
            else
            {
                var result = (TL.MessagesChannelMessagesType)request.Result;
                messages = result.Messages;
                chats = result.Chats;
                users = result.Users;
                messageGroup = result.Collapsed;
                pts = result.Pts;
                count = result.Count;
            }

            return new Tuple<List<Message>, List<Chat>, List<User>, List<MessageGroup>, int, int>
                (messages, chats, users, messageGroup, pts, count);
        }

        public async Task<Tuple<List<User>, List<ChannelParticipant>>> 
            getParticipants(InputChannel channel, ChannelParticipantsFilter filter, int offset, int limit)
        {
            var request = new TL.ChannelsGetParticipantsRequest(channel, filter, offset, limit);

            await _sender.Send(request);
            await _sender.Receive(request);

            var result = (TL.ChannelsChannelParticipantsType)request.Result;

            return new Tuple<List<User>, List<ChannelParticipant>>(result.Users, result.Participants);
        }

        public async Task<Tuple<List<User>, ChannelParticipant>> getParticipant(InputChannel channel, InputUser user_id)
        {
            var request = new TL.ChannelsGetParticipantRequest(channel, user_id);

            await _sender.Send(request);
            await _sender.Receive(request);

            var result = (TL.ChannelsChannelParticipantType)request.Result;

            return new Tuple<List<User>, ChannelParticipant>(result.Users, result.Participant);
        }

        public async Task<List<Chat>> getChannels(List<InputChannel> id)
        {
            var request = new TL.ChannelsGetChannelsRequest(id);

            await _sender.Send(request);
            await _sender.Receive(request);

            var result = (TL.MessagesChatsType)request.Result;

            return result.Chats;
        }

        public async Task<Tuple<ChatFull, List<Chat>, List<User>>> getFullChannel(InputChannel channel)
        {
            var request = new TL.ChannelsGetFullChannelRequest(channel);

            await _sender.Send(request);
            await _sender.Receive(request);

            var result = (TL.MessagesChatFullType)request.Result;

            return new Tuple<ChatFull, List<Chat>, List<User>>(result.FullChat, result.Chats, result.Users);
        }

        public async Task<Updates> createChannel(True broadcast, True megagroup, string title, string about)
        {
            var request = new TL.ChannelsCreateChannelRequest(broadcast, megagroup, title, about);

            await _sender.Send(request);
            await _sender.Receive(request);

            return request.Result;
        }

        public async Task<bool> editAbout(InputChannel channel, string about)
        {
            var request = new TL.ChannelsEditAboutRequest(channel, about);

            await _sender.Send(request);
            await _sender.Receive(request);

            return request.Result;
        }

        public async Task<Updates> editAdmin(InputChannel channel, InputUser user_id, ChannelParticipantRole role)
        {
            var request = new TL.ChannelsEditAdminRequest(channel, user_id, role);

            await _sender.Send(request);
            await _sender.Receive(request);

            return request.Result;
        }

        public async Task<Updates> editTitle(InputChannel channel, string title)
        {
            var request = new TL.ChannelsEditTitleRequest(channel, title);

            await _sender.Send(request);
            await _sender.Receive(request);

            return request.Result;
        }

        public async Task<Updates> editPhoto(InputChannel channel,InputChatPhoto photo)
        {
            var request = new TL.ChannelsEditPhotoRequest(channel, photo);

            await _sender.Send(request);
            await _sender.Receive(request);

            return request.Result;
        }

        public async Task<Updates> toggleComments(InputChannel channel, bool enabled)
        {
            var request = new TL.ChannelsToggleCommentsRequest(channel, enabled);

            await _sender.Send(request);
            await _sender.Receive(request);

            return request.Result;
        }

        public async Task<bool> checkUsername(InputChannel channel, string username)
        {
            var request = new TL.ChannelsCheckUsernameRequest(channel, username);

            await _sender.Send(request);
            await _sender.Receive(request);

            return request.Result;
        }

        public async Task<bool> updateUsername(InputChannel channel, string username)
        {
            var request = new TL.ChannelsUpdateUsernameRequest(channel, username);

            await _sender.Send(request);
            await _sender.Receive(request);

            return request.Result;
        }

        public async Task<Updates> joinChannel(InputChannel channel)
        {
            var request = new TL.ChannelsJoinChannelRequest(channel);

            await _sender.Send(request);
            await _sender.Receive(request);

            return request.Result;
        }

        public async Task<Updates> leaveChannel(InputChannel channel)
        {
            var request = new TL.ChannelsLeaveChannelRequest(channel);

            await _sender.Send(request);
            await _sender.Receive(request);

            return request.Result;
        }


        public async Task<Updates> inviteToChannel(InputChannel channel, List<InputUser> users)
        {
            var request = new TL.ChannelsInviteToChannelRequest(channel, users);

            await _sender.Send(request);
            await _sender.Receive(request);

            return request.Result;
        }

        public async Task<Updates> kickFromChannel(InputChannel channel, InputUser user_id, bool kicked)
        {
            var request = new TL.ChannelsKickFromChannelRequest(channel, user_id, kicked);

            await _sender.Send(request);
            await _sender.Receive(request);

            return request.Result;
        }

        public async Task<ExportedChatInvite> exportInvite(InputChannel channel)
        {
            var request = new TL.ChannelsExportInviteRequest(channel);

            await _sender.Send(request);
            await _sender.Receive(request);

            return request.Result;
        }

        public async Task<Updates> deleteChannel(InputChannel channel)
        {
            var request = new TL.ChannelsDeleteChannelRequest(channel);

            await _sender.Send(request);
            await _sender.Receive(request);

            return request.Result;
        }

        public async Task<Updates> toggleInvites(InputChannel channel, bool enabled)
        {
            var request = new TL.ChannelsToggleInvitesRequest(channel, enabled);

            await _sender.Send(request);
            await _sender.Receive(request);

            return request.Result;
        }

        public async Task<ExportedMessageLink> exportMessageLink(InputChannel channel, int id)
        {
            var request = new TL.ChannelsExportMessageLinkRequest(channel, id);

            await _sender.Send(request);
            await _sender.Receive(request);

            return request.Result;
        }

        public async Task<Updates> toggleSignatures(InputChannel channel, bool enabled)
        {
            var request = new TL.ChannelsToggleSignaturesRequest(channel, enabled);

            await _sender.Send(request);
            await _sender.Receive(request);

            return request.Result;
        }

        public async Task<Updates> updatePinnedMessage(InputChannel channel, True silent, int id)
        {
            var request = new TL.ChannelsUpdatePinnedMessageRequest(silent, channel, id);

            await _sender.Send(request);
            await _sender.Receive(request);

            return request.Result;
        }
    }
}
