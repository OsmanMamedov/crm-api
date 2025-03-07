﻿using System;
using System.Collections.Generic;
using System.Text;

namespace General.Core.Concerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        void Add(string key, object data, int cacheTime);
        bool IsAdd(string key);
        void Remove(string key);
        void RemovePattern(string pattern);
        void Clear();
    }
}
