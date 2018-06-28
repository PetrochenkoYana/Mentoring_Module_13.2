using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task.DB;
using Task.TestHelpers;
using System.Runtime.Serialization;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using Task.Surrogates;

namespace Task
{
    [TestClass]
    public class SerializationSolutions
    {
        Northwind dbContext;

        [TestInitialize]
        public void Initialize()
        {
            dbContext = new Northwind();
        }

        [TestMethod]
        public void SerializationCallbacks()
        {
            dbContext.Configuration.ProxyCreationEnabled = false;

            var tester = new XmlDataContractSerializerTester<IEnumerable<Category>>(new NetDataContractSerializer(new StreamingContext(StreamingContextStates.All, dbContext)), true);
            var categories = dbContext.Categories.ToList();

            var c = categories.First();

            tester.SerializeAndDeserialize(categories);
        }

        [TestMethod]
        public void ISerializable()
        {
            dbContext.Configuration.ProxyCreationEnabled = false;

            var tester = new XmlDataContractSerializerTester<IEnumerable<Product>>(new NetDataContractSerializer(new StreamingContext(StreamingContextStates.All, dbContext)), true);
            var products = dbContext.Products.ToList();

            tester.SerializeAndDeserialize(products);
        }


        [TestMethod]
        public void ISerializationSurrogate()
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            var selector = new SurrogateSelector();

            selector.AddSurrogate(type: typeof(Order_Detail),
              context: new StreamingContext(StreamingContextStates.All, dbContext),
              surrogate: new Order_DetailsSerializationSurrogate()
            );
            var serializer = new NetDataContractSerializer();
            serializer.SurrogateSelector = selector;
            var tester = new XmlDataContractSerializerTester<IEnumerable<Order_Detail>>(serializer, true);
            var orderDetails = dbContext.Order_Details.ToList();

            tester.SerializeAndDeserialize(orderDetails);
        }
    }
}
