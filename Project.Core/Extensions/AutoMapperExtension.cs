using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Extensions
{
    public static class AutoMapperExtension
    {
        public static void IgnoreNullSourceProperties<TSource, TDestination>(this IMappingExpression<TSource, TDestination> mapping)
        {
            mapping.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
