using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleTurk.Core.Auth;
using TeleTurk.Core.MTProto;
using TeleTurk.Core.MTProto.Crypto;
using TeleTurk.Core.Network;
using MD5 = System.Security.Cryptography.MD5;

namespace TeleTurk.Core
{
    public class TelegramClient
    {
        public MtProtoSender _sender;
        private AuthKey _key;
        private TcpTransport _transport;
        private Session _session;
        private List<DcOption> dcOptions;

        public User loggedUser { get { return _session.User; } }
        public Configuration config = new Configuration();

        public List<Chat> chats;
        public List<User> users;

        public TelegramClient(ISessionStore store, string sessionUserId)
        {
            if (config.apiId == 0)
                throw new InvalidOperationException("Your API_ID is invalid. Do a configuration first");

            if (string.IsNullOrEmpty(config.apiHash))
                throw new InvalidOperationException("Your API_HASH is invalid. Do a configuration first");

            _session = Session.TryLoadOrCreateNew(store, sessionUserId);
            _transport = new TcpTransport(_session.ServerAddress, _session.Port);
        }


        public async Task<bool> Connect(bool reconnect = false)
        {
            if (_session.AuthKey == null || reconnect)
            {
                var result = await Authenticator.DoAuthentication(_transport);
                _session.AuthKey = result.AuthKey;
                _session.TimeOffset = result.TimeOffset;
            }

            _sender = new MtProtoSender(_transport, _session);

            if (!reconnect)
            {
                var request = new TL.InvokeWithLayerRequest(config.currentLayer,
                    new TL.InitConnectionRequest(config.apiId, config.DeviceModel, config.SystemVersion, config.AppVersion, config.LangCode,
                        new TL.HelpGetConfigRequest()));

                await _sender.Send(request);
                await _sender.Receive(request);

                var result = (TL.ConfigType)request.Result;
                dcOptions = result.DcOptions;
            }

            return true;
        }

        private async Task ReconnectToDc(int dcId)
        {
            if (dcOptions == null || !dcOptions.Any())
                throw new InvalidOperationException($"Can't reconnect. Establish initial connection first.");

            var dc = (TL.DcOptionType)dcOptions.First(d => ((TL.DcOptionType)d).Id == dcId);

            _transport = new TcpTransport(dc.IpAddress, dc.Port);
            _session.ServerAddress = dc.IpAddress;
            _session.Port = dc.Port;

            await Connect(true);
        }

        public bool IsUserAuthorized()
        {
            return _session.User != null;
        }

        public async Task<bool> IsPhoneRegistered(string phoneNumber)
        {
            if (_sender == null)
                throw new InvalidOperationException("Not connected!");

            var authCheckPhoneRequest = new TL.AuthCheckPhoneRequest(phoneNumber);
            await _sender.Send(authCheckPhoneRequest);
            await _sender.Receive(authCheckPhoneRequest);

            var result = (TL.AuthCheckedPhoneType)authCheckPhoneRequest.Result;
            return result.PhoneRegistered;
        }

        public async Task<string> SendCodeRequest()
        {
            var completed = false;

            TL.AuthSendCodeRequest request = null;

            while (!completed)
            {
                request = new TL.AuthSendCodeRequest(null, config.phoneNumber, true, config.apiId, config.apiHash, config.LangCode);
                try
                {
                    await _sender.Send(request);
                    await _sender.Receive(request);

                    completed = true;
                }
                catch (InvalidOperationException ex)
                {
                    if (ex.Message.StartsWith("Your phone number registered to") && ex.Data["dcId"] != null)
                    {
                        await ReconnectToDc((int)ex.Data["dcId"]);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            // TODO handle other types (such as SMS)
            var result = (TL.AuthSentCodeType)request.Result;
            return result.PhoneCodeHash;
        }

        public async Task<User> MakeAuth(string phoneHash, string code)
        {
            var request = new TL.AuthSignInRequest(config.phoneNumber, phoneHash, code);
            await _sender.Send(request);
            await _sender.Receive(request);

            var result = (TL.AuthAuthorizationType)request.Result;
            _session.User = result.User;

            _session.Save();

            return result.User;
        }

        public async Task<List<User>> ImportContacts(params string[] phoneNumbers)
        {
            var contacts = new List<InputContact>(phoneNumbers.Length);
            foreach (var phone in phoneNumbers)
                contacts.Add(new TL.InputPhoneContactType(0, phone, "Test Name " + phone, string.Empty));

            var request = new TL.ContactsImportContactsRequest(contacts, false);
            await _sender.Send(request);
            await _sender.Receive(request);

            var result = (TL.ContactsImportedContactsType)request.Result;

            return result.Users;
        }

        public async Task<bool> SendMessage(User user, string message)
        {
            InputPeer peer;
            if (user is TL.UserType)
            {
                var userType = (TL.UserType)user;
                peer = new TL.InputPeerUserType(userType.Id, userType.AccessHash ?? 0);
            }
            else
                return false;

            var request = new TL.MessagesSendMessageRequest(null, null, null, null, peer, null, message, getRandomLong(), null, null);

            await _sender.Send(request);
            await _sender.Receive(request);

            return true;
        }

        public async Task<UpdatesDifference> GetDifferenceUpdates(int pts, int date, int qts)
        {
            var request = new TL.UpdatesGetDifferenceRequest(pts, date, qts);

            await _sender.Send(request);
            await _sender.Receive(request);

            var result = (UpdatesDifference)request.Result;

            return result;
        }

        static readonly Random r = new Random();
        static long getRandomLong()
        {
            var buffer = new byte[sizeof(long)];
            r.NextBytes(buffer);
            return BitConverter.ToInt64(buffer, 0);
        }
    }
}
