using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using NaviServer.Data;
using NaviServer.Models;
using Newtonsoft.Json.Linq;
using NaviServer.Code.ControllerPOD;
using NaviServer.Code.ControllerLogic;
using Microsoft.AspNetCore.Authorization;

namespace NaviServer.Controllers.Api
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ShipController : ControllerBase
    {
        [HttpPost]
        [Route("MoveShip")]
        public bool MoveShip(ShipMovement shipMovement)
        {
            string shipUsername;

            using (var db = new NaviContext())
            {
                Ship ship = db.Ship.Find(new object[] { shipMovement.ShipID });
                if (ship == null)
                    return false;
                shipUsername = ship.Player.Credentials.Username;
            }

            if (Request.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value != shipUsername)
                return false;

            return ShipLogic.MoveShip(shipMovement);
        }
    }
}
