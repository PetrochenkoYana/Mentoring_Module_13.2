using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Runtime.Serialization;

namespace Task.DB
{
    [Serializable]
    public partial class Product : ISerializable
    {
        protected Product(SerializationInfo info, StreamingContext context)
        { 
            ProductID = info.GetInt32(nameof(ProductID));
            ProductName = info.GetString(nameof(ProductName));
            SupplierID = info.GetInt32(nameof(SupplierID));
            CategoryID = info.GetInt32(nameof(CategoryID));
            QuantityPerUnit = info.GetString(nameof(QuantityPerUnit));
            ReorderLevel = info.GetInt16(nameof(ReorderLevel));
            Discontinued = info.GetBoolean(nameof(Discontinued));
            UnitPrice = info.GetDecimal(nameof(UnitPrice));
            UnitsInStock = info.GetInt16(nameof(UnitsInStock));
            UnitsOnOrder = info.GetInt16(nameof(UnitsOnOrder));
            Supplier = (Supplier)info.GetValue(nameof(Supplier), typeof(Supplier));
            Category = (Category)info.GetValue(nameof(Category), typeof(Category));
            Order_Details = (ICollection<Order_Detail>)info.GetValue(nameof(Order_Details), typeof(ICollection<Order_Detail>));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(ProductID), ProductID);
            info.AddValue(nameof(ProductName), ProductName);
            info.AddValue(nameof(SupplierID), SupplierID);
            info.AddValue(nameof(CategoryID), CategoryID);
            info.AddValue(nameof(QuantityPerUnit), QuantityPerUnit);
            info.AddValue(nameof(ReorderLevel), ReorderLevel);
            info.AddValue(nameof(Discontinued), Discontinued);
            info.AddValue(nameof(UnitPrice), UnitPrice);
            info.AddValue(nameof(UnitsInStock), UnitsInStock);
            info.AddValue(nameof(UnitsOnOrder), UnitsOnOrder);

            var t = (context.Context as IObjectContextAdapter).ObjectContext;
            t.LoadProperty(this, obj=>obj.Supplier);
            t.LoadProperty(this, obj => obj.Category);
            t.LoadProperty(this, obj => obj.Order_Details);

            info.AddValue(nameof(Supplier), Supplier, typeof(Supplier));
            info.AddValue(nameof(Category), Category, typeof(Category));
            info.AddValue(nameof(Order_Details), Order_Details, typeof(ICollection<Order_Detail>));
        }
    }
}