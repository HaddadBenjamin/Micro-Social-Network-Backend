using System;
using System.Linq.Expressions;
using AutoMapper;

namespace DiabloII.Domain.Extensions
{
    public static class MappingExpressionsExtensions
    {
        public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>(this IMappingExpression<TSource, TDestination> map, Expression<Func<TDestination, object>> selector) =>
            map.ForMember(selector, config => config.Ignore());
    }
}