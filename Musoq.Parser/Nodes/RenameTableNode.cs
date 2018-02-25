﻿using System;

namespace Musoq.Parser.Nodes
{
    public class RenameTableNode : Node
    {
        public RenameTableNode(string tableSourceName, string tableDestinationName)
        {
            TableSourceName = tableSourceName;
            TableDestinationName = tableDestinationName;
        }

        public string TableSourceName { get; }

        public string TableDestinationName { get; }

        public override Type ReturnType => typeof(void);

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string Id => $"{nameof(RenameTableNode)}{TableSourceName}{TableDestinationName}";

        public override string ToString()
        {
            return $"RENAME {TableSourceName} AS {TableDestinationName}";
        }
    }
}