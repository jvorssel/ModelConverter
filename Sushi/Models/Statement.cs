﻿using System.Collections.Generic;
using Sushi.Common.Enum;

namespace Sushi.Models
{
    public sealed class Statement
    {
        public string Script { get; }
        public StatementType Type { get; }
        public bool IsComment { get; }
        public bool IsEmptyLine { get; }

        public bool IsEmpty => Script == string.Empty && !IsEmptyLine;

        public IEnumerable<string> Lines => Script.Split('\n');


        public Statement(string script, StatementType type, bool isComment = false, bool isEmptyLine = false)
        {
            Script = script;
            Type = type;
            IsComment = isComment;
            IsEmptyLine = isEmptyLine;
        }

        #region Overrides of Object

        /// <inheritdoc />
        public override string ToString() => Script;

        #endregion
    }
}