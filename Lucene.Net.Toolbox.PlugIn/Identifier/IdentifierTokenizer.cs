using System.IO;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Tokenattributes;
using Lucene.Net.Util;

namespace Lucene.Net.Toolbox.PlugIn.Identifier
{
    public sealed class IdentifierTokenizer
        : Tokenizer
    {
        private int _offset = 0, _bufferIndex = 0, _dataLen = 0;
        private const int MaxWordLen = 255;
        private const int IoBufferSize = 4096;
        private readonly char[] _ioBuffer = new char[IoBufferSize];

        private readonly ITermAttribute _termAtt;
        private readonly IOffsetAttribute _offsetAtt;

	    public IdentifierTokenizer(TextReader input)
            :base(input)
		{
			_offsetAtt = AddAttribute<IOffsetAttribute>();
            _termAtt = AddAttribute<ITermAttribute>();
		}

        public IdentifierTokenizer(AttributeSource source, TextReader input)
            : base(source, input)
		{
            _offsetAtt = AddAttribute<IOffsetAttribute>();
            _termAtt = AddAttribute<ITermAttribute>();
		}

        public IdentifierTokenizer(AttributeFactory factory, TextReader input)
            : base(factory, input)
		{
            _offsetAtt = AddAttribute<IOffsetAttribute>();
            _termAtt = AddAttribute<ITermAttribute>();
		}

        /// <summary>Returns true iff a character should be included in a token.  This
        /// tokenizer generates as tokens adjacent sequences of characters which
        /// satisfy this predicate.  Characters for which this is false are used to
        /// define token boundaries and are not included in tokens. 
        /// </summary>
        public bool IsTokenChar(char c)
        {
            return !char.IsWhiteSpace(c);
        }
		
		/// <summary>Called on each token character to normalize it before it is added to the
		/// token.  The default implementation does nothing.  Subclasses may use this
		/// to, e.g., lowercase tokens. 
		/// </summary>
		public char Normalize(char c)
		{
			return c;
		}
		
		public override bool IncrementToken()
		{
			ClearAttributes();
			int length = 0;
			int start = _bufferIndex;
			char[] buffer = _termAtt.TermBuffer();
			while (true)
			{
				
				if (_bufferIndex >= _dataLen)
				{
					_offset += _dataLen;
					_dataLen = input.Read(_ioBuffer, 0, _ioBuffer.Length);
					if (_dataLen <= 0)
					{
						_dataLen = 0; // so next offset += dataLen won't decrement offset
						if (length > 0)
							break;
						else
							return false;
					}
					_bufferIndex = 0;
				}
				
				char c = _ioBuffer[_bufferIndex++];
				
				if (IsTokenChar(c))
				{
					// if it's a token char
					
					if (length == 0)
					// start of token
						start = _offset + _bufferIndex - 1;
					else if (length == buffer.Length)
						buffer = _termAtt.ResizeTermBuffer(1 + length);
					
					buffer[length++] = Normalize(c); // buffer it, normalized
					
					if (length == MaxWordLen)
					// buffer overflow!
						break;
				}
				else if (length > 0)
				// at non-Letter w/ chars
					break; // return 'em
			}
			
			_termAtt.SetTermLength(length);
			_offsetAtt.SetOffset(CorrectOffset(start), CorrectOffset(start + length));
			return true;
		}
		
		public override void  End()
		{
			// set final offset
			int finalOffset = CorrectOffset(_offset);
			_offsetAtt.SetOffset(finalOffset, finalOffset);
		}
		
		public override void  Reset(TextReader input)
		{
			base.Reset(input);
			_bufferIndex = 0;
			_offset = 0;
			_dataLen = 0;
		}
    }
}