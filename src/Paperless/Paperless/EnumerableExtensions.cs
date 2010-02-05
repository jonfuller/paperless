using System;
using System.Collections.Generic;

namespace Paperless
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TOutput> Map<TInput, TOutput>(this IEnumerable<TInput> target, Func<TInput, TOutput> mapper)
        {
            foreach (var item in target)
                yield return mapper(item);
        }
    }
}