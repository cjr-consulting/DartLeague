using System;
using System.Collections.Generic;
using System.Text;

namespace DartLeague.Common.Data
{
    public class Entity<T>
    {
        public T Id { get; protected set; }
    }
}
