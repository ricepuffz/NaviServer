using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Numerics;
using NaviServer.Models;
using NaviServer.Data;

namespace NaviServer.Code.Game
{
    public static class MovementTicker
    {
        public static void Tick()
        {
            using var db = new NaviContext();

            List<uint> movementIDs = new List<uint>();
            foreach (Movement movement in db.Movement)
                movementIDs.Add(movement.MovementId);
            
            foreach (uint movementID in movementIDs)
            {
                Movement movement = db.Movement.Find(new object[] { movementID });

                float moveDistance = Util.MovementDistance(movement);
                float shipSpeed = movement.Ship.Speed;

                if (moveDistance <= 0)
                    movement.Progress = 1;
                else
                {
                    float incrementedProgress = movement.Progress + (shipSpeed * (float)Ticker.SecondsSinceLastTick) / moveDistance;
                    movement.Progress = incrementedProgress >= 1 ? 1 : incrementedProgress;
                }
                
                if (movement.Progress >= 1)
                {
                    CompleteMovement(movement, db);
                }
            }

            db.SaveChanges();
        }

        private static void CompleteMovement(Movement movement, NaviContext db)
        {
            ApplyMovementCoordinates(movement);
            db.Movement.Remove(movement);
            db.Coordinates.Remove(movement.CoordinatesTo);
        }

        private static void ApplyMovementCoordinates(Movement movement)
        {
            Coordinates coordinates = movement.Ship.Coordinates;
            coordinates.PosX = movement.CoordinatesTo.PosX;
            coordinates.PosY = movement.CoordinatesTo.PosY;
        }
    }
}
