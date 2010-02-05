using System;
using System.Collections.Generic;
using System.Linq;

namespace Paperless
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<OUTPUT> Map<INPUT, OUTPUT>( this IEnumerable<INPUT> target, Func<INPUT, OUTPUT> mapper )
        {
            foreach ( var item in target )
                yield return mapper( item );
        }

        public static IEnumerable<T> ForEach<T>( this IEnumerable<T> target, Action<T> action )
        {
            foreach ( var item in target )
            {
                action( item );
                yield return item;
            }
        }

        public static IEnumerable<T> Flatten<T>( this IEnumerable<IEnumerable<T>> target )
        {
            foreach ( var item in target )
                foreach ( var subItem in item )
                    yield return subItem;
        }

        public static IEnumerable<T> Evaluate<T>( this IEnumerable<T> target )
        {
            return target.ToList();
        }
    }
}