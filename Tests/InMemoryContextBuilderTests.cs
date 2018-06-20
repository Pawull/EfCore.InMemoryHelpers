﻿using System;
using Xunit;
using Xunit.Abstractions;
using System.Linq;
using EfCore.InMemoryHelpers;

public class InMemoryContextBuilderTests : TestBase
{
    [Fact]
    public void GetInMemoryContext()
    {
        using (var context = InMemoryContextBuilder.Build<TestDataContext>())
        {
            var user = new TestEntity
            {
                Property = "prop"
            };
            context.Add(user);
            context.SaveChanges();
            var item = context.TestEntities.ToList();
            Assert.Single(item);
        }
    }

    [Fact]
    public void AssertIdIsReset()
    {
        using (var context = InMemoryContextBuilder.Build<TestDataContext>())
        {
            var user = new TestEntity
            {
                Property = "prop1"
            };
            context.Add(user);
            context.SaveChanges();
            var id = context.TestEntities.Single().Id;
            Assert.Equal(1, id);
        }

        using (var context = InMemoryContextBuilder.Build<TestDataContext>())
        {
            var user = new TestEntity
            {
                Property = "prop2"
            };
            context.Add(user);
            context.SaveChanges();
            var id = context.TestEntities.Single().Id;
            Assert.Equal(1, id);
        }
    }

    public InMemoryContextBuilderTests(ITestOutputHelper output) :
        base(output)
    {
    }
}