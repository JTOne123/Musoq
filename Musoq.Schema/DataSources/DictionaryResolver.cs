﻿using System.Collections.Generic;

namespace Musoq.Schema.DataSources
{
    public class DictionaryResolver : IObjectResolver
    {
        private readonly IDictionary<string, object> _entity;

        public DictionaryResolver(IDictionary<string, object> entity)
        {
            _entity = entity;
        }

        public object[] Contexts => new object[] { _entity };

        object IObjectResolver.this[string name] => _entity[name];

        object IObjectResolver.this[int index] => null;

        public bool HasColumn(string name)
        {
            return _entity.ContainsKey(name);
        }
    }
}