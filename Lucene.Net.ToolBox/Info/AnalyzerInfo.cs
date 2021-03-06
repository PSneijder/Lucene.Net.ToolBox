﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Tokenattributes;
using Lucene.Net.Toolbox.Contracts;
using Version = Lucene.Net.Util.Version;

namespace Lucene.Net.Toolbox.Info
{
    sealed class AnalyzerInfo
        : IAnalyzer
    {
        public string Field => "DUMMYFIELD";

        public string Name { get; set; }
        public Type Type { get; set; }

        public Task<IEnumerable<IToken>> AnalyzeAsync(string text)
        {
            return Task.FromResult(Analyze(text));
        }

        private IEnumerable<IToken> Analyze(string text, Version version = Constants.IndexVersion)
        {
            var analyzer = CreateAnalyzer(version);

            if(analyzer == null)
            {
                return Enumerable.Empty<IToken>();
            }

            return CreateTokens(analyzer, text);
        }

        private Analyzer CreateAnalyzer(Version version)
        {
            Analyzer analyzer = null;
            var constructor = Type.GetConstructor(Type.EmptyTypes);

            if (constructor != null)
            {
                analyzer = (Analyzer) Activator.CreateInstance(Type);
            }
            else
            {
                constructor = Type.GetConstructor(new [] { typeof(Version) });
                if (constructor != null)
                {
                    analyzer = (Analyzer) Activator.CreateInstance(Type, version);
                }
            }

            return analyzer;
        }

        private IEnumerable<IToken> CreateTokens(Analyzer analyzer, string text)
        {
            StringReader reader = new StringReader(text);
            TokenStream tokenStream = analyzer.TokenStream(Field, reader);

            var term = tokenStream.AddAttribute<ITermAttribute>();
            var flag = tokenStream.AddAttribute<IFlagsAttribute>();
            var offset = tokenStream.AddAttribute<IOffsetAttribute>();
            var positionIncrement = tokenStream.AddAttribute<IPositionIncrementAttribute>();
            var type = tokenStream.AddAttribute<ITypeAttribute>();
            var payload = tokenStream.AddAttribute<IPayloadAttribute>();

            var tokens = new List<IToken>();
            while (tokenStream.IncrementToken())
            {
                tokens.Add(new TokenInfo
                {
                    Term = term.Term,
                    Type = type.Type,
                    StartOffset = offset.StartOffset,
                    EndOffset = offset.EndOffset,
                    Position = positionIncrement.PositionIncrement,
                    Payload = payload.Payload?.GetType()
                });
            }

            return tokens;
        }
    }
}