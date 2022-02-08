using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using NaviServer.Code.ControllerPOD;
using NaviServer.Data;
using NaviServer.Models;

namespace NaviServer.Code.ControllerLogic
{
    public static class ShipLogic
    {
        public static bool MoveShip(ShipMovement shipMovement)
        {
            using var db = new NaviContext();

            Ship ship = db.Ship.Find(new object[] { shipMovement.ShipID });
            if (ship == null)
                return false;

            Coordinates targetCoords = new Coordinates()
            {
                PosX = shipMovement.TargetX,
                PosY = shipMovement.TargetY
            };
            db.Coordinates.Add(targetCoords);
            db.Movement.Add(new Movement()
            {
                ShipId = shipMovement.ShipID,
                CoordinatesFromId = ship.CoordinatesId,
                CoordinatesTo = targetCoords
            });

            db.SaveChanges();
            return true;
        }

        public static Vector2? GetLocation(uint shipID)
        {
            Vector2? result;

            using var db = new NaviContext();
            Ship ship = db.Ship.Find(new object[] { shipID });
            if (ship == null)
                return null;
            result = new Vector2(ship.Coordinates.PosX, ship.Coordinates.PosY);

            Movement movement = ship.Movement;
            if (movement != null)
            {
                Vector2 movementFrom = new Vector2(movement.CoordinatesFrom.PosX, movement.CoordinatesFrom.PosY);
                Vector2 movementTo = new Vector2(movement.CoordinatesTo.PosX, movement.CoordinatesTo.PosY);
                Vector2 movementVector = (movementTo - movementFrom) * movement.Progress;
                Vector2 currentLocation = movementFrom + movementVector;
                result = currentLocation;
            }

            return result;
        }
    }
}
