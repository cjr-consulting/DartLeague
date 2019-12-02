using System;

namespace DartLeague.Common.Messaging
{
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQuery
    {
        TResult Handle(TQuery query);
    }

}
