using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Owin.Logging;
using ServicesAbstraction.Twilio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Services.Twilio
{
    public class TwilioServices : ISmsService
    {
        private readonly TwilioSetting _settings;
        private readonly ILogger<TwilioServices> _logger;

        public TwilioServices(IOptions<TwilioSetting> settings,ILogger<TwilioServices> logger)
        {
            _settings = settings.Value;
            TwilioClient.Init(_settings.AccountSid, _settings.AuthToken);
            _logger = logger;
        }
        public async Task SendSmsAsync(string phoneNumber, string message)
        {
            var msg = await MessageResource.CreateAsync(
            to: new PhoneNumber(phoneNumber),
            from: new PhoneNumber(_settings.phonenumber),
            body: message
        );
            _logger.LogInformation($"📩 SMS sent to {phoneNumber}: {msg.Sid}");
        }
    }
}
