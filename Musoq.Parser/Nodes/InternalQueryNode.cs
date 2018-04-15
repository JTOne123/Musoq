﻿using System;

namespace Musoq.Parser.Nodes
{
    public class InternalQueryNode : QueryNode
    {
        public InternalQueryNode(SelectNode select, FromNode from, WhereNode where, GroupByNode groupBy, OrderByNode orderBy, IntoNode into,
            ShouldBePresentInTheTable shouldBePresent, SkipNode skip, TakeNode take,  bool shouldLoadResultTableAsResult, string resultTable,
            bool useColumnAccessInstead, RefreshNode refresh)
            : base(select, from, null, where, groupBy, orderBy, skip, take)
        {
            UseColumnAccessInstead = useColumnAccessInstead;
            Refresh = refresh;
            Into = into;
            ShouldBePresent = shouldBePresent;
            ShouldLoadResultTableAsResult = shouldLoadResultTableAsResult;
            ResultTable = resultTable;
        }

        public bool UseColumnAccessInstead { get; }

        public IntoNode Into { get; }

        public ShouldBePresentInTheTable ShouldBePresent { get; }

        public RefreshNode Refresh { get; }

        public bool ShouldLoadResultTableAsResult { get; }

        public string ResultTable { get; }

        public override Type ReturnType => null;

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return
                $"{Select.ToString()} {From.ToString()} {Where.ToString()} {GroupBy?.ToString()} {OrderBy?.ToString()} {Into?.ToString()} {ShouldBePresent?.ToString()} {Skip?.ToString()} {Take?.ToString()}";
        }
    }
}