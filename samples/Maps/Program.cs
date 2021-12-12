﻿using System;
using System.Collections.Generic;
using Parsec.Helpers;
using Parsec.Shaiya.Svmap;
using Parsec.Shaiya.Zon;
using static Parsec.Shaiya.Core.FileBase;

namespace Parsec.Sample.SVMAP
{
    class Program
    {
        static void Main(string[] args)
        {
            #region svmap

            var svmap = ReadFromFile<Svmap>("2.svmap");

            Console.WriteLine($"File: {svmap.FileName}");
            Console.WriteLine($"MapSize: {svmap.MapSize}");
            Console.WriteLine($"Ladder Count: {svmap.Ladders.Count}");
            Console.WriteLine($"Monster Area Count: {svmap.MonsterAreas.Count}");
            Console.WriteLine($"Npc Count: {svmap.Npcs.Count}");
            Console.WriteLine($"Portal Count: {svmap.Portals.Count}");
            Console.WriteLine($"Spawn Count: {svmap.Spawns.Count}");
            Console.WriteLine($"Named Area Count: {svmap.NamedAreas.Count}");

            svmap.ExportJson($"{svmap.FileNameWithoutExtension}.json", new List<string>
            {
                nameof(svmap.MapHeight)
            });

            #endregion

            #region zon

            var zon = ReadFromFile<Zon>("TacticsZone.zon");
            zon.ExportJson($"{zon.FileName}.json");

            var zonfromjson = Deserializer.ReadFromJson<Zon>($"{zon.FileName}.json");
            zonfromjson.Write($"{zonfromjson.FileNameWithoutExtension}.Created.zon");

            #endregion
        }
    }
}
