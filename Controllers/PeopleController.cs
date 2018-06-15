using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Neo4j.Driver.V1;
using Roomies;

[Route("api/[controller]")]
public class PeopleController : Controller
{
    public GraphDao graphDao = new GraphDao();

    [HttpGet]
    public IActionResult Get()
    {
        Func<ITransaction, List<Person>> workToDo = (ITransaction tx) =>
        {
            var cReturn = tx.Run("MATCH (p:Person) RETURN p ");
            var returnedNodes = cReturn.Select(x =>
                new Dictionary<string, IEntity>()
                { 
                    { "person", x["p"].As<INode>() },
                });
            var peopleToReturn = new List<Person>();
            foreach (var returnedNodeDict in returnedNodes)
            {
                var personNode = returnedNodeDict["person"] as INode;
                var personToReturn = new Person()
                {
                    Name = personNode.Properties.ContainsKey("name") ? personNode["name"] as string : "No Name Found :("
                };
                peopleToReturn.Add(personToReturn);
            }

            return peopleToReturn;
        };

        return Ok(graphDao.DoWorkOnDb(workToDo));
    }

    [HttpGet("{name}/bills")]
    public IActionResult GetBillsForPerson(string name)
    {
        Func<ITransaction, List<Bill>> workToDo = (ITransaction tx) =>
        {
            var cReturn = tx.Run("MATCH (p:Person {name: $name})-[:Owes]-(b:Bill) RETURN b ", new {name = name});
            var returnedNodes = cReturn.Select(x =>
                new Dictionary<string, IEntity>()
                { 
                    { "bill", x["b"].As<INode>() },
                });
            var billsToReturn = new List<Bill>();
            foreach (var returnedNodeDict in returnedNodes)
            {
                var billNode = returnedNodeDict["bill"] as INode;
                var billToReturn = new Bill()
                {
                    Amount = billNode.Properties.ContainsKey("amount") ? Convert.ToDecimal(billNode["amount"]) : 0.0m,
                    Date = billNode.Properties.ContainsKey("date") ? DateTime.Parse(billNode["date"] as string) : DateTime.MinValue
                };
                billsToReturn.Add(billToReturn);
            }

            return billsToReturn;
        };

        return Ok(graphDao.DoWorkOnDb(workToDo));
    }
}