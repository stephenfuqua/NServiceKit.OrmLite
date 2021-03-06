﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NServiceKit.OrmLite.SqlServerTests
{
    /// <summary>An insert parameter get last insert identifier.</summary>
    public class InsertParam_GetLastInsertId : OrmLiteTestBase
    {
        /// <summary>Can get last inserted identifier using insert parameter.</summary>
        [Test]
        public void Can_GetLastInsertedId_using_InsertParam()
        {
            var testObject = new SimpleType { Name = "test" };

            //verify that "normal" Insert works as expected
            using (var con = OpenDbConnection())
            {
                con.CreateTable<SimpleType>(true);

                con.Insert(testObject);
                var normalLastInsertedId = con.GetLastInsertId();
                Assert.Greater(normalLastInsertedId, 0, "normal Insert");
            }

            //test with InsertParam
            using (var con = OpenDbConnection())
            {
                con.CreateTable<SimpleType>(true);

                var lastInsertId = con.InsertParam(testObject, selectIdentity:true);
                Assert.Greater(lastInsertId, 0, "with InsertParam");
            }
        }
    }
}
