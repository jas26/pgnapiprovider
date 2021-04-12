﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DynamoDBLibraries.DynamoDb;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PGN_Db_Operations.Commom;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PGN_Db_Operations
{
    [Route("pgn")]
    public class DynamoDBController : ControllerBase
    {
        public readonly IPostData _postData;
        public DynamoDBController(IPostData postData)
        {
            _postData = postData;
        }
        [HttpPost("post")]
        [Consumes("application/json")]
        public async Task<IActionResult> PostToDynamoDB([FromBody]PostModel payload)
        {
            //if(payload == null)
            //{
            //    return new ContentResult
            //    {
            //        Content = "Payload is empty",
            //        StatusCode = (int)HttpStatusCode.BadRequest
            //    };
            //}
            //Console.WriteLine(payload);
            JObject postbody = JObject.Parse(JsonConvert.SerializeObject(payload));
            await _postData.AddPost(postbody);
            Console.WriteLine(payload);
            return new JsonResult("created") { StatusCode = (int)HttpStatusCode.Created };
        }

    }
}
