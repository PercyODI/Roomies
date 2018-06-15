using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Neo4j.Driver.V1;
using Roomies;

 [Route("api/[controller]")]
public class BillsController : Controller
{
    public GraphDao billDao = new GraphDao();

    [HttpGet]
    public IActionResult Get()
    {
        Func<ITransaction, List<Bill>> workToDo = (ITransaction tx) =>
        {
            var cReturn = tx.Run("MATCH (p:Person)-[r:Owes]-(b:Bill)-[i:Issued]-(c:Company) RETURN r, b, i ,c ");
            var returnedNodes = cReturn.Select(x =>
                new Dictionary<string, IEntity>()
                {
                    //				{"persons", x["p"].As<INode>()},
                    { "owes", x["r"].As<IRelationship>() }, { "bills", x["b"].As<INode>() }, { "issued", x["i"].As<IRelationship>() }, { "company", x["c"].As<INode>() }
                });
            var billsToReturn = new List<Bill>();
            foreach (var returnedNodeDict in returnedNodes)
            {
                var billNode = returnedNodeDict["bills"] as INode;
                var billToReturn = new Bill()
                {
                    Amount = billNode.Properties.ContainsKey("amount") ? Convert.ToDecimal(billNode["amount"]) : 0.0m,
                    Date = billNode.Properties.ContainsKey("date") ? DateTime.Parse(billNode["date"] as string) : DateTime.MinValue
                };
                billsToReturn.Add(billToReturn);
            }

            return billsToReturn;
        };
        
        return Ok(billDao.DoWorkOnDb(workToDo));
    }
}