using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catalog1.Models;
using System.Linq;
using Catalog1.Controllers;
using System.Web.Mvc;
using System.Collections.Generic;

namespace CatalogTest
{
    [TestClass]
    public class ProductTest
    {
        TestContext context;

        [TestInitialize]
        public void CreateDefaultProductContext()
        {
            context = new TestContext();

            context.Products.Add(
                new Product
                {
                    ProductID = 1,
                    Name = "Widget",
                    Price = 45m,
                    Quantity = 20
                }
            );
        }
        [TestMethod]
        public void ProductInventoryLowShouldEmail()
        {
            // Arrange   using System.Linq;
            context.Products.First().Quantity = 5;

            var repository = new Repository(context);

            // Act
            var cart = repository.FindOrCreateCart("mtwohey2@yahoo.com");

            repository.AddShoppingCart(1, 1, cart);

            // Assert
            Assert.AreEqual(1, context.Emails.Count());
        }
        [TestMethod]
        public void ProductControllerDetails()
        {
            // Arrange
            var repository = new Repository(context);

            ProductController prodcontroller =
                new ProductController(repository);

            // Act
            ViewResult result = prodcontroller.Detail(1) as ViewResult;

            // Assert
            Assert.AreEqual("Widget", ((Product)result.Model).Name);
            Assert.AreEqual(45m, ((Product)result.Model).Price);
        }
        [TestMethod]
        public void ProductReviewAverage()
        {
            // Arrange
            var review1 = new ProductReview { Rating = 1 };
            var review2 = new ProductReview { Rating = 5 };

            var product = new Product { Reviews = new List<ProductReview>() };

            // Act
            product.Reviews.Add(review1);
            product.Reviews.Add(review2);

            // Assert
            Assert.AreEqual(3, product.AverageReview);
        }
        [TestMethod]
        public void ProductReviewAverageZeroReviews()
        {
            // Arrange
            var product = new Product { Reviews = new List<ProductReview>() };

            // Act

            // Assert
            Assert.AreEqual(0, product.AverageReview);
        }
        [TestMethod]
        public void ProductReturnAll()
        {
            // Arrange
            context.Products.Add(new Product
                {
                    Name = "Super Widget",
                    Price = 23m,
                    Quantity = 7,
                });

            var repository = new Repository(context);

            // Act
            var products = repository.GetAllProducts()
                .OrderBy(q => q.Name).ToList();

            // Assert
            Assert.AreEqual(2, products.Count());
            Assert.AreEqual("Super Widget", products[0].Name);
            Assert.AreEqual("Widget", products[1].Name);
        }
        [TestMethod]
        public void ProductPurchaseShouldEmail()
        {
            // Arrange
            TestContext context = new TestContext();

            context.Products.Add(new Product
                {
                    ProductID = 1,
                    Name = "Widget",
                    Price = 45m,
                    Quantity = 20,
                });
            var repository = new Repository(context);

            // Act
            var cart = repository.FindOrCreateCart("mtwohey2@yahoo.com");

            repository.AddShoppingCart(1, 1, cart);

            repository.PurchaseCart("mtwohey2@yahoo.com");

            // Assert
            Assert.AreEqual(1, context.Emails.Count());
        }
        
    }
}
