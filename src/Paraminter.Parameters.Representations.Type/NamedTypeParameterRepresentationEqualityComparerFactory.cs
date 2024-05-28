﻿namespace Paraminter.Parameters.Representations;

using System;
using System.Collections.Generic;

/// <inheritdoc cref="INamedTypeParameterRepresentationEqualityComparerFactory"/>
public sealed class NamedTypeParameterRepresentationEqualityComparerFactory
    : INamedTypeParameterRepresentationEqualityComparerFactory
{
    /// <summary>Instantiates a <see cref="NamedTypeParameterRepresentationEqualityComparerFactory"/>, handling creation of comparers of <see cref="ITypeParameterRepresentation"/> which consider the names of type parameter representations.</summary>
    public NamedTypeParameterRepresentationEqualityComparerFactory() { }

    IEqualityComparer<ITypeParameterRepresentation> INamedTypeParameterRepresentationEqualityComparerFactory.Create(
        IEqualityComparer<string> nameComparer)
    {
        if (nameComparer is null)
        {
            throw new ArgumentNullException(nameof(nameComparer));
        }

        return new TypeParameterRepresentationEqualityComparer(nameComparer);
    }

    private sealed class TypeParameterRepresentationEqualityComparer
        : IEqualityComparer<ITypeParameterRepresentation>
    {
        private readonly IEqualityComparer<string> NameComparer;

        public TypeParameterRepresentationEqualityComparer(
            IEqualityComparer<string> nameComparer)
        {
            NameComparer = nameComparer;
        }

        bool IEqualityComparer<ITypeParameterRepresentation>.Equals(
            ITypeParameterRepresentation x,
            ITypeParameterRepresentation y)
        {
            if (x is null)
            {
                throw new ArgumentNullException(nameof(x));
            }

            if (y is null)
            {
                throw new ArgumentNullException(nameof(y));
            }

            ValidateRepresentation(x, nameof(x));
            ValidateRepresentation(y, nameof(y));

            return NameComparer.Equals(x.GetName(), y.GetName());
        }

        int IEqualityComparer<ITypeParameterRepresentation>.GetHashCode(
            ITypeParameterRepresentation obj)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            ValidateRepresentation(obj, nameof(obj));

            return NameComparer.GetHashCode(obj.GetName());
        }

        private static void ValidateRepresentation(
            ITypeParameterRepresentation representation,
            string paramName)
        {
            if (representation.IsNameKnown is false)
            {
                throw new ArgumentException("Expected the name of the represented type parameter to be known.", paramName);
            }
        }
    }
}
