﻿namespace Attribinter.Parameters.Representations.IndexedTypeParameterRepresentationFactoryCases.TypeParameterRepresentationCases;

using Xunit;

public sealed class IsNameKnown
{
    private static bool Target(IRepresentationFixture fixture) => fixture.Sut.IsNameKnown;

    [Fact]
    public void ReturnsFalse()
    {
        var fixture = RepresentationFixtureFactory.Create(0);

        var resuöt = Target(fixture);

        Assert.False(resuöt);
    }
}
