using McGuard.src.handlers;
using McGuard.src.structures.text;
namespace McGuard.src.structures
{
    internal struct Message
    {
        /// <summary>
        /// Content of the message
        /// </summary>
        public string Content;

        /// <summary>
        /// Message length
        /// </summary>
        public long Length;

        /// <summary>
        /// Message color style
        /// </summary>
        public Color Color;

        /// <summary>
        /// Font style like bold, italic etc..
        /// </summary>
        public Style Style;

        public Message(string content, long length, Color color, Style style)
        {
            this.Content = content;
            this.Length = length;
            this.Color = color;
            this.Style = style;
        }
    }
}
