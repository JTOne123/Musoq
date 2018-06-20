﻿using System;

namespace Musoq.Service.Client.Core
{
    public class ResultTable
    {
        public ResultTable(string name, string[] columns, object[][] rows, string[] errors, TimeSpan computationTime)
        {
            Name = name;
            Columns = columns;
            Rows = rows;
            ComputationTime = computationTime;
        }

        public string[] Columns { get; }

        public object[][] Rows { get; }

        public string[] Errors { get; }

        public string Name { get; }

        public TimeSpan ComputationTime { get; }
    }
}