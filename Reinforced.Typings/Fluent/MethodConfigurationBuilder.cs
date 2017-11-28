﻿using System.Collections.Generic;
using System.Reflection;
using Reinforced.Typings.Attributes;
using Reinforced.Typings.Fluent.Interfaces;

namespace Reinforced.Typings.Fluent
{
    /// <summary>
    ///     Fluent configuration builder for exported methods
    /// </summary>
    public class MethodConfigurationBuilder : IMemberExportConfiguration<TsFunctionAttribute,MethodInfo>, IIgnorable,
        IDecoratorsAggregator, IOrderableMember, ISupportsInferring<MethodInfo>
    {
        internal MethodConfigurationBuilder(MethodInfo member)
        {
            Member = member;
            AttributePrototype = member.RetrieveOrCreateCustomAttribute<TsFunctionAttribute>();
            Decorators = new List<TsDecoratorAttribute>();
        }

        private TsFunctionAttribute AttributePrototype { get; set; }
        private List<TsDecoratorAttribute> Decorators { get; set; }

        List<TsDecoratorAttribute> IDecoratorsAggregator.Decorators
        {
            get { return Decorators; }
        }

        TsFunctionAttribute IAttributed<TsFunctionAttribute>.AttributePrototype
        {
            get { return AttributePrototype; }
        }

        bool IIgnorable.Ignore { get; set; }

        double IOrderableMember.MemberOrder
        {
            get { return AttributePrototype.Order; }
            set { AttributePrototype.Order = value; }
        }

        /// <summary>
        /// Exporting member
        /// </summary>
        public MethodInfo Member { get; private set; }

        /// <summary>
        /// Type inferers set instance
        /// </summary>
        public InlineTypeInferers<MethodInfo> TypeInferers { get { return AttributePrototype.TypeInferers; } }
    }
}