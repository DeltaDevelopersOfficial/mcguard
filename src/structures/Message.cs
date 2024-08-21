using McGuard.src.handlers;
using McGuard.src.structures.text;
namespace McGuard.src.structures
{
    internal struct Message
    {
        /// <summary>
        /// Content of the message
        /// </summary>
        public string Content { get; private set; }
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
            this.Content = content;
            this.Length = length;
            this.Color = color;
            this.Style = style;
            this.IsServerMessage = isServerMessage;
        }
    }
}
