﻿using Newtonsoft.Json;
using Telesharp.Common.Interfaces;

namespace Telesharp.Common.Types
{
    /// <summary>
    ///     This object represents a custom keyboard with reply options.
    /// </summary>
    public class ReplyKeyboardMarkup : IReplyMarkup
    {
        /// <summary>
        ///     Array of button rows, each represented by an Array of Strings
        /// </summary>
        [JsonProperty(PropertyName = "keyboard")]
        public string[][] Keyboard { get; set; } = {};

        /// <summary>
        ///     Requests clients to resize the keyboard vertically for optimal fit (e.g., make the keyboard smaller if there are
        ///     just two rows of buttons). Defaults to false, in which case the custom keyboard is always of the same height as the
        ///     app's standard keyboard.
        /// </summary>
        [JsonProperty(PropertyName = "resize_keyboard")]
        public bool ResizeKeyboard { get; set; }

        /// <summary>
        ///     Requests clients to hide the keyboard as soon as it's been used. Defaults to false.
        /// </summary>
        [JsonProperty(PropertyName = "one_time_keyboard")]
        public bool OneTimeKeyboard { get; set; }

        /// <summary>
        ///     Use this parameter if you want to show the keyboard to specific users only. Targets: 1) users that are @mentioned
        ///     in the text of the Message object; 2) if the bot's message is a reply (has reply_to_message_id), sender of the
        ///     original message.
        /// </summary>
        /// <example>
        ///     A user requests to change the bot‘s language, bot replies to the request with a keyboard to select the new
        ///     language. Other users in the group don’t see the keyboard.
        /// </example>
        [JsonProperty(PropertyName = "selective")]
        public bool Selective { get; set; }
    }
}