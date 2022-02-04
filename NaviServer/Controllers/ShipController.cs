using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NaviServer.Data;
using NaviServer.Models;
using Newtonsoft.Json.Linq;
using NaviServer.Code.ControllerPOD;
using NaviServer.Code.ControllerLogic;

namespace NaviServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipController : ControllerBase
    {
        [HttpPost]
        [Route("MoveShip")]
        public bool MoveShip(ShipMovement shipMovement)
        {
            return ShipLogic.MoveShip(shipMovement);
        }
    }
}
