using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using privplan_api.Models;
using privplan_api.Wrappers;
using privplan_api.ConfigClasses;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Pomelo.EntityFrameworkCore.MySql.Storage.Internal;
using System.Security.Cryptography;
using System.Text;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace privplan_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllowedAccountsController : ControllerBase
    {
        private readonly PrivPlanContext _context;

        private string _serverConfigPath = String.Empty;
        public AllowedAccountsController(PrivPlanContext context)
        {
            if(System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                //TODO: Decide where the server config file will be in windows
            }
            else if(System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                _serverConfigPath = $"/home/{Environment.UserName}/.config/privplan/server/config.json";
                
            }
            else //TODO: Add support for macos and freebsd
                Console.WriteLine("Operating system not recognized or not yet supported");

            _context = context;
        }

        // GET: api/AllowedAccounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AllowedAccounts>>> GetAllowedDevices()
        {
            return await _context.AllowedAccounts.ToListAsync();
        }

        // GET: api/AllowedAccounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AllowedAccounts>> GetAllowedAccounts(int id)
        {
            var allowedAccounts = await _context.AllowedAccounts.FindAsync(id);

            if (allowedAccounts == null)
            {
                return NotFound();
            }

            return allowedAccounts;
        }

        // PUT: api/AllowedAccounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAllowedAccounts(int id, AllowedAccounts allowedAccounts)
        {
            if (id != allowedAccounts.Id)
            {
                return BadRequest();
            }

            _context.Entry(allowedAccounts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AllowedAccountsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private byte[] Hasher(string toHash)
        {
            using(HashAlgorithm algo = SHA256.Create())
                return algo.ComputeHash(Encoding.UTF8.GetBytes(toHash));
        }

        private async Task<bool> Validate(string enteredPassword)
        {
            try
            {
                StreamReader reader = new StreamReader(_serverConfigPath);
                var json = await reader.ReadToEndAsync();
                List<ServerConfig>? serverConfig = JsonConvert.DeserializeObject<List<ServerConfig>>(json);
                
                if(serverConfig != null)
                {
                    StringBuilder passwdEnteredHashed = new StringBuilder();
                    foreach(byte b in Hasher(enteredPassword + serverConfig[0].Salt))
                    {
                        passwdEnteredHashed.Append(b.ToString("X2"));
                    }

                    if(passwdEnteredHashed.ToString() == serverConfig[0].HashedPassword)
                    {
                        return true;
                    }            
                }
                else
                {
                    Console.WriteLine("««««««««««««««ERROR«««««««««««««««««««««");
                    Console.WriteLine("     There is no valid config file");
                    Console.WriteLine("««««««««««««««««««««««««««««««««««««««««");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("««««««««««««««ERROR«««««««««««««««««««««");
                Console.WriteLine(ex.Message);
                Console.WriteLine("««««««««««««««««««««««««««««««««««««««««");
            }
            return false;
        }
        // POST: api/AllowedAccounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AllowedAccounts>> PostAllowedAccounts(AllowedAccountsWrapper allowedAccountsWrapper)
        {
            // TODO: Implement authentication

            bool isValid = await Validate(allowedAccountsWrapper.Password);
            if(isValid)
            {
                _context.AllowedAccounts.Add(allowedAccountsWrapper.Request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAllowedAccounts", new { id = allowedAccountsWrapper.Request.Id }, allowedAccountsWrapper.Request);
            }
            else
            {
                return Unauthorized(new {error = "Wrong password or blocked account"});
            }
            
        }

        // DELETE: api/AllowedAccounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAllowedAccounts(int id)
        {
            var allowedAccounts = await _context.AllowedAccounts.FindAsync(id);
            if (allowedAccounts == null)
            {
                return NotFound();
            }

            _context.AllowedAccounts.Remove(allowedAccounts);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AllowedAccountsExists(int id)
        {
            return _context.AllowedAccounts.Any(e => e.Id == id);
        }
    }
}
