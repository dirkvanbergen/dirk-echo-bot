// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace Microsoft.BotBuilderSamples.Bots
{
    public class EchoBot : ActivityHandler
    {
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var userInfo = turnContext.Activity.Entities?.FirstOrDefault(entity => entity.Type.Equals("UserInfo", StringComparison.Ordinal));
            var replyText = "";
            if (userInfo != null)
            {
                var email = userInfo.Properties.Value<string>("UserEmail");

                if (!string.IsNullOrEmpty(email))
                {
                    //Do something with the user's email address.
                }

                replyText = $"Hoi {email}, you said: {turnContext.Activity.Text}";
            }
            else
            {
                replyText = $"I don't know who you are but you said: {turnContext.Activity.Text}";
            }
            await turnContext.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken);
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "Hello and welcome!";
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                }
            }
        }
    }
}
