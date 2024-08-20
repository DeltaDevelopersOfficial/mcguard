using McGuard.src.handlers;
using McGuard.src.structures.text;
namespace McGuard.src.structures
{
    internal struct Message
    {
        /// <summary>
        /// Help variable for getter and setter
        /// </summary>
        private string _content;

        /// <summary>
        /// Content of the message
        /// </summary>
        public string Content
        {
            get => _content;
            private set => _content = value.Trim();
        }

        /// <summary>
        /// Message length
        /// </summary>
        public long Length { get; private set; }

        /// <summary>
        /// Message color style
        /// </summary>
        public Color Color { get; private set; }

        /// <summary>
        /// Font style like bold, italic etc..
        /// </summary>
        public Style Style { get; private set; }

        /// <summary>
        /// Identify if it is server message
        /// </summary>
        public bool IsServerMessage { get; private set; }

        public Message(string content, long length, Color color, Style style, bool isServerMessage)
        {
            _content = content?.Trim() ?? string.Empty;
            this.Length = length;
            this.Color = color;
            this.Style = style;
            this.IsServerMessage = isServerMessage;
        }
    }
}
