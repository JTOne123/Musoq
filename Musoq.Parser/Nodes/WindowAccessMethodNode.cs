using System;
using System.Linq;
using System.Text;

namespace Musoq.Parser.Nodes
{
    public class WindowAccessMethodNode : Node
    {
        public WindowAccessMethodNode(AccessMethodNode method, FieldNode[] partitionParticipants)
        {
            string partitionParticipantsId = string.Empty;
            if (partitionParticipants.Length > 0)
                partitionParticipantsId = partitionParticipants.Select(f => f.Id).Aggregate((a, b) => a + b);

            Id = $"{nameof(WindowAccessMethodNode)}{method.Id}{partitionParticipantsId}";

            Method = method;
            PartitionParticipants = partitionParticipants;
        }

        public override string Id { get; }

        public AccessMethodNode Method { get; }

        public FieldNode[] PartitionParticipants { get; }

        public override Type ReturnType => Method.ReturnType;

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            if (PartitionParticipants.Length > 0)
            {
                for (int i = 0; i < PartitionParticipants.Length - 1; i++)
                {
                    builder.Append(PartitionParticipants[i].ToString());
                    builder.Append(", ");
                }

                builder.Append(PartitionParticipants[PartitionParticipants.Length - 1].ToString());
            }

            return $"{Method.ToString()} OVER (PARTITION BY {builder.ToString()})";
        }
    }
}