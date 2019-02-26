using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Dapper;
using Microsoft.AspNetCore.Mvc;

using Storefront.web.Data;
using Storefront.web.Data.Model;

namespace Storefront.web.Controllers
{
    public class WrecksController : Controller
    {
        private readonly IDatabase _db;

        public WrecksController(IDatabase db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index(CancellationToken token)
        {
            // Fetch list of wrecks
            string sql = @"SELECT * FROM dbo.Wreck ORDER BY [Name]";
            var wrecks = await (await _db.GetConnectionAsync()).QueryAsync<Wreck>(sql);

            return View("List", wrecks);
        }

        public async Task<IActionResult> Details(int id, CancellationToken token)
        {
            string sql = @"SELECT * FROM dbo.Wreck WHERE Id = @id";
            var wreck = (await (await _db.GetConnectionAsync()).QueryAsync<Wreck>(sql, new { id })).SingleOrDefault();
            return View(wreck);
        }
    }
}