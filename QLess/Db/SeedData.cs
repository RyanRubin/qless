using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QLess.Models;

namespace QLess.Db
{
    public class SeedData
    {
        private readonly QLessDbContext dbContext;

        public SeedData(QLessDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void HandleSeeding()
        {
            if (dbContext.TransportLines.Count() > 0)
            {
                return;
            }

            if (dbContext.TransportStations.Count() > 0)
            {
                return;
            }

            if (dbContext.TransportFares.Count() > 0)
            {
                return;
            }

            const string mrtLine1Id = "A";
            const string mrtLine2Id = "B";

            dbContext.TransportLines.Add(new TransportLine { Id = mrtLine1Id, Name = "MRT Line 1" });
            dbContext.TransportLines.Add(new TransportLine { Id = mrtLine2Id, Name = "MRT Line 2" });

            string[] mrtLine1Stations = {
                    "Baclaran",
                    "Edsa",
                    "Libertad",
                    "Gil Puyat",
                    "V. Cruz",
                    "Quirino",
                    "P. Gil",
                    "United Nations",
                    "C. Terminal",
                    "Carriedo",
                    "D. Jose",
                    "Bambang",
                    "Tayuman",
                    "Blumentritt",
                    "A. Santos",
                    "R. Papa",
                    "5th Ave",
                    "Monumento",
                    "Balintawak",
                    "Roosevelt"
                };

            for (int i = 0; i < mrtLine1Stations.Length; i++)
            {
                dbContext.TransportStations.Add(new TransportStation
                {
                    Id = $"{mrtLine1Id}{i + 1}",
                    TransportLineId = mrtLine1Id,
                    Name = mrtLine1Stations[i]
                });
            }

            string[] mrtLine2Stations = {
                    "Recto",
                    "Legarda",
                    "Pureze",
                    "V. Mapa",
                    "J. Ruiz",
                    "Gilmore",
                    "Betty-Go",
                    "Cubao",
                    "Anonas",
                    "Katipunan",
                    "Santolan"
                };

            for (int i = 0; i < mrtLine2Stations.Length; i++)
            {
                dbContext.TransportStations.Add(new TransportStation
                {
                    Id = $"{mrtLine2Id}{i + 1}",
                    TransportLineId = mrtLine2Id,
                    Name = mrtLine2Stations[i]
                });
            }

            int[,] mrtLine1FareMatrix = {
                { 11, 12, 13, 13, 14, 15, 16, 17, 18, 19, 19, 20, 21, 21, 22, 23, 24, 25, 27, 29 },
                { 12, 11, 12, 13, 14, 15, 15, 16, 17, 18, 19, 19, 20, 21, 22, 22, 23, 24, 27, 29 },
                { 13, 12, 11, 12, 13, 14, 14, 15, 16, 17, 18, 18, 19, 20, 21, 21, 22, 23, 26, 28 },
                { 13, 13, 12, 11, 12, 13, 14, 14, 16, 16, 17, 18, 18, 19, 20, 21, 22, 23, 25, 27 },
                { 14, 14, 13, 12, 11, 12, 13, 13, 15, 15, 16, 17, 17, 18, 19, 20, 21, 22, 24, 26 },
                { 15, 15, 14, 13, 12, 11, 12, 13, 14, 14, 15, 16, 16, 17, 18, 19, 20, 21, 23, 25 },
                { 16, 15, 14, 14, 13, 12, 11, 12, 13, 14, 14, 15, 16, 16, 17, 18, 19, 20, 22, 24 },
                { 17, 16, 15, 14, 13, 13, 12, 11, 12, 13, 14, 14, 15, 16, 17, 17, 18, 19, 22, 23 },
                { 18, 17, 16, 16, 15, 14, 13, 12, 11, 12, 12, 13, 14, 14, 15, 16, 17, 18, 20, 22 },
                { 19, 18, 17, 16, 15, 14, 14, 13, 12, 11, 12, 12, 13, 14, 15, 15, 16, 17, 20, 22 },
                { 19, 19, 18, 17, 16, 15, 14, 14, 12, 12, 11, 12, 12, 13, 14, 15, 15, 17, 19, 21 },
                { 20, 19, 18, 18, 17, 16, 15, 14, 13, 12, 12, 11, 12, 12, 13, 14, 15, 16, 18, 20 },
                { 21, 20, 19, 18, 17, 16, 16, 15, 14, 13, 12, 12, 11, 12, 13, 13, 14, 15, 18, 20 },
                { 21, 21, 20, 19, 18, 17, 16, 16, 14, 14, 13, 12, 12, 11, 12, 13, 14, 15, 17, 19 },
                { 22, 22, 21, 20, 19, 18, 17, 17, 15, 15, 14, 13, 13, 12, 11, 12, 13, 14, 16, 18 },
                { 23, 22, 21, 21, 20, 19, 18, 17, 16, 15, 15, 14, 13, 13, 12, 11, 12, 13, 15, 17 },
                { 24, 23, 22, 22, 21, 20, 19, 18, 17, 16, 15, 15, 14, 14, 13, 12, 11, 12, 15, 16 },
                { 25, 24, 23, 23, 22, 21, 20, 19, 18, 17, 17, 16, 15, 15, 14, 13, 12, 11, 13, 15 },
                { 27, 27, 26, 25, 24, 23, 22, 22, 20, 20, 19, 18, 18, 17, 16, 15, 15, 13, 11, 13 },
                { 29, 29, 28, 27, 26, 25, 24, 23, 22, 22, 21, 20, 20, 19, 18, 17, 16, 15, 13, 11 }
            };

            for (int i = 0; i < mrtLine1FareMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < mrtLine1FareMatrix.GetLength(1); j++)
                {
                    dbContext.TransportFares.Add(new TransportFare
                    {
                        Id = Guid.NewGuid(),
                        TransportLineId = mrtLine1Id,
                        FromStationId = $"{mrtLine1Id}{i + 1}",
                        ToStationId = $"{mrtLine1Id}{j + 1}",
                        Fare = mrtLine1FareMatrix[i, j]
                    });
                }
            }

            int[,] mrtLine2FareMatrix = {
                { 11, 12, 14, 15, 16, 17, 18, 19, 21, 22, 24 },
                { 12, 11, 13, 14, 15, 16, 17, 18, 20, 21, 23 },
                { 14, 13, 11, 13, 14, 15, 16, 17, 19, 20, 22 },
                { 15, 14, 13, 11, 13, 14, 15, 16, 18, 19, 21 },
                { 16, 15, 14, 13, 11, 12, 13, 14, 16, 17, 19 },
                { 17, 16, 15, 14, 12, 11, 12, 13, 15, 16, 18 },
                { 18, 17, 16, 15, 13, 12, 11, 12, 14, 15, 17 },
                { 19, 18, 17, 16, 14, 13, 12, 11, 12, 13, 15 },
                { 21, 20, 19, 18, 16, 15, 14, 12, 11, 12, 14 },
                { 22, 21, 20, 19, 17, 16, 15, 13, 12, 11, 13 },
                { 24, 23, 22, 21, 19, 18, 17, 15, 14, 13, 11 }
            };

            for (int i = 0; i < mrtLine2FareMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < mrtLine2FareMatrix.GetLength(1); j++)
                {
                    dbContext.TransportFares.Add(new TransportFare
                    {
                        Id = Guid.NewGuid(),
                        TransportLineId = mrtLine2Id,
                        FromStationId = $"{mrtLine2Id}{i + 1}",
                        ToStationId = $"{mrtLine2Id}{j + 1}",
                        Fare = mrtLine2FareMatrix[i, j]
                    });
                }
            }

            dbContext.SaveChanges();
        }
    }
}
