using FakerLib;
using System;
using System.Collections.Generic;
using Xunit;

namespace FakerTests
{
    public class Tests
    {
        [Fact]
        public void DefaultTypeTests()
        {
            Faker faker = new Faker();
            Assert.True(faker.Create<bool>());
            Assert.NotEqual(default(byte), faker.Create<byte>());
            Assert.NotEqual(default(char), faker.Create<char>());
            Assert.NotEqual(default(DateTime), faker.Create<DateTime>());
            Assert.NotEqual(default(decimal), faker.Create<decimal>());
            Assert.NotEqual(default(double), faker.Create<double>());
            Assert.NotEqual(default(float), faker.Create<float>());
            Assert.NotEqual(default(int), faker.Create<int>());
            Assert.NotEqual(default(long), faker.Create<long>());
            Assert.NotEqual(default(short), faker.Create<short>());
            Assert.NotEqual(default(string), faker.Create<string>());
            Assert.NotEqual(default(List<string>), faker.Create<List<string>>());
        }

        [Fact]
        public void ListTest()
        {
            Faker faker = new Faker();
            var list = faker.Create<List<List<List<int>>>>();
            Assert.InRange(list.Count, 1, 10);
            foreach (var item in list)
            {
                Assert.InRange(item.Count, 1, 10);
                foreach (var item2 in item)
                {
                    Assert.InRange(item2.Count, 1, 10);
                    foreach (var item3 in item2)
                        Assert.NotEqual(default(int), item3);
                }
            }
            Assert.NotEqual(default(int), list[0][0][0]);
        }

        [Fact]
        public void DateTimeTest()
        {
            Faker faker = new Faker();
            DateTime dateTime = faker.Create<DateTime>();
            Assert.InRange(dateTime.Year, 1, 2100);
            Assert.InRange(dateTime.Month, 1, 13);
            Assert.InRange(dateTime.Day, 1, 29);
            Assert.InRange(dateTime.Hour, 0, 24);
            Assert.InRange(dateTime.Minute, 0, 60);
            Assert.InRange(dateTime.Second, 0, 60);
            Assert.InRange(dateTime.Millisecond, 0, 1000);
        }

        [Fact]
        public void SimpleClassTest()
        {
            Faker faker = new Faker();
            SimpleUserClass simpleObject = faker.Create<SimpleUserClass>();
            Assert.NotEqual(ObjectCreator.GetDefaultValue(simpleObject.nullField.GetType()), simpleObject.nullField);
            Assert.Equal(-1, simpleObject.field);
            Assert.NotEqual(ObjectCreator.GetDefaultValue(simpleObject.nullProperty.GetType()), simpleObject.nullProperty);
            Assert.Equal(-2, simpleObject.property);
        }

        [Fact]
        public void PrivateTest()
        {
            Faker faker = new Faker();
            var userClass = faker.Create<SimpleUserClass>();
            Assert.Equal(default(int), userClass.GetPrivField());
            Assert.Equal(default(int), userClass.GetPrivProperty());
        }

        [Fact]
        public void ConstructorTest()
        {
            Faker faker = new Faker();
            MulticonstractorClass multiObj = faker.Create<MulticonstractorClass>();
            List<int> sums = multiObj.GetSum();
            Assert.Equal(default(int), sums[0]);
            Assert.Equal(default(int), sums[1]);
            Assert.Equal(default(int), sums[2]);
            Assert.NotEqual(default(int), sums[3]);
            Assert.Equal(default(int), sums[4]);
        }

        [Fact]
        public void RecursiveClassTest()
        {
            Faker faker = new Faker();
            RecursiveClass recursive = faker.Create<RecursiveClass>();
            Assert.NotNull(recursive);
            Assert.NotNull(recursive.recursive);
            Assert.NotEqual(default(int), recursive.number);
            if (recursive.recursive != null)
            {
                Assert.Null(recursive.recursive.recursive);
                Assert.NotEqual(default(int), recursive.recursive.number);
            }
        }

        [Fact]
        public void NoConstructorTest()
        {
            Faker faker = new Faker();
            var ex = Assert.Throws<FakerException>(() => faker.Create<IntrovertClass>());
            Assert.Contains("Can not create object of type", ex.Message);
        }

        [Fact]
        public void BrokenListTest()
        {
            Faker faker = new Faker();
            var ex = Assert.Throws<FakerException>(() => faker.Create<List<IntrovertClass>>());
            Assert.Contains("FakerException:", ex.Message);
        }

        [Fact]
        public void ÑodependencyTest()
        {
            Faker faker = new Faker();
            Ñodependency a = faker.Create<Ñodependency>();
            if (a != null && a.b != null && a.b.a != null)
            {
                Assert.NotNull(a.b.a.b);
                if (a.b.a.b != null) Assert.Null(a.b.a.b.a);
            }
            else Assert.Equal(1, 2);
        }
    }
}