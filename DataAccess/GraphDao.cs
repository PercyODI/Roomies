using System;
using System.Collections.Generic;
using System.Linq;
using Neo4j.Driver.V1;

namespace Roomies
{
    public class GraphDao
    {
        public T DoWorkOnDb<T>(Func<ITransaction, T> func)
        {
            using(IDriver _driver = GraphDatabase.Driver("bolt://ph-roomies-neo4j.southcentralus.cloudapp.azure.com",
                AuthTokens.Basic("phutson", "p44ca#%AL")))
            {
                using(var session = _driver.Session())
                {
                    return session.WriteTransaction(tx =>
                    {
                        return func(tx);
                    });
                }
            }

            throw new Exception("Shouldn't get here...");
        }
    }
}