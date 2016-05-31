using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pikaedit
{
    public static class PkmLib
    {

        public enum Languages
        {
            None,
            Japan,
            English,
            French,
            Italian,
            German,
            Spanish = 7,
            Korean
        }

        public enum GenderRatios
        {
            Genderless,
            Always_Male,
            Always_Female,
            Half, //50%
            Mostly_Male, //87.5% Male
            Almost_Male, //75% Male
            Mostly_Female, //75% Female
        }

        public static byte[] dsvfooter = new byte[] { 0x7C, 0x3C, 0x2D, 0x2D, 0x53, 0x6E, 0x69, 0x70, 0x20, 0x61, 0x62, 0x6F, 0x76, 0x65, 0x20, 0x68, 0x65, 0x72, 0x65, 0x20, 0x74, 0x6F, 0x20, 0x63, 0x72, 0x65, 0x61, 0x74, 0x65, 0x20, 0x61, 0x20, 0x72, 0x61, 0x77, 0x20, 0x73, 0x61, 0x76, 0x20, 0x62, 0x79, 0x20, 0x65, 0x78, 0x63, 0x6C, 0x75, 0x64, 0x69, 0x6E, 0x67, 0x20, 0x74, 0x68, 0x69, 0x73, 0x20, 0x44, 0x65, 0x53, 0x6D, 0x75, 0x4D, 0x45, 0x20, 0x73, 0x61, 0x76, 0x65, 0x64, 0x61, 0x74, 0x61, 0x20, 0x66, 0x6F, 0x6F, 0x74, 0x65, 0x72, 0x3A, 0x0, 0x0, 0x8, 0x0, 0x0, 0x0, 0x8, 0x0, 0x5, 0x0, 0x0, 0x0, 0x3, 0x0, 0x0, 0x0, 0x0, 0x0, 0x8, 0x0, 0x0, 0x0, 0x0, 0x0, 0x7C, 0x2D, 0x44, 0x45, 0x53, 0x4D, 0x55, 0x4D, 0x45, 0x20, 0x53, 0x41, 0x56, 0x45, 0x2D, 0x7C };
        public static byte[] genderRatio = new byte[] { 0, 4, 4, 4, 4, 4, 4, 4, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 2, 2, 1, 1, 1, 6, 6, 6, 6, 6, 6, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 5, 5, 3, 3, 3, 5, 5, 5, 5, 5, 5, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0, 3, 3, 3, 3, 1, 1, 3, 3, 3, 3, 3, 2, 3, 2, 3, 3, 3, 3, 0, 0, 3, 3, 2, 5, 5, 3, 1, 3, 3, 3, 0, 4, 4, 4, 4, 0, 4, 4, 4, 4, 4, 4, 0, 0, 0, 3, 3, 3, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 6, 6, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 3, 3, 3, 0, 3, 3, 3, 3, 3, 3, 3, 6, 6, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 6, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 3, 3, 1, 1, 2, 5, 5, 2, 2, 0, 0, 0, 3, 3, 3, 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 3, 3, 3, 5, 5, 6, 3, 6, 6, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0, 3, 3, 3, 3, 0, 0, 4, 4, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 6, 3, 3, 3, 0, 0, 0, 0, 0, 0, 2, 1, 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 3, 2, 1, 4, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 6, 6, 3, 3, 3, 0, 0, 3, 3, 2, 3, 3, 3, 3, 3, 4, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 3, 3, 3, 5, 5, 4, 3, 4, 4, 3, 3, 0, 1, 3, 3, 2, 0, 0, 0, 0, 0, 0, 3, 0, 0, 2, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4, 4, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 5, 5, 5, 3, 3, 3, 1, 1, 3, 3, 3, 3, 3, 3, 3, 3, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 3, 3, 4, 4, 6, 6, 6, 6, 6, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0, 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 3, 3, 3, 3, 3, 3, 0, 0, 3, 3, 3, 1, 1, 2, 2, 3, 3, 3, 3, 3, 3, 3, 0, 0, 0, 1, 1, 0, 0, 1, 0, 0, 0, 0 };
        
        //English lists
        public static List<string> species = new List<string>();
        public static List<string> abilities = new List<string>();
        public static List<string> natures = new List<string>();
        public static List<string> items = new List<string>();
        public static List<string> locations = new List<string>();
        public static List<string> moves = new List<string>();
        public static List<string> versions = new List<string>();
        public static List<string> languages = new List<string>();

        //Language non-dependant lists
        public static List<ushort> locationValues = new List<ushort> { 0, 0, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123, 124, 125, 126, 127, 128, 129, 130, 131, 132, 133, 134, 135, 136, 137, 139, 140, 141, 142, 143, 144, 145, 146, 147, 148, 149, 150, 151, 152, 153, 2000, 2002, 2009, 2010, 3000, 3001, 3002, 30001, 30002, 30003, 30004, 30005, 30006, 30007, 30008, 30010, 30011, 30012, 30013, 30014, 30015, 40001, 40002, 40003, 40004, 40005, 40006, 40007, 40008, 40009, 40010, 40011, 40012, 40013, 40014, 40015, 40016, 40017, 40018, 40019, 40021, 40020, 40022, 40023, 40024, 40025, 40026, 40027, 40028, 40029, 40030, 40031, 40032, 40033, 40034, 40035, 40036, 40037, 40038, 40039, 40040, 40041, 40042, 40043, 40044, 40045, 40046, 40047, 40048, 40049, 40050, 40051, 40052, 40053, 40054, 40055, 40056, 40057, 40058, 40059, 40060, 40061, 40062, 40063, 40064, 40065, 40066, 40067, 40068, 40069, 40070, 40071, 40072, 40073, 40074, 40075, 40076, 40077, 40078, 40079, 40080, 40081, 40082, 40083, 40084, 40085, 40086, 40087, 40088, 40089, 40090, 40091, 40092, 40093, 40094, 40095, 40096, 40097, 40098, 40099, 40100, 40101, 40102, 40103, 40104, 40105, 40106, 40107, 40108, 40109, 60001, 60002, 60003 };
        public static List<string> pokeballs = new List<string> { "", "Master Ball", "Ultra Ball", "Great Ball", "Poké Ball", "Safari Ball", "Net Ball", "Dive Ball", "Nest Ball", "Repeat Ball", "Timer Ball", "Luxury Ball", "Premier Ball", "Dusk Ball", "Heal Ball", "Quick Ball", "Cherish Ball", "Fast Ball", "Level Ball", "Lure Ball", "Heavy Ball", "Love Ball", "Friend Ball", "Moon Ball", "Sport Ball", "Dream Ball" };
        public static byte[] resetpkm = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x2B, 0x8D, 0x1, 0x0, 0x0, 0x0, 0x1, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x41, 0x0, 0x2, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xD5, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xF, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x2, 0x3, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x42, 0x0, 0x75, 0x0, 0x6C, 0x0, 0x62, 0x0, 0x61, 0x0, 0x73, 0x0, 0x61, 0x0, 0x75, 0x0, 0x72, 0x0, 0xFF, 0xFF, 0xFF, 0xFF, 0x0, 0x17, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x50, 0x0, 0x69, 0x0, 0x6B, 0x0, 0x61, 0x0, 0x45, 0x0, 0x64, 0x0, 0x74, 0x0, 0xFF, 0xFF, 0x0, 0x0, 0x0, 0xD, 0x2, 0x18, 0x0, 0x0, 0x0, 0x0, 0x0, 0x4, 0x1, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x1, 0x0, 0xB, 0x0, 0xB, 0x0, 0x6, 0x0, 0x5, 0x0, 0x6, 0x0, 0x5, 0x0, 0x6, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
        public static List<string> passPowers = new List<string> { "None", "Encounter Power ↑", "Encounter Power ↑↑", "Encounter Power ↑↑↑", "Encounter Power ↓", "Encounter Power ↓↓", "Encounter Power ↓↓↓", "Hatching Power ↑", "Hatching Power ↑↑", "Hatching Power ↑↑↑", "Befriending Power ↑", "Befriending Power ↑↑", "Befriending Power ↑↑↑", "Bargain Power ↑", "Bargain Power ↑↑", "Bargain Power ↑↑↑", "HP Restoring Power ↑", "HP Restoring Power ↑↑", "HP Restoring Power ↑↑↑", "PP Restoring Power ↑", "PP Restoring Power ↑↑", "PP Restoring Power ↑↑↑", "Exp. Point Power ↑", "Exp. Point Power ↑↑", "Exp. Point Power ↑↑↑", "Exp. Point Power ↓", "Exp. Point Power ↓↓", "Exp. Point Power ↓↓↓", "Prize Money Power ↑", "Prize Money Power ↑↑", "Prize Money Power ↑↑↑", "Capture Power ↑", "Capture Power ↑↑", "Capture Power ↑↑↑", "Hatching Power S", "Bargain Power S", "Befriending Power S", "Exp. Point Power S", "Prize Money Power S", "Capture Power S", "Full Recovery Power", "Hatching Power MAX", "Bargain Power MAX", "Befriending Power MAX", "Exp. Point Power MAX", "Prize Money Power MAX", "Capture Power MAX", "None", "None", "None", "Search Power +", "Search Power ++", "Search Power +++", "Hidden Grotto Power +", "Hidden Grotto Power ++", "Hidden Grotto Power +++", "Charm Power +", "Charm Power ++", "Charm Power +++", "Exploring Power S", "Exploring Power MAX", "Grotto Power S", "Grotto Power MAX", "Lucky Power S", "Lucky Power MAX" };
        public static List<byte> pp = new List<byte> { 0, 35, 25, 10, 15, 20, 20, 15, 15, 15, 35, 30, 5, 10, 20, 30, 35, 35, 20, 15, 20, 20, 25, 20, 30, 5, 10, 15, 15, 15, 25, 20, 5, 35, 15, 20, 20, 10, 15, 30, 35, 20, 20, 30, 25, 40, 20, 15, 20, 20, 20, 30, 25, 15, 30, 25, 5, 15, 10, 5, 20, 20, 20, 5, 35, 20, 25, 20, 20, 20, 15, 25, 15, 10, 40, 25, 10, 35, 30, 15, 10, 40, 10, 15, 30, 15, 20, 10, 15, 10, 5, 10, 10, 25, 10, 20, 40, 30, 30, 20, 20, 15, 10, 40, 15, 10, 30, 10, 20, 10, 40, 40, 20, 30, 30, 20, 30, 10, 10, 20, 5, 10, 30, 20, 20, 20, 5, 15, 15, 20, 10, 15, 35, 20, 15, 10, 10, 30, 15, 40, 20, 15, 10, 5, 10, 30, 10, 15, 20, 15, 40, 20, 10, 5, 15, 10, 10, 10, 15, 30, 30, 10, 10, 20, 10, 0, 1, 10, 10, 10, 5, 15, 25, 15, 10, 15, 30, 5, 40, 15, 10, 25, 10, 30, 10, 20, 10, 10, 10, 10, 10, 20, 5, 40, 5, 5, 15, 5, 10, 5, 10, 10, 10, 10, 20, 20, 40, 15, 10, 20, 20, 25, 5, 15, 10, 5, 20, 15, 20, 25, 20, 5, 30, 5, 10, 20, 40, 5, 20, 40, 20, 15, 35, 10, 5, 5, 5, 15, 5, 20, 5, 5, 15, 20, 10, 5, 5, 15, 10, 15, 15, 10, 10, 10, 20, 10, 10, 10, 10, 15, 15, 15, 10, 20, 20, 10, 20, 20, 20, 20, 20, 10, 10, 10, 20, 20, 5, 15, 10, 10, 15, 10, 20, 5, 5, 10, 10, 20, 5, 10, 20, 10, 20, 20, 20, 5, 5, 15, 20, 10, 15, 20, 15, 10, 10, 15, 10, 5, 5, 10, 15, 10, 5, 20, 25, 5, 40, 15, 5, 40, 15, 20, 20, 5, 15, 20, 20, 15, 15, 5, 10, 30, 20, 30, 15, 5, 40, 15, 5, 20, 5, 15, 25, 40, 15, 20, 15, 20, 15, 20, 10, 20, 20, 5, 5, 10, 5, 40, 10, 10, 5, 10, 10, 15, 10, 20, 30, 30, 10, 20, 5, 10, 10, 15, 10, 10, 5, 15, 5, 10, 10, 30, 20, 20, 10, 10, 5, 5, 10, 5, 20, 10, 20, 10, 15, 10, 20, 20, 20, 15, 15, 10, 15, 20, 15, 10, 10, 10, 20, 10, 30, 5, 10, 15, 10, 10, 5, 20, 30, 10, 30, 15, 15, 15, 15, 30, 10, 20, 15, 10, 10, 20, 15, 5, 5, 15, 15, 5, 10, 5, 20, 5, 15, 20, 5, 20, 20, 20, 20, 10, 20, 10, 15, 20, 15, 10, 10, 5, 10, 5, 5, 10, 5, 5, 10, 5, 5, 5, 15, 10, 10, 10, 10, 10, 10, 15, 20, 15, 10, 15, 10, 15, 10, 20, 10, 15, 10, 20, 20, 20, 20, 20, 15, 15, 15, 15, 15, 15, 20, 15, 10, 15, 15, 15, 15, 10, 10, 10, 10, 10, 15, 15, 15, 15, 5, 5, 15, 5, 10, 10, 10, 20, 20, 20, 10, 10, 30, 15, 15, 10, 15, 25, 10, 20, 10, 10, 10, 20, 10, 10, 10, 10, 10, 15, 15, 5, 5, 10, 10, 10, 5, 5, 10, 5, 5, 15, 10, 5, 5, 5, 10, 15, 10, 10, 20, 25, 10, 20, 30, 25, 20, 20, 15, 15, 15, 20, 10, 20, 10, 25, 10, 10, 20, 10, 30, 15, 10, 10, 10, 20, 20, 5, 5, 5, 20, 10, 20, 20, 15, 20, 20, 10, 20, 30, 10, 10, 40, 40, 30, 20, 40, 35, 30, 10, 10, 10, 10, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static byte[] cgearfooter = {0x0, 0x0, 0x1, 0x0, 0x0, 0x0, 0x14, 0x27, 0x0, 0x0, 0x27, 0x35, 0x5, 0x31, 0x0, 0x0};
        public static byte[] pokedexfooter = { 0x0, 0x0, 0x1, 0x0, 0x0, 0x0, 0x14, 0x63, 0x0, 0x0, 0x27, 0x35, 0x5, 0x31, 0x0, 0x0 };
        public static byte[] musicalbw2footer = { 0x0, 0x0, 0x3, 0x0, 0x0, 0x0, 0x14, 0x7D, 0x1, 0x0, 0x27, 0x35, 0x5, 0x31, 0x0, 0x0 };
        public static byte[] musicalbwfooter = { 0x0, 0x0, 0x4, 0x0, 0x0, 0x0, 0x14, 0xFD, 0x1, 0x0, 0x27, 0x35, 0x5, 0x31, 0x0, 0x0 };
        public static byte[] battlevideofooter = { 0x0, 0x0, 0x1, 0x0, 0x0, 0x0, 0x14, 0x19, 0x0, 0x0, 0x27, 0x35, 0x5, 0x31, 0x0, 0x0 };

        //Language dependant lists
        public static List<string> lspecies = new List<string>();
        public static List<string> labilities = new List<string>();
        public static List<string> lnatures = new List<string>();
        public static List<string> litems = new List<string>();
        public static List<string> llocations = new List<string>();
        public static List<string> lmoves = new List<string>();
        public static List<string> lversions = new List<string>();
        public static List<string> llanguages = new List<string>();
        public static List<string> lbadges = new List<string>();

        /// <summary>
        /// Current language value, 0=English, 1=Spanish, 2=Spanish/English, 3=French, 4=German, 6= Korean
        /// </summary>
        public static byte lang = 0;
        public static bool dictionariesInitialized = false;

        //Data
        public static Dictionary<int, byte[]> baseStats = new Dictionary<int, byte[]>();
        //public static Dictionary<string, byte> pokerus = new Dictionary<string, byte>();


        //Experience lists
        public static ushort[] slow = { 58, 59, 72, 73, 90, 91, 102, 103, 111, 112, 120, 121, 127, 128, 129, 130, 131, 142, 143, 144, 145, 146, 147, 148, 149, 150, 170, 171, 214, 220, 221, 226, 227, 228, 229, 234, 241, 243, 244, 245, 246, 247, 248, 249, 250, 280, 281, 282, 287, 288, 289, 304, 305, 306, 309, 310, 318, 319, 357, 369, 371, 372, 373, 374, 375, 376, 377, 378, 379, 380, 381, 382, 383, 384, 385, 386, 443, 444, 445, 446, 449, 450, 451, 452, 455, 458, 459, 460, 464, 473, 475, 480, 481, 482, 483, 484, 485, 486, 487, 488, 489, 490, 491, 493, 494, 582, 583, 584, 602, 603, 604, 610, 611, 612, 627, 628, 629, 630, 633, 634, 635, 636, 637, 638, 639, 640, 641, 642, 643, 644, 645, 646, 647, 648, 649, 692, 693, 703, 704, 705, 706, 716, 717, 718, 719, 720, 721 };
        public static ushort[] mediumSlow = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 16, 17, 18, 29, 30, 31, 32, 33, 34, 43, 44, 45, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 74, 75, 76, 92, 93, 94, 151, 152, 153, 154, 155, 156, 157, 158, 159, 160, 179, 180, 181, 182, 186, 187, 188, 189, 191, 192, 198, 207, 213, 215, 251, 252, 253, 254, 255, 256, 257, 258, 259, 260, 270, 271, 272, 273, 274, 275, 276, 277, 293, 294, 295, 302, 315, 328, 329, 330, 331, 332, 352, 359, 363, 364, 365, 387, 388, 389, 390, 391, 392, 393, 394, 395, 396, 397, 398, 401, 402, 403, 404, 405, 406, 407, 415, 416, 430, 441, 447, 448, 461, 472, 492, 495, 496, 497, 498, 499, 500, 501, 502, 503, 506, 507, 508, 519, 520, 521, 524, 525, 526, 532, 533, 534, 535, 536, 537, 540, 541, 542, 543, 544, 545, 551, 552, 553, 554, 555, 570, 571, 574, 575, 576, 577, 578, 579, 599, 600, 601, 607, 608, 609, 619, 620, 650, 651, 652, 653, 654, 655, 656, 657, 658, 661, 662, 663, 667, 668 };
        public static ushort[] mediumFast = { 10, 11, 12, 13, 14, 15, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 37, 38, 41, 42, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 95, 96, 97, 98, 99, 100, 101, 104, 105, 106, 107, 108, 109, 110, 114, 115, 116, 117, 118, 119, 122, 123, 124, 125, 126, 132, 133, 134, 135, 136, 137, 138, 139, 140, 141, 161, 162, 163, 164, 169, 172, 177, 178, 185, 193, 194, 195, 196, 197, 199, 201, 202, 203, 204, 205, 206, 208, 211, 212, 216, 217, 218, 219, 223, 224, 230, 231, 232, 233, 236, 237, 238, 239, 240, 261, 262, 263, 264, 265, 266, 267, 268, 269, 278, 279, 283, 284, 299, 307, 308, 311, 312, 322, 323, 324, 339, 340, 343, 344, 351, 360, 361, 362, 399, 400, 412, 413, 414, 417, 418, 419, 420, 421, 422, 423, 427, 428, 434, 435, 436, 437, 438, 439, 442, 453, 454, 462, 463, 465, 466, 467, 469, 470, 471, 474, 476, 478, 479, 504, 505, 509, 510, 511, 512, 513, 514, 515, 516, 522, 523, 527, 528, 529, 530, 538, 539, 546, 547, 548, 549, 550, 556, 557, 558, 559, 560, 561, 562, 563, 564, 565, 566, 567, 568, 569, 580, 581, 585, 586, 587, 588, 589, 590, 591, 592, 593, 595, 596, 597, 598, 605, 606, 613, 614, 615, 616, 617, 618, 621, 622, 623, 624, 625, 626, 631, 632, 659, 660, 664, 665, 666, 669, 670, 671, 672, 673, 674, 675, 676, 677, 678, 679, 680, 681, 682, 683, 684, 685, 686, 687, 688, 689, 690, 691, 694, 695, 696, 697, 698, 699, 700, 701, 702, 708, 709, 710, 711, 712, 713, 714, 715 };
        public static ushort[] fast = { 35, 36, 39, 40, 113, 165, 166, 167, 168, 173, 174, 175, 176, 183, 184, 190, 200, 209, 210, 222, 225, 235, 242, 298, 300, 301, 303, 325, 326, 327, 337, 338, 353, 354, 355, 356, 358, 370, 424, 429, 431, 432, 433, 440, 468, 477, 517, 518, 531, 572, 573, 594, 707 };
        public static ushort[] fluctuating = { 285, 286, 296, 297, 314, 316, 317, 320, 321, 336, 341, 342, 425, 426 };
        public static ushort[] erratic = { 290, 291, 292, 313, 333, 334, 335, 345, 346, 347, 348, 349, 350, 366, 367, 368, 408, 409, 410, 411, 456, 457 };
        public static uint[] slowlist = { 0, 10, 33, 80, 156, 270, 428, 640, 911, 1250, 1663, 2160, 2746, 3430, 4218, 5120, 6141, 7290, 8573, 10000, 11576, 13310, 15208, 17280, 19531, 21970, 24603, 27440, 30486, 33750, 37238, 40960, 44921, 49130, 53593, 58320, 63316, 68590, 74148, 80000, 86151, 92610, 99383, 106480, 113906, 121670, 129778, 138240, 147061, 156250, 165813, 175760, 186096, 196830, 207968, 219520, 231491, 243890, 256723, 270000, 283726, 297910, 312558, 327680, 343281, 359370, 375953, 393040, 410636, 428750, 447388, 466560, 486271, 506530, 527343, 548720, 570666, 593190, 616298, 640000, 664301, 689210, 714733, 740880, 767656, 795070, 823128, 851840, 881211, 911250, 941963, 973360, 1005446, 1038230, 1071718, 1105920, 1140841, 1176490, 1212873, 1250000 };
        public static uint[] mediumSlowList = { 0, 9, 57, 96, 135, 179, 236, 314, 419, 560, 742, 973, 1261, 1612, 2035, 2535, 3120, 3798, 4575, 5460, 6458, 7577, 8825, 10208, 11735, 13411, 15244, 17242, 19411, 21760, 24294, 27021, 29949, 33084, 36435, 40007, 43808, 47846, 52127, 56660, 61450, 66505, 71833, 77440, 83335, 89523, 96012, 102810, 109923, 117360, 125126, 133229, 141677, 150476, 159635, 169159, 179056, 189334, 199999, 211060, 222522, 234393, 246681, 259392, 272535, 286115, 300140, 314618, 329555, 344960, 360838, 377197, 394045, 411388, 429235, 447591, 466464, 485862, 505791, 526260, 547274, 568841, 590969, 613664, 636935, 660787, 685228, 710266, 735907, 762160, 789030, 816525, 844653, 873420, 902835, 932903, 963632, 995030, 1027103, 1059860 };
        public static uint[] mediumFastList = { 0, 8, 27, 64, 125, 216, 343, 512, 729, 1000, 1331, 1728, 2197, 2744, 3375, 4096, 4913, 5832, 6859, 8000, 9261, 10648, 12167, 13824, 15625, 17576, 19683, 21952, 24389, 27000, 29791, 32768, 35937, 39304, 42875, 46656, 50653, 54872, 59319, 64000, 68921, 74088, 79507, 85184, 91125, 97336, 103823, 110592, 117649, 125000, 132651, 140608, 148877, 157464, 166375, 175616, 185193, 195112, 205379, 216000, 226981, 238328, 250047, 262144, 274625, 287496, 300763, 314432, 328509, 343000, 357911, 373248, 389017, 405224, 421875, 438976, 456533, 474552, 493039, 512000, 531441, 551368, 571787, 592704, 614125, 636056, 658503, 681472, 704969, 729000, 753571, 778688, 804357, 830584, 857375, 884736, 912673, 941192, 970299, 1000000 };
        public static uint[] fastlist = { 0, 6, 21, 51, 100, 172, 274, 409, 583, 800, 1064, 1382, 1757, 2195, 2700, 3276, 3930, 4665, 5487, 6400, 7408, 8518, 9733, 11059, 12500, 14060, 15746, 17561, 19511, 21600, 23832, 26214, 28749, 31443, 34300, 37324, 40522, 43897, 47455, 51200, 55136, 59270, 63605, 68147, 72900, 77868, 83058, 88473, 94119, 100000, 106120, 112486, 119101, 125971, 133100, 140492, 148154, 156089, 164303, 172800, 181584, 190662, 200037, 209715, 219700, 229996, 240610, 251545, 262807, 274400, 286328, 298598, 311213, 324179, 337500, 351180, 365226, 379641, 394431, 409600, 425152, 441094, 457429, 474163, 491300, 508844, 526802, 545177, 563975, 583200, 602856, 622950, 643485, 664467, 685900, 707788, 730138, 752953, 776239, 800000 };
        public static uint[] fluctuatinglist = { 0, 4, 13, 32, 65, 112, 178, 276, 393, 540, 745, 967, 1230, 1591, 1957, 2457, 3046, 3732, 4526, 5440, 6482, 7666, 9003, 10506, 12187, 14060, 16140, 18439, 20974, 23760, 26811, 30146, 33780, 37731, 42017, 46656, 50653, 55969, 60505, 66560, 71677, 78533, 84277, 91998, 98415, 107069, 114205, 123863, 131766, 142500, 151222, 163105, 172697, 185807, 196322, 210739, 222231, 238036, 250562, 267840, 281456, 300293, 315059, 335544, 351520, 373744, 390991, 415050, 433631, 459620, 479600, 507617, 529063, 559209, 582187, 614566, 639146, 673863, 700115, 737280, 765275, 804997, 834809, 877201, 908905, 954084, 987754, 1035837, 1071552, 1122660, 1160499, 1214753, 1254796, 1312322, 1354652, 1415577, 1460276, 1524731, 1571884, 1640000 };
        public static uint[] erraticlist = { 0, 15, 52, 122, 237, 406, 637, 942, 1326, 1800, 2369, 3041, 3822, 4719, 5737, 6881, 8155, 9564, 11111, 12800, 14632, 16610, 18737, 21012, 23437, 26012, 28737, 31610, 34632, 37800, 41111, 44564, 48155, 51881, 55737, 59719, 63822, 68041, 72369, 76800, 81326, 85942, 90637, 95406, 100237, 105122, 110052, 115015, 120001, 125000, 131324, 137795, 144410, 151165, 158056, 165079, 172229, 179503, 186894, 194400, 202013, 209728, 217540, 225443, 233431, 241496, 249633, 257834, 267406, 276458, 286328, 296358, 305767, 316074, 326531, 336255, 346965, 357812, 367807, 378880, 390077, 400293, 411686, 423190, 433572, 445239, 457001, 467489, 479378, 491346, 501878, 513934, 526049, 536557, 548720, 560922, 571333, 583539, 591882, 600000 };
        //0 = Slow, 1 = Medium Slow, 2 = Medium Fast, 3 = Fast, 4 = Fluctuating, 5 = Erratic
        public static byte[] expList = { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 3, 3, 2, 2, 3, 3, 2, 2, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 0, 0, 2, 2, 2, 2, 2, 2, 2, 0, 0, 3, 2, 2, 2, 2, 2, 2, 0, 0, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 2, 0, 0, 2, 3, 3, 3, 3, 2, 2, 1, 1, 1, 1, 3, 3, 2, 1, 1, 1, 1, 3, 1, 1, 2, 2, 2, 2, 2, 1, 2, 3, 2, 2, 2, 2, 2, 2, 1, 2, 3, 3, 2, 2, 1, 0, 1, 2, 2, 2, 2, 0, 0, 3, 2, 2, 3, 0, 0, 0, 0, 2, 2, 2, 2, 0, 3, 2, 2, 2, 2, 2, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 0, 0, 0, 2, 2, 4, 4, 0, 0, 0, 5, 5, 5, 1, 1, 1, 4, 4, 3, 2, 3, 3, 1, 3, 0, 0, 0, 2, 2, 0, 0, 2, 2, 5, 4, 1, 4, 4, 0, 0, 4, 4, 2, 2, 2, 3, 3, 3, 1, 1, 1, 1, 1, 5, 5, 5, 4, 3, 3, 2, 2, 4, 4, 2, 2, 5, 5, 5, 5, 5, 5, 2, 1, 3, 3, 3, 3, 0, 3, 1, 2, 2, 2, 1, 1, 1, 5, 5, 5, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 1, 1, 1, 1, 1, 1, 1, 5, 5, 5, 5, 2, 2, 2, 1, 1, 2, 2, 2, 2, 2, 2, 2, 3, 4, 4, 2, 2, 3, 1, 3, 3, 3, 2, 2, 2, 2, 2, 2, 3, 1, 2, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 2, 2, 0, 5, 5, 0, 0, 0, 1, 2, 2, 0, 2, 2, 2, 3, 2, 2, 2, 1, 0, 2, 0, 2, 3, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 1, 1, 1, 2, 2, 1, 1, 1, 2, 2, 2, 2, 3, 1, 1, 1, 1, 1, 1, 2, 2, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 3, 3, 1, 1, 1, 1, 1, 1, 2, 2, 0, 0, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 2, 2, 2, 2, 1, 1, 1, 0, 0, 0, 2, 2, 1, 1, 1, 0, 0, 0, 2, 2, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 1, 1, 1, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 3, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0 };

        /// <summary>
        /// Get pokemon sprite
        /// </summary>
        /// <param name="pkmNo">Species Pokedex number</param>
        /// <param name="form">Pokemon form</param>
        /// <returns>Given Pokemon sprite as Image</returns>
        public static System.Drawing.Image getSprite(ushort pkmNo, string form = "None")
        {
            if (form == "None")
            {
                return Properties.Resources.ResourceManager.GetObject("_" + pkmNo) as System.Drawing.Image;
            }
            else
            {
                if (form == "Fan")
                {
                    return Properties.Resources.ResourceManager.GetObject("_" + pkmNo + "_s") as System.Drawing.Image;
                }
                else
                {
                    if (Properties.Resources.ResourceManager.GetObject("_" + pkmNo + "_" + form.First().ToString().ToLowerInvariant()) != null)
                    {
                        return Properties.Resources.ResourceManager.GetObject("_" + pkmNo + "_" + form.First().ToString().ToLowerInvariant()) as System.Drawing.Image;
                    }
                    else
                    {
                        return Properties.Resources.ResourceManager.GetObject("_" + pkmNo) as System.Drawing.Image;
                    }
                }
            }
        }
        public static void clearLists()
        {
            lspecies.Clear();
            lmoves.Clear();
            labilities.Clear();
            lnatures.Clear();
            litems.Clear();
            llocations.Clear();
            lversions.Clear();
            llanguages.Clear();
        }

        private static void initializeLists()
        {
            species.Clear();
            moves.Clear();
            abilities.Clear();
            natures.Clear();
            items.Clear();
            locations.Clear();
            string[] temp = Properties.Resources.enSpecies.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                species.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.enMoves.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                moves.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.enAbilities.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                abilities.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.enNatures.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                natures.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.enItems.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                items.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.enLocations.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                locations.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.enLanguages.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                languages.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.enVersions.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                versions.Add(temp[i].TrimEnd('\r'));
            }
            english();
        }

        public static void changeLanguage(byte l=0)
        {
            lang = l;
            //Clear lists
            clearLists();
            switch (lang)
            {
                case 0:
                    english();
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    french();
                    break;
                case 4:
                    german();
                    break;
                case 6:
                    korean();
                    break;
                default:
                    break;
            }
        }

        private static void english()
        {
            string[] temp = Properties.Resources.enSpecies.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                lspecies.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.enMoves.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                lmoves.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.enAbilities.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                labilities.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.enNatures.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                lnatures.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.enItems.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                litems.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.enLocations.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                llocations.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.enLanguages.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                llanguages.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.enVersions.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                lversions.Add(temp[i].TrimEnd('\r'));
            }
        }

        private static void french()
        {
            string[] temp = Properties.Resources.frSpecies.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                lspecies.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.frMoves.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                lmoves.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.frAbilities.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                labilities.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.frNatures.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                lnatures.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.frItems.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                litems.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.frLocations.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                llocations.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.frLanguages.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                llanguages.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.frVersions.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                lversions.Add(temp[i].TrimEnd('\r'));
            }
        }

        private static void korean()
        {
            string[] temp = Properties.Resources.krSpecies.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                lspecies.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.krMoves.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                lmoves.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.krAbilities.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                labilities.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.krNatures.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                lnatures.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.krItems.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                litems.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.krLocations.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                llocations.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.enLanguages.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                llanguages.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.enVersions.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                lversions.Add(temp[i].TrimEnd('\r'));
            }
        }

        private static void german()
        {
            string[] temp = Properties.Resources.grSpecies.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                lspecies.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.grMoves.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                lmoves.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.grAbilities.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                labilities.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.grNatures.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                lnatures.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.grItems.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                litems.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.grLocations.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                llocations.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.grLanguages.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                llanguages.Add(temp[i].TrimEnd('\r'));
            }
            temp = Properties.Resources.grVersions.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                lversions.Add(temp[i].TrimEnd('\r'));
            }
        }

        public static string speciesTranslate(string s)
        {
            if (lang == 0)
            {
                return s;
            }
            else
            {
                int i = lspecies.IndexOf(s);
                if (i != -1)
                {
                    return species[i];
                }
                return s;
            }
        }

        public static string abilityTranslate(string s)
        {
            if (lang == 0)
            {
                return s;
            }
            else
            {
                int i = labilities.IndexOf(s);
                if (i != -1)
                {
                    return abilities[i];
                }
                return s;
            }
        }

        public static string itemTranslate(string s)
        {
            if (lang == 0)
            {
                return s;
            }
            else
            {
                int i = litems.IndexOf(s);
                if (i != -1)
                {
                    return items[i];
                }
                return s;
            }
        }

        public static string locationTranslate(string s)
        {
            if (lang == 0)
            {
                return s;
            }
            else
            {
                int i = llocations.IndexOf(s);
                if (i != -1)
                {
                    return locations[i];
                }
                return s;
            }
        }

        public static string natureTranslate(string s)
        {
            if (lang == 0)
            {
                return s;
            }
            else
            {
                int i = lnatures.IndexOf(s);
                if (i != -1)
                {
                    return natures[i];
                }
                return s;
            }
        }

        public static string moveTranslate(string s)
        {
            if (lang == 0)
            {
                return s;
            }
            else
            {
                int i = lmoves.IndexOf(s);
                if (i != -1)
                {
                    return moves[i];
                }
                return s;
            }
        }

        public static string languageTranslate(string s)
        {
            if (lang == 0)
            {
                return s;
            }
            else
            {
                int i = llanguages.IndexOf(s);
                if (i != -1)
                {
                    return languages[i];
                }
                return s;
            }
        }

        public static string versionTranslate(string s)
        {
            if (lang == 0)
            {
                return s;
            }
            else
            {
                int i = lversions.IndexOf(s);
                if (i != -1)
                {
                    return versions[i];
                }
                return s;
            }
        }

        public static string speciesTranslateTo(string s)
        {
            if (lang == 0)
            {
                return s;
            }
            else
            {
                int i = species.IndexOf(s);
                if (i != -1)
                {
                    return lspecies[i];
                }
                return s;
            }
        }

        public static string abilityTranslateTo(string s)
        {
            if (lang == 0)
            {
                return s;
            }
            else
            {
                int i = abilities.IndexOf(s);
                if (i != -1)
                {
                    return labilities[i];
                }
                return s;
            }
        }

        public static string itemTranslateTo(string s)
        {
            if (lang == 0)
            {
                return s;
            }
            else
            {
                int i = items.IndexOf(s);
                if (i != -1)
                {
                    return litems[i];
                }
                return s;
            }
        }

        public static string locationTranslateTo(string s)
        {
            if (lang == 0)
            {
                return s;
            }
            else
            {
                int i = locations.IndexOf(s);
                if (i != -1)
                {
                    return llocations[i];
                }
                return s;
            }
        }

        public static string natureTranslateTo(string s)
        {
            if (lang == 0)
            {
                return s;
            }
            else
            {
                int i = natures.IndexOf(s);
                if (i != -1)
                {
                    return lnatures[i];
                }
                return s;
            }
        }

        public static string moveTranslateTo(string s)
        {
            if (lang == 0)
            {
                return s;
            }
            else
            {
                int i = moves.IndexOf(s);
                if (i != -1)
                {
                    return lmoves[i];
                }
                return s;
            }
        }

        public static string versionTranslateTo(string s)
        {
            if (lang == 0)
            {
                return s;
            }
            else
            {
                int i = versions.IndexOf(s);
                if (i != -1)
                {
                    return lversions[i];
                }
                return s;
            }
        }

        public static string languageTranslateTo(string s)
        {
            if (lang == 0)
            {
                return s;
            }
            else
            {
                int i = languages.IndexOf(s);
                if (i != -1)
                {
                    return llanguages[i];
                }
                return s;
            }
        }

        public static string[] getFormList(string species)
        {
            string[] forms = new string[] { "None" };
            if (species == "Unown")
            {
                forms = new string[]{
				"A",
				"B",
				"C",
				"D",
				"E",
				"F",
				"G",
				"H",
				"I",
				"J",
				"K",
				"L",
				"M",
				"N",
				"O",
				"P",
				"Q",
				"R",
				"S",
				"T",
				"U",
				"V",
				"W",
				"X",
				"Y",
				"Z",
				"?",
				"!"
			};
            }
            if (species == "Deoxys")
            {
                forms = new string[]{
				"Normal",
				"Attack",
				"Defense",
				"Speed"
			};
            }
            if (species == "Burmy")
            {
                forms = new string[]{
				"Plant",
				"Sandy",
				"Thrash"
			};
            }
            if (species == "Wormadam")
            {
                forms = new string[]{
				"Plant",
				"Sandy",
				"Thrash"
			};
            }
            if (species == "Shellos")
            {
                forms = new string[]{
				"West",
				"East"
			};
            }
            if (species == "Gastrodon")
            {
                forms = new string[]{
				"West",
				"East"
			};
            }
            if (species == "Rotom")
            {
                forms = new string[]{
				"Normal",
				"Heat",
				"Wash",
				"Frost",
				"Fan",
				"Mow"
			};
            }
            if (species == "Giratina")
            {
                forms = new string[]{
				"Altered",
				"Origin"
			};
            }
            if (species == "Shaymin")
            {
                forms = new string[]{
				"Land",
				"Sky"
			};
            }
            if (species == "Arceus")
            {
                forms = new string[]{
				"Normal",
				"Fighting",
				"Flying",
				"Poison",
				"Ground",
				"Rock",
				"Bug",
				"Ghost",
				"Steel",
				"Fire",
				"Water",
				"Grass",
				"Electric",
				"Psychic",
				"Ice",
				"Dragon",
				"Dark"
			};
            }
            if (species == "Basculin")
            {
                forms = new string[]{
				"Red Striped",
				"Blue Striped"
			};
            }
            if (species == "Deerling")
            {
                forms = new string[]{
				"Spring",
				"Summer",
				"Autumn",
				"Winter"
			};
            }
            if (species == "Sawsbuck")
            {
                forms = new string[]{
				"Spring",
				"Summer",
				"Autumn",
				"Winter"
			};
            }
            if (species == "Tornadus")
            {
                forms = new string[] {
				"Incarnate",
				"Therian"
			};
            }
            if (species == "Thundurus")
            {
                forms = new string[]{
				"Incarnate",
				"Therian"
			};
            }
            if (species == "Landorus")
            {
                forms = new string[]{
				"Incarnate",
				"Therian"
			};
            }
            if (species == "Kyurem")
            {
                forms = new string[]{
				"Normal",
				"White",
                "Black",
            };
            }
            if (species == "Keldeo")
            {
                forms = new string[]{
				"Ordinary",
				"Resolute"
			};
            }
            if (species == "Meloetta")
            {
                forms = new string[]{
				"Aria",
				"Pirouette"
			};
            }
            if (species == "Genesect")
            {
                forms = new string[]{
				"Normal",
				"Douse",
				"Shock",
				"Burn",
				"Chill"
			};
            }
            if (species == "Vivillon" | species == "Scatterbug" | species == "Spewpa")
            {
                forms = new string[]{
                "Icy-snow",
                "Polar",
                "Tundra",
                "Continental",
                "Garden",
                "Elegant",
                "Meadow",
                "Modern",
                "Marine",
                "Archipelago",
                "High-plains",
                "Sandstorm",
                "River",
                "Monsoon",
                "Savanna",
                "Sun",
                "Ocean",
                "Jungle"
            };
            }
            if (species == "Flabébé" | species == "Floette" | species == "Florges")
            {
                forms = new string[]{
                "Red",
                "Yellow",
                "Orange",
                "Blue",
                "White"
            };
            }
            if (species == "Furfrou")
            {
                forms = new string[]{
                "Natural",
                "Heart",
                "Star",
                "Diamond",
                "Deputante",
                "Matron",
                "Dandy",
                "La-reine",
                "Kabuki",
                "Pharoah"
            };
            }
            if (species == "Meowstic")
            {
                forms = new string[]{
                "Male",
                "Female"
            };
            }
            if (species == "Aegislash")
            {
                forms = new string[]{
                "Shield",
                "Blade"
            };
            }
            if (species == "Pumpkaboo" | species == "Gourgeist")
            {
                forms = new string[]{
                "Average",
                "Small",
                "Large",
                "Super"
            };
            }
            if (species == "Venusaur" | species == "Blastoise" | species == "Alakazam" | species == "Gengar" | species == "Kangaskhan" | species == "Pinsir" | species == "Gyarados" | species == "Aerodactyl" | species == "Ampharos" | species == "Scizor" | species == "Heracross" | species == "Houndoom" | species == "Tyranitar" | species == "Blaziken" | species == "Gardevoir" | species == "Mawile" | species == "Aggron" | species == "Medicham" | species == "Manectric" | species == "Banette" | species == "Absol" | species == "Garchomp" | species == "Lucario" | species == "Abomasnow")
            {
                forms = new string[]{
                "Normal",
                "Mega"
            };
            }
            if (species == "Charizard" | species == "Mewtwo")
            {
                forms = new string[]{
                "Normal",
                "Mega X",
                "Mega Y"
            };
            }
            return forms;
        }

        public static string getFormFromValue(string species, byte value)
        {
            string[] forms = getFormList(species);
            if (forms.Length < value)
            {
                return "None";
            }
            return forms[value];
        }

        public static byte getFormValue(string species, string form)
        {
            string[] forms = getFormList(species);
            if (forms.Length == 1)
            {
                return 0;
            }
            for (byte i = 0; i < forms.Length; i++)
            {
                if (forms[i] == form)
                {
                    return i;
                }
            }
            return 0;
        }

        public static void Initialize()
        {
            if (dictionariesInitialized)
                return;

            initializeLists();

            ////Pokerus
            //pokerus.Add("None", 0);
            //pokerus.Add("Infected (0)", 0x2);
            //pokerus.Add("Cured (0)", 0x1);
            //pokerus.Add("Infected (1)", 0x12);
            //pokerus.Add("Cured (1)", 0x10);
            //pokerus.Add("Infected (2)", 0x23);
            //pokerus.Add("Cured (2)", 0x20);
            //pokerus.Add("Infected (3)", 0x34);
            //pokerus.Add("Cured (3)", 0x30);
            //pokerus.Add("Infected (4)", 0x41);
            //pokerus.Add("Cured (4)", 0x40);
            //pokerus.Add("Infected (5)", 0x52);
            //pokerus.Add("Cured (5)", 0x50);
            //pokerus.Add("Infected (6)", 0x63);
            //pokerus.Add("Cured (6)", 0x60);
            //pokerus.Add("Infected (7)", 0x74);
            //pokerus.Add("Cured (7)", 0x70);
            //pokerus.Add("Infected (8)", 0x81);
            //pokerus.Add("Cured (8)", 0x80);
            //pokerus.Add("Infected (9)", 0x92);
            //pokerus.Add("Cured (9)", 0x90);
            //pokerus.Add("Infected (10)", 0xa3);
            //pokerus.Add("Cured (10)", 0xa0);
            //pokerus.Add("Infected (11)", 0xb4);
            //pokerus.Add("Cured (11)", 0xb0);
            //pokerus.Add("Infected (12)", 0xc1);
            //pokerus.Add("Cured (12)", 0xc0);
            //pokerus.Add("Infected (13)", 0xd2);
            //pokerus.Add("Cured (13)", 0xd0);
            //pokerus.Add("Infected (14)", 0xe3);
            //pokerus.Add("Cured (14)", 0xe0);
            //pokerus.Add("Infected (15)", 0xf4);
            //pokerus.Add("Cured (15)", 0xf0);

            //Base Stats
            //HP/Atk/Def/SpA/SpD/Spd
            baseStats.Add(0, new byte[] {
			0,
			0,
			0,
			0,
			0,
			0
		});
            baseStats.Add(1, new byte[] {
			45,
			49,
			49,
			65,
			65,
			45
		});
            baseStats.Add(2, new byte[] {
			60,
			62,
			63,
			80,
			80,
			60
		});
            baseStats.Add(3, new byte[] {
			80,
			82,
			83,
			100,
			100,
			80
		});
            baseStats.Add(4, new byte[] {
			39,
			52,
			43,
			60,
			50,
			65
		});
            baseStats.Add(5, new byte[] {
			58,
			64,
			58,
			80,
			65,
			80
		});
            baseStats.Add(6, new byte[] {
			78,
			84,
			78,
			109,
			85,
			100
		});
            baseStats.Add(7, new byte[] {
			44,
			48,
			65,
			50,
			64,
			43
		});
            baseStats.Add(8, new byte[] {
			59,
			63,
			80,
			65,
			80,
			58
		});
            baseStats.Add(9, new byte[] {
			79,
			83,
			100,
			85,
			105,
			78
		});
            baseStats.Add(10, new byte[] {
			45,
			30,
			35,
			20,
			20,
			45
		});
            baseStats.Add(11, new byte[] {
			50,
			20,
			55,
			25,
			25,
			30
		});
            baseStats.Add(12, new byte[] {
			60,
			45,
			50,
			80,
			80,
			70
		});
            baseStats.Add(13, new byte[] {
			40,
			35,
			30,
			20,
			20,
			50
		});
            baseStats.Add(14, new byte[] {
			45,
			25,
			50,
			25,
			25,
			35
		});
            baseStats.Add(15, new byte[] {
			65,
			80,
			40,
			45,
			80,
			75
		});
            baseStats.Add(16, new byte[] {
			40,
			45,
			40,
			35,
			35,
			56
		});
            baseStats.Add(17, new byte[] {
			63,
			60,
			55,
			50,
			50,
			71
		});
            baseStats.Add(18, new byte[] {
			83,
			80,
			75,
			70,
			70,
			91
		});
            baseStats.Add(19, new byte[] {
			30,
			56,
			35,
			25,
			35,
			72
		});
            baseStats.Add(20, new byte[] {
			55,
			81,
			60,
			50,
			70,
			97
		});
            baseStats.Add(21, new byte[] {
			40,
			60,
			30,
			31,
			31,
			70
		});
            baseStats.Add(22, new byte[] {
			65,
			90,
			65,
			61,
			61,
			100
		});
            baseStats.Add(23, new byte[] {
			35,
			60,
			44,
			40,
			54,
			55
		});
            baseStats.Add(24, new byte[] {
			60,
			85,
			69,
			65,
			79,
			80
		});
            baseStats.Add(25, new byte[] {
			35,
			55,
			30,
			50,
			40,
			90
		});
            baseStats.Add(26, new byte[] {
			60,
			90,
			55,
			90,
			80,
			100
		});
            baseStats.Add(27, new byte[] {
			50,
			75,
			85,
			20,
			30,
			40
		});
            baseStats.Add(28, new byte[] {
			75,
			100,
			110,
			45,
			55,
			65
		});
            baseStats.Add(29, new byte[] {
			55,
			47,
			52,
			40,
			40,
			41
		});
            baseStats.Add(30, new byte[] {
			70,
			62,
			67,
			55,
			55,
			56
		});
            baseStats.Add(31, new byte[] {
			90,
			82,
			87,
			75,
			85,
			76
		});
            baseStats.Add(32, new byte[] {
			46,
			57,
			40,
			40,
			40,
			50
		});
            baseStats.Add(33, new byte[] {
			61,
			72,
			57,
			55,
			55,
			65
		});
            baseStats.Add(34, new byte[] {
			81,
			92,
			77,
			85,
			75,
			85
		});
            baseStats.Add(35, new byte[] {
			70,
			45,
			48,
			60,
			65,
			35
		});
            baseStats.Add(36, new byte[] {
			95,
			70,
			73,
			85,
			90,
			60
		});
            baseStats.Add(37, new byte[] {
			38,
			41,
			40,
			50,
			65,
			65
		});
            baseStats.Add(38, new byte[] {
			73,
			76,
			75,
			81,
			100,
			100
		});
            baseStats.Add(39, new byte[] {
			115,
			45,
			20,
			45,
			25,
			20
		});
            baseStats.Add(40, new byte[] {
			140,
			70,
			45,
			75,
			50,
			45
		});
            baseStats.Add(41, new byte[] {
			40,
			45,
			35,
			30,
			40,
			55
		});
            baseStats.Add(42, new byte[] {
			75,
			80,
			70,
			65,
			75,
			90
		});
            baseStats.Add(43, new byte[] {
			45,
			50,
			55,
			75,
			65,
			30
		});
            baseStats.Add(44, new byte[] {
			60,
			65,
			70,
			85,
			75,
			40
		});
            baseStats.Add(45, new byte[] {
			75,
			80,
			85,
			100,
			90,
			50
		});
            baseStats.Add(46, new byte[] {
			35,
			70,
			55,
			45,
			55,
			25
		});
            baseStats.Add(47, new byte[] {
			60,
			95,
			80,
			60,
			80,
			30
		});
            baseStats.Add(48, new byte[] {
			60,
			55,
			50,
			40,
			55,
			45
		});
            baseStats.Add(49, new byte[] {
			70,
			65,
			60,
			90,
			75,
			90
		});
            baseStats.Add(50, new byte[] {
			10,
			55,
			25,
			35,
			45,
			95
		});
            baseStats.Add(51, new byte[] {
			35,
			80,
			50,
			50,
			70,
			120
		});
            baseStats.Add(52, new byte[] {
			40,
			45,
			35,
			40,
			40,
			90
		});
            baseStats.Add(53, new byte[] {
			65,
			70,
			60,
			65,
			65,
			115
		});
            baseStats.Add(54, new byte[] {
			50,
			52,
			48,
			65,
			50,
			55
		});
            baseStats.Add(55, new byte[] {
			80,
			82,
			78,
			95,
			80,
			85
		});
            baseStats.Add(56, new byte[] {
			40,
			80,
			35,
			35,
			45,
			70
		});
            baseStats.Add(57, new byte[] {
			65,
			105,
			60,
			60,
			70,
			95
		});
            baseStats.Add(58, new byte[] {
			55,
			70,
			45,
			70,
			50,
			60
		});
            baseStats.Add(59, new byte[] {
			90,
			110,
			80,
			100,
			80,
			95
		});
            baseStats.Add(60, new byte[] {
			40,
			50,
			40,
			40,
			40,
			90
		});
            baseStats.Add(61, new byte[] {
			65,
			65,
			65,
			50,
			50,
			90
		});
            baseStats.Add(62, new byte[] {
			90,
			85,
			95,
			70,
			90,
			70
		});
            baseStats.Add(63, new byte[] {
			25,
			20,
			15,
			105,
			55,
			90
		});
            baseStats.Add(64, new byte[] {
			40,
			35,
			30,
			120,
			70,
			105
		});
            baseStats.Add(65, new byte[] {
			55,
			50,
			45,
			135,
			85,
			120
		});
            baseStats.Add(66, new byte[] {
			70,
			80,
			50,
			35,
			35,
			35
		});
            baseStats.Add(67, new byte[] {
			80,
			100,
			70,
			50,
			60,
			45
		});
            baseStats.Add(68, new byte[] {
			90,
			130,
			80,
			65,
			85,
			55
		});
            baseStats.Add(69, new byte[] {
			50,
			75,
			35,
			70,
			30,
			40
		});
            baseStats.Add(70, new byte[] {
			65,
			90,
			50,
			85,
			45,
			55
		});
            baseStats.Add(71, new byte[] {
			80,
			105,
			65,
			100,
			60,
			70
		});
            baseStats.Add(72, new byte[] {
			40,
			40,
			35,
			50,
			100,
			70
		});
            baseStats.Add(73, new byte[] {
			80,
			70,
			65,
			80,
			120,
			100
		});
            baseStats.Add(74, new byte[] {
			40,
			80,
			100,
			30,
			30,
			20
		});
            baseStats.Add(75, new byte[] {
			55,
			95,
			115,
			45,
			45,
			35
		});
            baseStats.Add(76, new byte[] {
			80,
			110,
			130,
			55,
			65,
			45
		});
            baseStats.Add(77, new byte[] {
			50,
			85,
			55,
			65,
			65,
			90
		});
            baseStats.Add(78, new byte[] {
			65,
			100,
			70,
			80,
			80,
			105
		});
            baseStats.Add(79, new byte[] {
			90,
			65,
			65,
			40,
			40,
			15
		});
            baseStats.Add(80, new byte[] {
			95,
			75,
			110,
			100,
			80,
			30
		});
            baseStats.Add(81, new byte[] {
			25,
			35,
			70,
			95,
			55,
			45
		});
            baseStats.Add(82, new byte[] {
			50,
			60,
			95,
			120,
			70,
			70
		});
            baseStats.Add(83, new byte[] {
			52,
			65,
			55,
			58,
			62,
			60
		});
            baseStats.Add(84, new byte[] {
			35,
			85,
			45,
			35,
			35,
			75
		});
            baseStats.Add(85, new byte[] {
			60,
			110,
			70,
			60,
			60,
			100
		});
            baseStats.Add(86, new byte[] {
			65,
			45,
			55,
			45,
			70,
			45
		});
            baseStats.Add(87, new byte[] {
			90,
			70,
			80,
			70,
			95,
			70
		});
            baseStats.Add(88, new byte[] {
			80,
			80,
			50,
			40,
			50,
			25
		});
            baseStats.Add(89, new byte[] {
			105,
			105,
			75,
			65,
			100,
			50
		});
            baseStats.Add(90, new byte[] {
			30,
			65,
			100,
			45,
			25,
			40
		});
            baseStats.Add(91, new byte[] {
			50,
			95,
			180,
			85,
			45,
			70
		});
            baseStats.Add(92, new byte[] {
			30,
			35,
			30,
			100,
			35,
			80
		});
            baseStats.Add(93, new byte[] {
			45,
			50,
			45,
			115,
			55,
			95
		});
            baseStats.Add(94, new byte[] {
			60,
			65,
			60,
			130,
			75,
			110
		});
            baseStats.Add(95, new byte[] {
			35,
			45,
			160,
			30,
			45,
			70
		});
            baseStats.Add(96, new byte[] {
			60,
			48,
			45,
			43,
			90,
			42
		});
            baseStats.Add(97, new byte[] {
			85,
			73,
			70,
			73,
			115,
			67
		});
            baseStats.Add(98, new byte[] {
			30,
			105,
			90,
			25,
			25,
			50
		});
            baseStats.Add(99, new byte[] {
			55,
			130,
			115,
			50,
			50,
			75
		});
            baseStats.Add(100, new byte[] {
			40,
			30,
			50,
			55,
			55,
			100
		});
            baseStats.Add(101, new byte[] {
			60,
			50,
			70,
			80,
			80,
			140
		});
            baseStats.Add(102, new byte[] {
			60,
			40,
			80,
			60,
			45,
			40
		});
            baseStats.Add(103, new byte[] {
			95,
			95,
			85,
			125,
			65,
			55
		});
            baseStats.Add(104, new byte[] {
			50,
			50,
			95,
			40,
			50,
			35
		});
            baseStats.Add(105, new byte[] {
			60,
			80,
			110,
			50,
			80,
			45
		});
            baseStats.Add(106, new byte[] {
			50,
			120,
			53,
			35,
			110,
			87
		});
            baseStats.Add(107, new byte[] {
			50,
			105,
			79,
			35,
			110,
			76
		});
            baseStats.Add(108, new byte[] {
			90,
			55,
			75,
			60,
			75,
			30
		});
            baseStats.Add(109, new byte[] {
			40,
			65,
			95,
			60,
			45,
			35
		});
            baseStats.Add(110, new byte[] {
			65,
			90,
			120,
			85,
			70,
			60
		});
            baseStats.Add(111, new byte[] {
			80,
			85,
			95,
			30,
			30,
			25
		});
            baseStats.Add(112, new byte[] {
			105,
			130,
			120,
			45,
			45,
			40
		});
            baseStats.Add(113, new byte[] {
			250,
			5,
			5,
			35,
			105,
			50
		});
            baseStats.Add(114, new byte[] {
			65,
			55,
			115,
			100,
			40,
			60
		});
            baseStats.Add(115, new byte[] {
			105,
			95,
			80,
			40,
			80,
			90
		});
            baseStats.Add(116, new byte[] {
			30,
			40,
			70,
			70,
			25,
			60
		});
            baseStats.Add(117, new byte[] {
			55,
			65,
			95,
			95,
			45,
			85
		});
            baseStats.Add(118, new byte[] {
			45,
			67,
			60,
			35,
			50,
			63
		});
            baseStats.Add(119, new byte[] {
			80,
			92,
			65,
			65,
			80,
			68
		});
            baseStats.Add(120, new byte[] {
			30,
			45,
			55,
			70,
			55,
			85
		});
            baseStats.Add(121, new byte[] {
			60,
			75,
			85,
			100,
			85,
			115
		});
            baseStats.Add(122, new byte[] {
			40,
			45,
			65,
			100,
			120,
			90
		});
            baseStats.Add(123, new byte[] {
			70,
			110,
			80,
			55,
			80,
			105
		});
            baseStats.Add(124, new byte[] {
			65,
			50,
			35,
			115,
			95,
			95
		});
            baseStats.Add(125, new byte[] {
			65,
			83,
			57,
			95,
			85,
			105
		});
            baseStats.Add(126, new byte[] {
			65,
			95,
			57,
			100,
			85,
			93
		});
            baseStats.Add(127, new byte[] {
			65,
			125,
			100,
			55,
			70,
			85
		});
            baseStats.Add(128, new byte[] {
			75,
			100,
			95,
			40,
			70,
			110
		});
            baseStats.Add(129, new byte[] {
			20,
			10,
			55,
			15,
			20,
			80
		});
            baseStats.Add(130, new byte[] {
			95,
			125,
			79,
			60,
			100,
			81
		});
            baseStats.Add(131, new byte[] {
			130,
			85,
			80,
			85,
			95,
			60
		});
            baseStats.Add(132, new byte[] {
			48,
			48,
			48,
			48,
			48,
			48
		});
            baseStats.Add(133, new byte[] {
			55,
			55,
			50,
			45,
			65,
			55
		});
            baseStats.Add(134, new byte[] {
			130,
			65,
			60,
			110,
			95,
			65
		});
            baseStats.Add(135, new byte[] {
			65,
			65,
			60,
			110,
			95,
			130
		});
            baseStats.Add(136, new byte[] {
			65,
			130,
			60,
			95,
			110,
			65
		});
            baseStats.Add(137, new byte[] {
			65,
			60,
			70,
			85,
			75,
			40
		});
            baseStats.Add(138, new byte[] {
			35,
			40,
			100,
			90,
			55,
			35
		});
            baseStats.Add(139, new byte[] {
			70,
			60,
			125,
			115,
			70,
			55
		});
            baseStats.Add(140, new byte[] {
			30,
			80,
			90,
			55,
			45,
			55
		});
            baseStats.Add(141, new byte[] {
			60,
			115,
			105,
			65,
			70,
			80
		});
            baseStats.Add(142, new byte[] {
			80,
			105,
			65,
			60,
			75,
			130
		});
            baseStats.Add(143, new byte[] {
			160,
			110,
			65,
			65,
			110,
			30
		});
            baseStats.Add(144, new byte[] {
			90,
			85,
			100,
			95,
			125,
			85
		});
            baseStats.Add(145, new byte[] {
			90,
			90,
			85,
			125,
			90,
			100
		});
            baseStats.Add(146, new byte[] {
			90,
			100,
			90,
			125,
			85,
			90
		});
            baseStats.Add(147, new byte[] {
			41,
			64,
			45,
			50,
			50,
			50
		});
            baseStats.Add(148, new byte[] {
			61,
			84,
			65,
			70,
			70,
			70
		});
            baseStats.Add(149, new byte[] {
			91,
			134,
			95,
			100,
			100,
			80
		});
            baseStats.Add(150, new byte[] {
			106,
			110,
			90,
			154,
			90,
			130
		});
            baseStats.Add(151, new byte[] {
			100,
			100,
			100,
			100,
			100,
			100
		});
            baseStats.Add(152, new byte[] {
			45,
			49,
			65,
			49,
			65,
			45
		});
            baseStats.Add(153, new byte[] {
			60,
			62,
			80,
			63,
			80,
			60
		});
            baseStats.Add(154, new byte[] {
			80,
			82,
			100,
			83,
			100,
			80
		});
            baseStats.Add(155, new byte[] {
			39,
			52,
			43,
			60,
			50,
			65
		});
            baseStats.Add(156, new byte[] {
			58,
			64,
			58,
			80,
			65,
			80
		});
            baseStats.Add(157, new byte[] {
			78,
			84,
			78,
			109,
			85,
			100
		});
            baseStats.Add(158, new byte[] {
			50,
			65,
			64,
			44,
			48,
			43
		});
            baseStats.Add(159, new byte[] {
			65,
			80,
			80,
			59,
			63,
			58
		});
            baseStats.Add(160, new byte[] {
			85,
			105,
			100,
			79,
			83,
			78
		});
            baseStats.Add(161, new byte[] {
			35,
			46,
			34,
			35,
			45,
			20
		});
            baseStats.Add(162, new byte[] {
			85,
			76,
			64,
			45,
			55,
			90
		});
            baseStats.Add(163, new byte[] {
			60,
			30,
			30,
			36,
			56,
			50
		});
            baseStats.Add(164, new byte[] {
			100,
			50,
			50,
			76,
			96,
			70
		});
            baseStats.Add(165, new byte[] {
			40,
			20,
			30,
			40,
			80,
			55
		});
            baseStats.Add(166, new byte[] {
			55,
			35,
			50,
			55,
			110,
			85
		});
            baseStats.Add(167, new byte[] {
			40,
			60,
			40,
			40,
			40,
			30
		});
            baseStats.Add(168, new byte[] {
			70,
			90,
			70,
			60,
			60,
			40
		});
            baseStats.Add(169, new byte[] {
			85,
			90,
			80,
			70,
			80,
			130
		});
            baseStats.Add(170, new byte[] {
			75,
			38,
			38,
			56,
			56,
			67
		});
            baseStats.Add(171, new byte[] {
			125,
			58,
			58,
			76,
			76,
			67
		});
            baseStats.Add(172, new byte[] {
			20,
			40,
			15,
			35,
			35,
			60
		});
            baseStats.Add(173, new byte[] {
			50,
			25,
			28,
			45,
			55,
			15
		});
            baseStats.Add(174, new byte[] {
			90,
			30,
			15,
			40,
			20,
			15
		});
            baseStats.Add(175, new byte[] {
			35,
			20,
			65,
			40,
			65,
			20
		});
            baseStats.Add(176, new byte[] {
			55,
			40,
			85,
			80,
			105,
			40
		});
            baseStats.Add(177, new byte[] {
			40,
			50,
			45,
			70,
			45,
			70
		});
            baseStats.Add(178, new byte[] {
			65,
			75,
			70,
			95,
			70,
			95
		});
            baseStats.Add(179, new byte[] {
			55,
			40,
			40,
			65,
			45,
			35
		});
            baseStats.Add(180, new byte[] {
			70,
			55,
			55,
			80,
			60,
			45
		});
            baseStats.Add(181, new byte[] {
			90,
			75,
			75,
			115,
			90,
			55
		});
            baseStats.Add(182, new byte[] {
			75,
			80,
			85,
			90,
			100,
			50
		});
            baseStats.Add(183, new byte[] {
			70,
			20,
			50,
			20,
			50,
			40
		});
            baseStats.Add(184, new byte[] {
			100,
			50,
			80,
			50,
			80,
			50
		});
            baseStats.Add(185, new byte[] {
			70,
			100,
			115,
			30,
			65,
			30
		});
            baseStats.Add(186, new byte[] {
			90,
			75,
			75,
			90,
			100,
			70
		});
            baseStats.Add(187, new byte[] {
			35,
			35,
			40,
			35,
			55,
			50
		});
            baseStats.Add(188, new byte[] {
			55,
			45,
			50,
			45,
			65,
			80
		});
            baseStats.Add(189, new byte[] {
			75,
			55,
			70,
			55,
			85,
			110
		});
            baseStats.Add(190, new byte[] {
			55,
			70,
			55,
			40,
			55,
			85
		});
            baseStats.Add(191, new byte[] {
			30,
			30,
			30,
			30,
			30,
			30
		});
            baseStats.Add(192, new byte[] {
			75,
			75,
			55,
			105,
			85,
			30
		});
            baseStats.Add(193, new byte[] {
			65,
			65,
			45,
			75,
			45,
			95
		});
            baseStats.Add(194, new byte[] {
			55,
			45,
			45,
			25,
			25,
			15
		});
            baseStats.Add(195, new byte[] {
			95,
			85,
			85,
			65,
			65,
			35
		});
            baseStats.Add(196, new byte[] {
			65,
			65,
			60,
			130,
			95,
			110
		});
            baseStats.Add(197, new byte[] {
			95,
			65,
			110,
			60,
			130,
			65
		});
            baseStats.Add(198, new byte[] {
			60,
			85,
			42,
			85,
			42,
			91
		});
            baseStats.Add(199, new byte[] {
			95,
			75,
			80,
			100,
			110,
			30
		});
            baseStats.Add(200, new byte[] {
			60,
			60,
			60,
			85,
			85,
			85
		});
            baseStats.Add(201, new byte[] {
			48,
			72,
			48,
			72,
			48,
			48
		});
            baseStats.Add(202, new byte[] {
			190,
			33,
			58,
			33,
			58,
			33
		});
            baseStats.Add(203, new byte[] {
			70,
			80,
			65,
			90,
			65,
			85
		});
            baseStats.Add(204, new byte[] {
			50,
			65,
			90,
			35,
			35,
			15
		});
            baseStats.Add(205, new byte[] {
			75,
			90,
			140,
			60,
			60,
			40
		});
            baseStats.Add(206, new byte[] {
			100,
			70,
			70,
			65,
			65,
			45
		});
            baseStats.Add(207, new byte[] {
			65,
			75,
			105,
			35,
			65,
			85
		});
            baseStats.Add(208, new byte[] {
			75,
			85,
			200,
			55,
			65,
			30
		});
            baseStats.Add(209, new byte[] {
			60,
			80,
			50,
			40,
			40,
			30
		});
            baseStats.Add(210, new byte[] {
			90,
			120,
			75,
			60,
			60,
			45
		});
            baseStats.Add(211, new byte[] {
			65,
			95,
			75,
			55,
			55,
			85
		});
            baseStats.Add(212, new byte[] {
			70,
			130,
			100,
			55,
			80,
			65
		});
            baseStats.Add(213, new byte[] {
			20,
			10,
			230,
			10,
			230,
			5
		});
            baseStats.Add(214, new byte[] {
			80,
			125,
			75,
			40,
			95,
			85
		});
            baseStats.Add(215, new byte[] {
			55,
			95,
			55,
			35,
			75,
			115
		});
            baseStats.Add(216, new byte[] {
			60,
			80,
			50,
			50,
			50,
			40
		});
            baseStats.Add(217, new byte[] {
			90,
			130,
			75,
			75,
			75,
			55
		});
            baseStats.Add(218, new byte[] {
			40,
			40,
			40,
			70,
			40,
			20
		});
            baseStats.Add(219, new byte[] {
			50,
			50,
			120,
			80,
			80,
			30
		});
            baseStats.Add(220, new byte[] {
			50,
			50,
			40,
			30,
			30,
			50
		});
            baseStats.Add(221, new byte[] {
			100,
			100,
			80,
			60,
			60,
			50
		});
            baseStats.Add(222, new byte[] {
			55,
			55,
			85,
			65,
			85,
			35
		});
            baseStats.Add(223, new byte[] {
			35,
			65,
			35,
			65,
			35,
			65
		});
            baseStats.Add(224, new byte[] {
			75,
			105,
			75,
			105,
			75,
			45
		});
            baseStats.Add(225, new byte[] {
			45,
			55,
			45,
			65,
			45,
			75
		});
            baseStats.Add(226, new byte[] {
			65,
			40,
			70,
			80,
			140,
			70
		});
            baseStats.Add(227, new byte[] {
			65,
			80,
			140,
			40,
			70,
			70
		});
            baseStats.Add(228, new byte[] {
			45,
			60,
			30,
			80,
			50,
			65
		});
            baseStats.Add(229, new byte[] {
			75,
			90,
			50,
			110,
			80,
			95
		});
            baseStats.Add(230, new byte[] {
			75,
			95,
			95,
			95,
			95,
			85
		});
            baseStats.Add(231, new byte[] {
			90,
			60,
			60,
			40,
			40,
			40
		});
            baseStats.Add(232, new byte[] {
			90,
			120,
			120,
			60,
			60,
			50
		});
            baseStats.Add(233, new byte[] {
			85,
			80,
			90,
			105,
			95,
			60
		});
            baseStats.Add(234, new byte[] {
			73,
			95,
			62,
			85,
			65,
			85
		});
            baseStats.Add(235, new byte[] {
			55,
			20,
			35,
			20,
			45,
			75
		});
            baseStats.Add(236, new byte[] {
			35,
			35,
			35,
			35,
			35,
			35
		});
            baseStats.Add(237, new byte[] {
			50,
			95,
			95,
			35,
			110,
			70
		});
            baseStats.Add(238, new byte[] {
			45,
			30,
			15,
			85,
			65,
			65
		});
            baseStats.Add(239, new byte[] {
			45,
			63,
			37,
			65,
			55,
			95
		});
            baseStats.Add(240, new byte[] {
			45,
			75,
			37,
			70,
			55,
			83
		});
            baseStats.Add(241, new byte[] {
			95,
			80,
			105,
			40,
			70,
			100
		});
            baseStats.Add(242, new byte[] {
			255,
			10,
			10,
			75,
			135,
			55
		});
            baseStats.Add(243, new byte[] {
			90,
			85,
			75,
			115,
			100,
			115
		});
            baseStats.Add(244, new byte[] {
			115,
			115,
			85,
			90,
			75,
			100
		});
            baseStats.Add(245, new byte[] {
			100,
			75,
			115,
			90,
			115,
			85
		});
            baseStats.Add(246, new byte[] {
			50,
			64,
			50,
			45,
			50,
			41
		});
            baseStats.Add(247, new byte[] {
			70,
			84,
			70,
			65,
			70,
			51
		});
            baseStats.Add(248, new byte[] {
			100,
			134,
			110,
			95,
			100,
			61
		});
            baseStats.Add(249, new byte[] {
			106,
			90,
			130,
			90,
			154,
			110
		});
            baseStats.Add(250, new byte[] {
			106,
			130,
			90,
			110,
			154,
			90
		});
            baseStats.Add(251, new byte[] {
			100,
			100,
			100,
			100,
			100,
			100
		});
            baseStats.Add(252, new byte[] {
			40,
			45,
			35,
			65,
			55,
			70
		});
            baseStats.Add(253, new byte[] {
			50,
			65,
			45,
			85,
			65,
			95
		});
            baseStats.Add(254, new byte[] {
			70,
			85,
			65,
			105,
			85,
			120
		});
            baseStats.Add(255, new byte[] {
			45,
			60,
			40,
			70,
			50,
			45
		});
            baseStats.Add(256, new byte[] {
			60,
			85,
			60,
			85,
			60,
			55
		});
            baseStats.Add(257, new byte[] {
			80,
			120,
			70,
			110,
			70,
			80
		});
            baseStats.Add(258, new byte[] {
			50,
			70,
			50,
			50,
			50,
			40
		});
            baseStats.Add(259, new byte[] {
			70,
			85,
			70,
			60,
			70,
			50
		});
            baseStats.Add(260, new byte[] {
			100,
			110,
			90,
			85,
			90,
			60
		});
            baseStats.Add(261, new byte[] {
			35,
			55,
			35,
			30,
			30,
			35
		});
            baseStats.Add(262, new byte[] {
			70,
			90,
			70,
			60,
			60,
			70
		});
            baseStats.Add(263, new byte[] {
			38,
			30,
			41,
			30,
			41,
			60
		});
            baseStats.Add(264, new byte[] {
			78,
			70,
			61,
			50,
			61,
			100
		});
            baseStats.Add(265, new byte[] {
			45,
			45,
			35,
			20,
			30,
			20
		});
            baseStats.Add(266, new byte[] {
			50,
			35,
			55,
			25,
			25,
			15
		});
            baseStats.Add(267, new byte[] {
			60,
			70,
			50,
			90,
			50,
			65
		});
            baseStats.Add(268, new byte[] {
			50,
			35,
			55,
			25,
			25,
			15
		});
            baseStats.Add(269, new byte[] {
			60,
			50,
			70,
			50,
			90,
			65
		});
            baseStats.Add(270, new byte[] {
			40,
			30,
			30,
			40,
			50,
			30
		});
            baseStats.Add(271, new byte[] {
			60,
			50,
			50,
			60,
			70,
			50
		});
            baseStats.Add(272, new byte[] {
			80,
			70,
			70,
			90,
			100,
			70
		});
            baseStats.Add(273, new byte[] {
			40,
			40,
			50,
			30,
			30,
			30
		});
            baseStats.Add(274, new byte[] {
			70,
			70,
			40,
			60,
			40,
			60
		});
            baseStats.Add(275, new byte[] {
			90,
			100,
			60,
			90,
			60,
			80
		});
            baseStats.Add(276, new byte[] {
			40,
			55,
			30,
			30,
			30,
			85
		});
            baseStats.Add(277, new byte[] {
			60,
			85,
			60,
			50,
			50,
			125
		});
            baseStats.Add(278, new byte[] {
			40,
			30,
			30,
			55,
			30,
			85
		});
            baseStats.Add(279, new byte[] {
			60,
			50,
			100,
			85,
			70,
			65
		});
            baseStats.Add(280, new byte[] {
			28,
			25,
			25,
			45,
			35,
			40
		});
            baseStats.Add(281, new byte[] {
			38,
			35,
			35,
			65,
			55,
			50
		});
            baseStats.Add(282, new byte[] {
			68,
			65,
			65,
			125,
			115,
			80
		});
            baseStats.Add(283, new byte[] {
			40,
			30,
			32,
			50,
			52,
			65
		});
            baseStats.Add(284, new byte[] {
			70,
			60,
			62,
			80,
			82,
			60
		});
            baseStats.Add(285, new byte[] {
			60,
			40,
			60,
			40,
			60,
			35
		});
            baseStats.Add(286, new byte[] {
			60,
			130,
			80,
			60,
			60,
			70
		});
            baseStats.Add(287, new byte[] {
			60,
			60,
			60,
			35,
			35,
			30
		});
            baseStats.Add(288, new byte[] {
			80,
			80,
			80,
			55,
			55,
			90
		});
            baseStats.Add(289, new byte[] {
			150,
			160,
			100,
			95,
			65,
			100
		});
            baseStats.Add(290, new byte[] {
			31,
			45,
			90,
			30,
			30,
			40
		});
            baseStats.Add(291, new byte[] {
			61,
			90,
			45,
			50,
			50,
			160
		});
            baseStats.Add(292, new byte[] {
			1,
			90,
			45,
			30,
			30,
			40
		});
            baseStats.Add(293, new byte[] {
			64,
			51,
			23,
			51,
			23,
			28
		});
            baseStats.Add(294, new byte[] {
			84,
			71,
			43,
			71,
			43,
			48
		});
            baseStats.Add(295, new byte[] {
			104,
			91,
			63,
			91,
			63,
			68
		});
            baseStats.Add(296, new byte[] {
			72,
			60,
			30,
			20,
			30,
			25
		});
            baseStats.Add(297, new byte[] {
			144,
			120,
			60,
			40,
			60,
			50
		});
            baseStats.Add(298, new byte[] {
			50,
			20,
			40,
			20,
			40,
			20
		});
            baseStats.Add(299, new byte[] {
			30,
			45,
			135,
			45,
			90,
			30
		});
            baseStats.Add(300, new byte[] {
			50,
			45,
			45,
			35,
			35,
			50
		});
            baseStats.Add(301, new byte[] {
			70,
			65,
			65,
			55,
			55,
			70
		});
            baseStats.Add(302, new byte[] {
			50,
			75,
			75,
			65,
			65,
			50
		});
            baseStats.Add(303, new byte[] {
			50,
			85,
			85,
			55,
			55,
			50
		});
            baseStats.Add(304, new byte[] {
			50,
			70,
			100,
			40,
			40,
			30
		});
            baseStats.Add(305, new byte[] {
			60,
			90,
			140,
			50,
			50,
			40
		});
            baseStats.Add(306, new byte[] {
			70,
			110,
			180,
			60,
			60,
			50
		});
            baseStats.Add(307, new byte[] {
			30,
			40,
			55,
			40,
			55,
			60
		});
            baseStats.Add(308, new byte[] {
			60,
			60,
			75,
			60,
			75,
			80
		});
            baseStats.Add(309, new byte[] {
			40,
			45,
			40,
			65,
			40,
			65
		});
            baseStats.Add(310, new byte[] {
			70,
			75,
			60,
			105,
			60,
			105
		});
            baseStats.Add(311, new byte[] {
			60,
			50,
			40,
			85,
			75,
			95
		});
            baseStats.Add(312, new byte[] {
			60,
			40,
			50,
			75,
			85,
			95
		});
            baseStats.Add(313, new byte[] {
			65,
			73,
			55,
			47,
			75,
			85
		});
            baseStats.Add(314, new byte[] {
			65,
			47,
			55,
			73,
			75,
			85
		});
            baseStats.Add(315, new byte[] {
			50,
			60,
			45,
			100,
			80,
			65
		});
            baseStats.Add(316, new byte[] {
			70,
			43,
			53,
			43,
			53,
			40
		});
            baseStats.Add(317, new byte[] {
			100,
			73,
			83,
			73,
			83,
			55
		});
            baseStats.Add(318, new byte[] {
			45,
			90,
			20,
			65,
			20,
			65
		});
            baseStats.Add(319, new byte[] {
			70,
			120,
			40,
			95,
			40,
			95
		});
            baseStats.Add(320, new byte[] {
			130,
			70,
			35,
			70,
			35,
			60
		});
            baseStats.Add(321, new byte[] {
			170,
			90,
			45,
			90,
			45,
			60
		});
            baseStats.Add(322, new byte[] {
			60,
			60,
			40,
			65,
			45,
			35
		});
            baseStats.Add(323, new byte[] {
			70,
			100,
			70,
			105,
			75,
			40
		});
            baseStats.Add(324, new byte[] {
			70,
			85,
			140,
			85,
			70,
			20
		});
            baseStats.Add(325, new byte[] {
			60,
			25,
			35,
			70,
			80,
			60
		});
            baseStats.Add(326, new byte[] {
			80,
			45,
			65,
			90,
			110,
			80
		});
            baseStats.Add(327, new byte[] {
			60,
			60,
			60,
			60,
			60,
			60
		});
            baseStats.Add(328, new byte[] {
			45,
			100,
			45,
			45,
			45,
			10
		});
            baseStats.Add(329, new byte[] {
			50,
			70,
			50,
			50,
			50,
			70
		});
            baseStats.Add(330, new byte[] {
			80,
			100,
			80,
			80,
			80,
			100
		});
            baseStats.Add(331, new byte[] {
			50,
			85,
			40,
			85,
			40,
			35
		});
            baseStats.Add(332, new byte[] {
			70,
			115,
			60,
			115,
			60,
			55
		});
            baseStats.Add(333, new byte[] {
			45,
			40,
			60,
			40,
			75,
			50
		});
            baseStats.Add(334, new byte[] {
			75,
			70,
			90,
			70,
			105,
			80
		});
            baseStats.Add(335, new byte[] {
			73,
			115,
			60,
			60,
			60,
			90
		});
            baseStats.Add(336, new byte[] {
			73,
			100,
			60,
			100,
			60,
			65
		});
            baseStats.Add(337, new byte[] {
			70,
			55,
			65,
			95,
			85,
			70
		});
            baseStats.Add(338, new byte[] {
			70,
			95,
			85,
			55,
			65,
			70
		});
            baseStats.Add(339, new byte[] {
			50,
			48,
			43,
			46,
			41,
			60
		});
            baseStats.Add(340, new byte[] {
			110,
			78,
			73,
			76,
			71,
			60
		});
            baseStats.Add(341, new byte[] {
			43,
			80,
			65,
			50,
			35,
			35
		});
            baseStats.Add(342, new byte[] {
			63,
			120,
			85,
			90,
			55,
			55
		});
            baseStats.Add(343, new byte[] {
			40,
			40,
			55,
			40,
			70,
			55
		});
            baseStats.Add(344, new byte[] {
			60,
			70,
			105,
			70,
			120,
			75
		});
            baseStats.Add(345, new byte[] {
			66,
			41,
			77,
			61,
			87,
			23
		});
            baseStats.Add(346, new byte[] {
			86,
			81,
			97,
			81,
			107,
			43
		});
            baseStats.Add(347, new byte[] {
			45,
			95,
			50,
			40,
			50,
			75
		});
            baseStats.Add(348, new byte[] {
			75,
			125,
			100,
			70,
			80,
			45
		});
            baseStats.Add(349, new byte[] {
			20,
			15,
			20,
			10,
			55,
			80
		});
            baseStats.Add(350, new byte[] {
			95,
			60,
			79,
			100,
			125,
			81
		});
            baseStats.Add(351, new byte[] {
			70,
			70,
			70,
			70,
			70,
			70
		});
            baseStats.Add(352, new byte[] {
			60,
			90,
			70,
			60,
			120,
			40
		});
            baseStats.Add(353, new byte[] {
			44,
			75,
			35,
			63,
			33,
			45
		});
            baseStats.Add(354, new byte[] {
			64,
			115,
			65,
			83,
			63,
			65
		});
            baseStats.Add(355, new byte[] {
			20,
			40,
			90,
			30,
			90,
			25
		});
            baseStats.Add(356, new byte[] {
			40,
			70,
			130,
			60,
			130,
			25
		});
            baseStats.Add(357, new byte[] {
			99,
			68,
			83,
			72,
			87,
			51
		});
            baseStats.Add(358, new byte[] {
			65,
			50,
			70,
			95,
			80,
			65
		});
            baseStats.Add(359, new byte[] {
			65,
			130,
			60,
			75,
			60,
			75
		});
            baseStats.Add(360, new byte[] {
			95,
			23,
			48,
			23,
			48,
			23
		});
            baseStats.Add(361, new byte[] {
			50,
			50,
			50,
			50,
			50,
			50
		});
            baseStats.Add(362, new byte[] {
			80,
			80,
			80,
			80,
			80,
			80
		});
            baseStats.Add(363, new byte[] {
			70,
			40,
			50,
			55,
			50,
			25
		});
            baseStats.Add(364, new byte[] {
			90,
			60,
			70,
			75,
			70,
			45
		});
            baseStats.Add(365, new byte[] {
			110,
			80,
			90,
			95,
			90,
			65
		});
            baseStats.Add(366, new byte[] {
			35,
			64,
			85,
			74,
			55,
			32
		});
            baseStats.Add(367, new byte[] {
			55,
			104,
			105,
			94,
			75,
			52
		});
            baseStats.Add(368, new byte[] {
			55,
			84,
			105,
			114,
			75,
			52
		});
            baseStats.Add(369, new byte[] {
			100,
			90,
			130,
			45,
			65,
			55
		});
            baseStats.Add(370, new byte[] {
			43,
			30,
			55,
			40,
			65,
			97
		});
            baseStats.Add(371, new byte[] {
			45,
			75,
			60,
			40,
			30,
			50
		});
            baseStats.Add(372, new byte[] {
			65,
			95,
			100,
			60,
			50,
			50
		});
            baseStats.Add(373, new byte[] {
			95,
			135,
			80,
			110,
			80,
			100
		});
            baseStats.Add(374, new byte[] {
			40,
			55,
			80,
			35,
			60,
			30
		});
            baseStats.Add(375, new byte[] {
			60,
			75,
			100,
			55,
			80,
			50
		});
            baseStats.Add(376, new byte[] {
			80,
			135,
			130,
			95,
			90,
			70
		});
            baseStats.Add(377, new byte[] {
			80,
			100,
			200,
			50,
			100,
			50
		});
            baseStats.Add(378, new byte[] {
			80,
			50,
			100,
			100,
			200,
			50
		});
            baseStats.Add(379, new byte[] {
			80,
			75,
			150,
			75,
			150,
			50
		});
            baseStats.Add(380, new byte[] {
			80,
			80,
			90,
			110,
			130,
			110
		});
            baseStats.Add(381, new byte[] {
			80,
			90,
			80,
			130,
			110,
			110
		});
            baseStats.Add(382, new byte[] {
			100,
			100,
			90,
			150,
			140,
			90
		});
            baseStats.Add(383, new byte[] {
			100,
			150,
			140,
			100,
			90,
			90
		});
            baseStats.Add(384, new byte[] {
			105,
			150,
			90,
			150,
			90,
			95
		});
            baseStats.Add(385, new byte[] {
			100,
			100,
			100,
			100,
			100,
			100
		});
            baseStats.Add(386, new byte[] {
			50,
			150,
			50,
			150,
			50,
			150
		});
            baseStats.Add(387, new byte[] {
			55,
			68,
			64,
			45,
			55,
			31
		});
            baseStats.Add(388, new byte[] {
			75,
			89,
			85,
			55,
			65,
			36
		});
            baseStats.Add(389, new byte[] {
			95,
			109,
			105,
			75,
			85,
			56
		});
            baseStats.Add(390, new byte[] {
			44,
			58,
			44,
			58,
			44,
			61
		});
            baseStats.Add(391, new byte[] {
			64,
			78,
			52,
			78,
			52,
			81
		});
            baseStats.Add(392, new byte[] {
			76,
			104,
			71,
			104,
			71,
			108
		});
            baseStats.Add(393, new byte[] {
			53,
			51,
			53,
			61,
			56,
			40
		});
            baseStats.Add(394, new byte[] {
			64,
			66,
			68,
			81,
			76,
			50
		});
            baseStats.Add(395, new byte[] {
			84,
			86,
			88,
			111,
			101,
			60
		});
            baseStats.Add(396, new byte[] {
			40,
			55,
			30,
			30,
			30,
			60
		});
            baseStats.Add(397, new byte[] {
			55,
			75,
			50,
			40,
			40,
			80
		});
            baseStats.Add(398, new byte[] {
			85,
			120,
			70,
			50,
			50,
			100
		});
            baseStats.Add(399, new byte[] {
			59,
			45,
			40,
			35,
			40,
			31
		});
            baseStats.Add(400, new byte[] {
			79,
			85,
			60,
			55,
			60,
			71
		});
            baseStats.Add(401, new byte[] {
			37,
			25,
			41,
			25,
			41,
			25
		});
            baseStats.Add(402, new byte[] {
			77,
			85,
			51,
			55,
			51,
			65
		});
            baseStats.Add(403, new byte[] {
			45,
			65,
			34,
			40,
			34,
			45
		});
            baseStats.Add(404, new byte[] {
			60,
			85,
			49,
			60,
			49,
			60
		});
            baseStats.Add(405, new byte[] {
			80,
			120,
			79,
			95,
			79,
			70
		});
            baseStats.Add(406, new byte[] {
			40,
			30,
			35,
			50,
			70,
			55
		});
            baseStats.Add(407, new byte[] {
			60,
			70,
			55,
			125,
			105,
			90
		});
            baseStats.Add(408, new byte[] {
			67,
			125,
			40,
			30,
			30,
			58
		});
            baseStats.Add(409, new byte[] {
			97,
			165,
			60,
			65,
			50,
			58
		});
            baseStats.Add(410, new byte[] {
			30,
			42,
			118,
			42,
			88,
			30
		});
            baseStats.Add(411, new byte[] {
			60,
			52,
			168,
			47,
			138,
			30
		});
            baseStats.Add(412, new byte[] {
			40,
			29,
			45,
			29,
			45,
			36
		});
            baseStats.Add(413, new byte[] {
			60,
			59,
			85,
			79,
			105,
			36
		});
            baseStats.Add(414, new byte[] {
			70,
			94,
			50,
			94,
			50,
			66
		});
            baseStats.Add(415, new byte[] {
			30,
			30,
			42,
			30,
			42,
			70
		});
            baseStats.Add(416, new byte[] {
			70,
			80,
			102,
			80,
			102,
			40
		});
            baseStats.Add(417, new byte[] {
			60,
			45,
			70,
			45,
			90,
			95
		});
            baseStats.Add(418, new byte[] {
			55,
			65,
			35,
			60,
			30,
			85
		});
            baseStats.Add(419, new byte[] {
			85,
			105,
			55,
			85,
			50,
			115
		});
            baseStats.Add(420, new byte[] {
			45,
			35,
			45,
			62,
			53,
			35
		});
            baseStats.Add(421, new byte[] {
			70,
			60,
			70,
			87,
			78,
			85
		});
            baseStats.Add(422, new byte[] {
			76,
			48,
			48,
			57,
			62,
			34
		});
            baseStats.Add(423, new byte[] {
			111,
			83,
			68,
			92,
			82,
			39
		});
            baseStats.Add(424, new byte[] {
			75,
			100,
			66,
			60,
			66,
			115
		});
            baseStats.Add(425, new byte[] {
			90,
			50,
			34,
			60,
			44,
			70
		});
            baseStats.Add(426, new byte[] {
			150,
			80,
			44,
			90,
			54,
			80
		});
            baseStats.Add(427, new byte[] {
			55,
			66,
			44,
			44,
			56,
			85
		});
            baseStats.Add(428, new byte[] {
			65,
			76,
			84,
			54,
			96,
			105
		});
            baseStats.Add(429, new byte[] {
			60,
			60,
			60,
			105,
			105,
			105
		});
            baseStats.Add(430, new byte[] {
			100,
			125,
			52,
			105,
			52,
			71
		});
            baseStats.Add(431, new byte[] {
			49,
			55,
			42,
			42,
			37,
			85
		});
            baseStats.Add(432, new byte[] {
			71,
			82,
			64,
			64,
			59,
			112
		});
            baseStats.Add(433, new byte[] {
			45,
			30,
			50,
			65,
			50,
			45
		});
            baseStats.Add(434, new byte[] {
			63,
			63,
			47,
			41,
			41,
			74
		});
            baseStats.Add(435, new byte[] {
			103,
			93,
			67,
			71,
			61,
			84
		});
            baseStats.Add(436, new byte[] {
			57,
			24,
			86,
			24,
			86,
			23
		});
            baseStats.Add(437, new byte[] {
			67,
			89,
			116,
			79,
			116,
			33
		});
            baseStats.Add(438, new byte[] {
			50,
			80,
			95,
			10,
			45,
			10
		});
            baseStats.Add(439, new byte[] {
			20,
			25,
			45,
			70,
			90,
			60
		});
            baseStats.Add(440, new byte[] {
			100,
			5,
			5,
			15,
			65,
			30
		});
            baseStats.Add(441, new byte[] {
			76,
			65,
			45,
			92,
			42,
			91
		});
            baseStats.Add(442, new byte[] {
			50,
			92,
			108,
			92,
			108,
			35
		});
            baseStats.Add(443, new byte[] {
			58,
			70,
			45,
			40,
			45,
			42
		});
            baseStats.Add(444, new byte[] {
			68,
			90,
			65,
			50,
			55,
			82
		});
            baseStats.Add(445, new byte[] {
			108,
			130,
			95,
			80,
			85,
			102
		});
            baseStats.Add(446, new byte[] {
			135,
			85,
			40,
			40,
			85,
			5
		});
            baseStats.Add(447, new byte[] {
			40,
			70,
			40,
			35,
			40,
			60
		});
            baseStats.Add(448, new byte[] {
			70,
			110,
			70,
			115,
			70,
			90
		});
            baseStats.Add(449, new byte[] {
			68,
			72,
			78,
			38,
			42,
			32
		});
            baseStats.Add(450, new byte[] {
			108,
			112,
			118,
			68,
			72,
			47
		});
            baseStats.Add(451, new byte[] {
			40,
			50,
			90,
			30,
			55,
			65
		});
            baseStats.Add(452, new byte[] {
			70,
			90,
			110,
			60,
			75,
			95
		});
            baseStats.Add(453, new byte[] {
			48,
			61,
			40,
			61,
			40,
			50
		});
            baseStats.Add(454, new byte[] {
			83,
			106,
			65,
			86,
			65,
			85
		});
            baseStats.Add(455, new byte[] {
			74,
			100,
			72,
			90,
			72,
			46
		});
            baseStats.Add(456, new byte[] {
			49,
			49,
			56,
			49,
			61,
			66
		});
            baseStats.Add(457, new byte[] {
			69,
			69,
			76,
			69,
			86,
			91
		});
            baseStats.Add(458, new byte[] {
			45,
			20,
			50,
			60,
			120,
			50
		});
            baseStats.Add(459, new byte[] {
			60,
			62,
			50,
			62,
			60,
			40
		});
            baseStats.Add(460, new byte[] {
			90,
			92,
			75,
			92,
			85,
			60
		});
            baseStats.Add(461, new byte[] {
			70,
			120,
			65,
			45,
			85,
			125
		});
            baseStats.Add(462, new byte[] {
			70,
			70,
			115,
			130,
			90,
			60
		});
            baseStats.Add(463, new byte[] {
			110,
			85,
			95,
			80,
			95,
			50
		});
            baseStats.Add(464, new byte[] {
			115,
			140,
			130,
			55,
			55,
			40
		});
            baseStats.Add(465, new byte[] {
			100,
			100,
			125,
			110,
			50,
			50
		});
            baseStats.Add(466, new byte[] {
			75,
			123,
			67,
			95,
			85,
			95
		});
            baseStats.Add(467, new byte[] {
			75,
			95,
			67,
			125,
			95,
			83
		});
            baseStats.Add(468, new byte[] {
			85,
			50,
			95,
			120,
			115,
			80
		});
            baseStats.Add(469, new byte[] {
			86,
			76,
			86,
			116,
			56,
			95
		});
            baseStats.Add(470, new byte[] {
			65,
			110,
			130,
			60,
			65,
			95
		});
            baseStats.Add(471, new byte[] {
			65,
			60,
			110,
			130,
			95,
			65
		});
            baseStats.Add(472, new byte[] {
			75,
			95,
			125,
			45,
			75,
			95
		});
            baseStats.Add(473, new byte[] {
			110,
			130,
			80,
			70,
			60,
			80
		});
            baseStats.Add(474, new byte[] {
			85,
			80,
			70,
			135,
			75,
			90
		});
            baseStats.Add(475, new byte[] {
			68,
			125,
			65,
			65,
			115,
			80
		});
            baseStats.Add(476, new byte[] {
			60,
			55,
			145,
			75,
			150,
			40
		});
            baseStats.Add(477, new byte[] {
			45,
			100,
			135,
			65,
			135,
			45
		});
            baseStats.Add(478, new byte[] {
			70,
			80,
			70,
			80,
			70,
			110
		});
            baseStats.Add(479, new byte[] {
			50,
			50,
			77,
			95,
			77,
			91
		});
            baseStats.Add(480, new byte[] {
			75,
			75,
			130,
			75,
			130,
			95
		});
            baseStats.Add(481, new byte[] {
			80,
			105,
			105,
			105,
			105,
			80
		});
            baseStats.Add(482, new byte[] {
			75,
			125,
			70,
			125,
			70,
			115
		});
            baseStats.Add(483, new byte[] {
			100,
			120,
			120,
			150,
			100,
			90
		});
            baseStats.Add(484, new byte[] {
			90,
			120,
			100,
			150,
			120,
			100
		});
            baseStats.Add(485, new byte[] {
			91,
			90,
			106,
			130,
			106,
			77
		});
            baseStats.Add(486, new byte[] {
			110,
			160,
			110,
			80,
			110,
			100
		});
            baseStats.Add(487, new byte[] {
			150,
			100,
			120,
			100,
			120,
			90
		});
            baseStats.Add(488, new byte[] {
			120,
			70,
			120,
			75,
			130,
			85
		});
            baseStats.Add(489, new byte[] {
			80,
			80,
			80,
			80,
			80,
			80
		});
            baseStats.Add(490, new byte[] {
			100,
			100,
			100,
			100,
			100,
			100
		});
            baseStats.Add(491, new byte[] {
			70,
			90,
			90,
			135,
			90,
			125
		});
            baseStats.Add(492, new byte[] {
			100,
			100,
			100,
			100,
			100,
			100
		});
            baseStats.Add(493, new byte[] {
			120,
			120,
			120,
			120,
			120,
			120
		});
            baseStats.Add(494, new byte[] {
			100,
			100,
			100,
			100,
			100,
			100
		});
            baseStats.Add(495, new byte[] {
			45,
			45,
			55,
			45,
			55,
			63
		});
            baseStats.Add(496, new byte[] {
			60,
			60,
			75,
			60,
			75,
			83
		});
            baseStats.Add(497, new byte[] {
			75,
			75,
			95,
			75,
			95,
			113
		});
            baseStats.Add(498, new byte[] {
			65,
			63,
			45,
			45,
			45,
			45
		});
            baseStats.Add(499, new byte[] {
			90,
			93,
			55,
			70,
			55,
			55
		});
            baseStats.Add(500, new byte[] {
			110,
			123,
			65,
			100,
			65,
			65
		});
            baseStats.Add(501, new byte[] {
			55,
			55,
			45,
			63,
			45,
			45
		});
            baseStats.Add(502, new byte[] {
			75,
			75,
			60,
			83,
			60,
			60
		});
            baseStats.Add(503, new byte[] {
			95,
			100,
			85,
			108,
			70,
			70
		});
            baseStats.Add(504, new byte[] {
			45,
			55,
			39,
			35,
			39,
			42
		});
            baseStats.Add(505, new byte[] {
			60,
			85,
			69,
			60,
			69,
			77
		});
            baseStats.Add(506, new byte[] {
			45,
			60,
			45,
			25,
			45,
			55
		});
            baseStats.Add(507, new byte[] {
			65,
			80,
			65,
			35,
			65,
			60
		});
            baseStats.Add(508, new byte[] {
			85,
			100,
			90,
			45,
			90,
			80
		});
            baseStats.Add(509, new byte[] {
			41,
			50,
			37,
			50,
			37,
			66
		});
            baseStats.Add(510, new byte[] {
			64,
			88,
			50,
			88,
			50,
			106
		});
            baseStats.Add(511, new byte[] {
			50,
			53,
			48,
			53,
			48,
			64
		});
            baseStats.Add(512, new byte[] {
			75,
			98,
			63,
			98,
			63,
			101
		});
            baseStats.Add(513, new byte[] {
			50,
			53,
			48,
			53,
			48,
			64
		});
            baseStats.Add(514, new byte[] {
			75,
			98,
			63,
			98,
			63,
			101
		});
            baseStats.Add(515, new byte[] {
			50,
			53,
			48,
			53,
			48,
			64
		});
            baseStats.Add(516, new byte[] {
			75,
			98,
			63,
			98,
			63,
			101
		});
            baseStats.Add(517, new byte[] {
			76,
			25,
			45,
			67,
			55,
			24
		});
            baseStats.Add(518, new byte[] {
			116,
			55,
			85,
			107,
			95,
			29
		});
            baseStats.Add(519, new byte[] {
			50,
			55,
			50,
			36,
			30,
			43
		});
            baseStats.Add(520, new byte[] {
			62,
			77,
			62,
			50,
			42,
			65
		});
            baseStats.Add(521, new byte[] {
			80,
			105,
			80,
			65,
			55,
			93
		});
            baseStats.Add(522, new byte[] {
			45,
			60,
			32,
			50,
			32,
			76
		});
            baseStats.Add(523, new byte[] {
			75,
			100,
			63,
			80,
			63,
			116
		});
            baseStats.Add(524, new byte[] {
			55,
			75,
			85,
			25,
			25,
			15
		});
            baseStats.Add(525, new byte[] {
			70,
			105,
			105,
			50,
			40,
			20
		});
            baseStats.Add(526, new byte[] {
			85,
			135,
			130,
			60,
			70,
			25
		});
            baseStats.Add(527, new byte[] {
			55,
			45,
			43,
			55,
			43,
			72
		});
            baseStats.Add(528, new byte[] {
			67,
			57,
			55,
			77,
			55,
			114
		});
            baseStats.Add(529, new byte[] {
			60,
			85,
			40,
			30,
			45,
			68
		});
            baseStats.Add(530, new byte[] {
			110,
			135,
			60,
			50,
			65,
			88
		});
            baseStats.Add(531, new byte[] {
			103,
			60,
			86,
			60,
			86,
			50
		});
            baseStats.Add(532, new byte[] {
			75,
			80,
			55,
			25,
			35,
			35
		});
            baseStats.Add(533, new byte[] {
			85,
			105,
			85,
			40,
			50,
			40
		});
            baseStats.Add(534, new byte[] {
			105,
			140,
			95,
			55,
			65,
			45
		});
            baseStats.Add(535, new byte[] {
			50,
			50,
			40,
			50,
			40,
			64
		});
            baseStats.Add(536, new byte[] {
			75,
			65,
			55,
			65,
			55,
			69
		});
            baseStats.Add(537, new byte[] {
			105,
			85,
			75,
			85,
			75,
			74
		});
            baseStats.Add(538, new byte[] {
			120,
			100,
			85,
			30,
			85,
			45
		});
            baseStats.Add(539, new byte[] {
			75,
			125,
			75,
			30,
			75,
			85
		});
            baseStats.Add(540, new byte[] {
			45,
			53,
			70,
			40,
			60,
			42
		});
            baseStats.Add(541, new byte[] {
			55,
			63,
			90,
			50,
			80,
			42
		});
            baseStats.Add(542, new byte[] {
			75,
			103,
			80,
			70,
			70,
			92
		});
            baseStats.Add(543, new byte[] {
			30,
			45,
			59,
			30,
			39,
			57
		});
            baseStats.Add(544, new byte[] {
			40,
			55,
			99,
			40,
			79,
			47
		});
            baseStats.Add(545, new byte[] {
			60,
			90,
			89,
			55,
			69,
			112
		});
            baseStats.Add(546, new byte[] {
			40,
			27,
			60,
			37,
			50,
			66
		});
            baseStats.Add(547, new byte[] {
			60,
			67,
			85,
			77,
			75,
			116
		});
            baseStats.Add(548, new byte[] {
			45,
			35,
			50,
			70,
			50,
			30
		});
            baseStats.Add(549, new byte[] {
			70,
			60,
			75,
			110,
			75,
			90
		});
            baseStats.Add(550, new byte[] {
			70,
			92,
			65,
			80,
			55,
			98
		});
            baseStats.Add(551, new byte[] {
			50,
			72,
			35,
			35,
			35,
			65
		});
            baseStats.Add(552, new byte[] {
			60,
			82,
			45,
			45,
			45,
			74
		});
            baseStats.Add(553, new byte[] {
			95,
			117,
			70,
			65,
			70,
			92
		});
            baseStats.Add(554, new byte[] {
			70,
			90,
			45,
			15,
			45,
			50
		});
            baseStats.Add(555, new byte[] {
			105,
			140,
			55,
			30,
			55,
			95
		});
            baseStats.Add(556, new byte[] {
			75,
			86,
			67,
			106,
			67,
			60
		});
            baseStats.Add(557, new byte[] {
			50,
			65,
			85,
			35,
			35,
			55
		});
            baseStats.Add(558, new byte[] {
			70,
			95,
			125,
			65,
			75,
			45
		});
            baseStats.Add(559, new byte[] {
			50,
			75,
			70,
			35,
			70,
			48
		});
            baseStats.Add(560, new byte[] {
			65,
			90,
			115,
			45,
			115,
			58
		});
            baseStats.Add(561, new byte[] {
			72,
			58,
			80,
			103,
			80,
			97
		});
            baseStats.Add(562, new byte[] {
			38,
			30,
			85,
			55,
			65,
			30
		});
            baseStats.Add(563, new byte[] {
			58,
			50,
			145,
			95,
			105,
			30
		});
            baseStats.Add(564, new byte[] {
			54,
			78,
			103,
			53,
			45,
			22
		});
            baseStats.Add(565, new byte[] {
			74,
			108,
			133,
			83,
			65,
			32
		});
            baseStats.Add(566, new byte[] {
			55,
			112,
			45,
			74,
			45,
			70
		});
            baseStats.Add(567, new byte[] {
			75,
			140,
			65,
			112,
			65,
			110
		});
            baseStats.Add(568, new byte[] {
			50,
			50,
			62,
			40,
			62,
			65
		});
            baseStats.Add(569, new byte[] {
			80,
			95,
			82,
			60,
			82,
			75
		});
            baseStats.Add(570, new byte[] {
			40,
			65,
			40,
			80,
			40,
			65
		});
            baseStats.Add(571, new byte[] {
			60,
			105,
			60,
			120,
			60,
			105
		});
            baseStats.Add(572, new byte[] {
			55,
			50,
			40,
			40,
			40,
			75
		});
            baseStats.Add(573, new byte[] {
			75,
			95,
			60,
			65,
			60,
			115
		});
            baseStats.Add(574, new byte[] {
			45,
			30,
			50,
			55,
			65,
			45
		});
            baseStats.Add(575, new byte[] {
			60,
			45,
			70,
			75,
			85,
			55
		});
            baseStats.Add(576, new byte[] {
			70,
			55,
			95,
			95,
			110,
			65
		});
            baseStats.Add(577, new byte[] {
			45,
			30,
			40,
			105,
			50,
			20
		});
            baseStats.Add(578, new byte[] {
			65,
			40,
			50,
			125,
			60,
			30
		});
            baseStats.Add(579, new byte[] {
			110,
			65,
			75,
			125,
			85,
			30
		});
            baseStats.Add(580, new byte[] {
			62,
			44,
			50,
			44,
			50,
			55
		});
            baseStats.Add(581, new byte[] {
			75,
			87,
			63,
			87,
			63,
			98
		});
            baseStats.Add(582, new byte[] {
			36,
			50,
			50,
			65,
			60,
			44
		});
            baseStats.Add(583, new byte[] {
			51,
			65,
			65,
			80,
			75,
			59
		});
            baseStats.Add(584, new byte[] {
			71,
			95,
			85,
			110,
			95,
			79
		});
            baseStats.Add(585, new byte[] {
			60,
			60,
			50,
			40,
			50,
			75
		});
            baseStats.Add(586, new byte[] {
			80,
			100,
			70,
			60,
			70,
			95
		});
            baseStats.Add(587, new byte[] {
			55,
			75,
			60,
			75,
			60,
			103
		});
            baseStats.Add(588, new byte[] {
			50,
			75,
			45,
			40,
			45,
			60
		});
            baseStats.Add(589, new byte[] {
			70,
			135,
			105,
			60,
			105,
			20
		});
            baseStats.Add(590, new byte[] {
			69,
			55,
			45,
			55,
			55,
			15
		});
            baseStats.Add(591, new byte[] {
			114,
			85,
			70,
			85,
			80,
			30
		});
            baseStats.Add(592, new byte[] {
			55,
			40,
			50,
			65,
			85,
			40
		});
            baseStats.Add(593, new byte[] {
			100,
			60,
			70,
			85,
			105,
			60
		});
            baseStats.Add(594, new byte[] {
			165,
			75,
			80,
			40,
			45,
			65
		});
            baseStats.Add(595, new byte[] {
			50,
			47,
			50,
			57,
			50,
			65
		});
            baseStats.Add(596, new byte[] {
			70,
			77,
			60,
			97,
			60,
			108
		});
            baseStats.Add(597, new byte[] {
			44,
			50,
			91,
			24,
			86,
			10
		});
            baseStats.Add(598, new byte[] {
			74,
			94,
			131,
			54,
			116,
			20
		});
            baseStats.Add(599, new byte[] {
			40,
			55,
			70,
			45,
			60,
			30
		});
            baseStats.Add(600, new byte[] {
			60,
			80,
			95,
			70,
			85,
			50
		});
            baseStats.Add(601, new byte[] {
			60,
			100,
			115,
			70,
			85,
			90
		});
            baseStats.Add(602, new byte[] {
			35,
			55,
			40,
			45,
			40,
			60
		});
            baseStats.Add(603, new byte[] {
			65,
			85,
			70,
			75,
			70,
			40
		});
            baseStats.Add(604, new byte[] {
			85,
			115,
			80,
			105,
			80,
			50
		});
            baseStats.Add(605, new byte[] {
			55,
			55,
			55,
			85,
			55,
			30
		});
            baseStats.Add(606, new byte[] {
			75,
			75,
			75,
			125,
			95,
			40
		});
            baseStats.Add(607, new byte[] {
			50,
			30,
			55,
			65,
			55,
			20
		});
            baseStats.Add(608, new byte[] {
			60,
			40,
			60,
			95,
			60,
			55
		});
            baseStats.Add(609, new byte[] {
			60,
			55,
			90,
			145,
			90,
			80
		});
            baseStats.Add(610, new byte[] {
			46,
			87,
			60,
			30,
			40,
			57
		});
            baseStats.Add(611, new byte[] {
			66,
			117,
			70,
			40,
			50,
			67
		});
            baseStats.Add(612, new byte[] {
			76,
			147,
			90,
			60,
			70,
			97
		});
            baseStats.Add(613, new byte[] {
			55,
			70,
			40,
			60,
			40,
			40
		});
            baseStats.Add(614, new byte[] {
			95,
			110,
			80,
			70,
			80,
			50
		});
            baseStats.Add(615, new byte[] {
			70,
			50,
			30,
			95,
			135,
			105
		});
            baseStats.Add(616, new byte[] {
			50,
			40,
			85,
			40,
			65,
			25
		});
            baseStats.Add(617, new byte[] {
			80,
			70,
			40,
			100,
			60,
			145
		});
            baseStats.Add(618, new byte[] {
			109,
			66,
			84,
			81,
			99,
			32
		});
            baseStats.Add(619, new byte[] {
			45,
			85,
			50,
			55,
			50,
			65
		});
            baseStats.Add(620, new byte[] {
			65,
			125,
			60,
			95,
			60,
			105
		});
            baseStats.Add(621, new byte[] {
			77,
			120,
			90,
			60,
			90,
			48
		});
            baseStats.Add(622, new byte[] {
			59,
			74,
			50,
			35,
			50,
			35
		});
            baseStats.Add(623, new byte[] {
			89,
			124,
			80,
			55,
			80,
			55
		});
            baseStats.Add(624, new byte[] {
			45,
			85,
			70,
			40,
			40,
			60
		});
            baseStats.Add(625, new byte[] {
			65,
			125,
			100,
			60,
			70,
			70
		});
            baseStats.Add(626, new byte[] {
			95,
			110,
			95,
			40,
			95,
			55
		});
            baseStats.Add(627, new byte[] {
			70,
			83,
			50,
			37,
			50,
			60
		});
            baseStats.Add(628, new byte[] {
			100,
			123,
			75,
			57,
			75,
			80
		});
            baseStats.Add(629, new byte[] {
			70,
			55,
			75,
			45,
			65,
			60
		});
            baseStats.Add(630, new byte[] {
			110,
			65,
			105,
			55,
			95,
			80
		});
            baseStats.Add(631, new byte[] {
			85,
			97,
			66,
			105,
			66,
			65
		});
            baseStats.Add(632, new byte[] {
			58,
			109,
			112,
			48,
			48,
			109
		});
            baseStats.Add(633, new byte[] {
			52,
			65,
			50,
			45,
			50,
			38
		});
            baseStats.Add(634, new byte[] {
			72,
			85,
			70,
			65,
			70,
			58
		});
            baseStats.Add(635, new byte[] {
			92,
			105,
			90,
			125,
			90,
			98
		});
            baseStats.Add(636, new byte[] {
			55,
			85,
			55,
			50,
			55,
			60
		});
            baseStats.Add(637, new byte[] {
			85,
			60,
			65,
			135,
			105,
			100
		});
            baseStats.Add(638, new byte[] {
			91,
			90,
			129,
			90,
			72,
			108
		});
            baseStats.Add(639, new byte[] {
			91,
			129,
			90,
			72,
			90,
			108
		});
            baseStats.Add(640, new byte[] {
			91,
			90,
			72,
			90,
			129,
			108
		});
            baseStats.Add(641, new byte[] {
			79,
			115,
			70,
			125,
			80,
			111
		});
            baseStats.Add(642, new byte[] {
			79,
			115,
			70,
			125,
			80,
			111
		});
            baseStats.Add(643, new byte[] {
			100,
			120,
			100,
			150,
			120,
			90
		});
            baseStats.Add(644, new byte[] {
			100,
			150,
			120,
			120,
			100,
			90
		});
            baseStats.Add(645, new byte[] {
			89,
			125,
			90,
			115,
			80,
			101
		});
            baseStats.Add(646, new byte[] {
			125,
			130,
			90,
			130,
			90,
			95
		});
            baseStats.Add(647, new byte[] {
			91,
			72,
			90,
			129,
			90,
			108
		});
            baseStats.Add(648, new byte[] {
			100,
			77,
			77,
			128,
			128,
			90
		});
            baseStats.Add(649, new byte[] {
			71,
			120,
			95,
			120,
			95,
			99
		});
            baseStats.Add(650, new byte[] {
			56,
			61,
			65,
			48,
			45,
			38
		});
            baseStats.Add(651, new byte[] {
			61,
			78,
			95,
			56,
			58,
			57
		});
            baseStats.Add(652, new byte[] {
			88,
			107,
			122,
			74,
			75,
			64
		});
            baseStats.Add(653, new byte[] {
			40,
			45,
			40,
			62,
			60,
			60
		});
            baseStats.Add(654, new byte[] {
			59,
			59,
			58,
			90,
			70,
			73
		});
            baseStats.Add(655, new byte[] {
			75,
			69,
			72,
			114,
			100,
			104
		});
            baseStats.Add(656, new byte[] {
			41,
			56,
			40,
			62,
			44,
			71
		});
            baseStats.Add(657, new byte[] {
			54,
			63,
			52,
			83,
			56,
			97
		});
            baseStats.Add(658, new byte[] {
			72,
			95,
			67,
			103,
			71,
			122
		});
            baseStats.Add(659, new byte[] {
			38,
			36,
			38,
			32,
			36,
			57
		});
            baseStats.Add(660, new byte[] {
			85,
			56,
			77,
			50,
			77,
			78
		});
            baseStats.Add(661, new byte[] {
			45,
			50,
			43,
			40,
			38,
			62
		});
            baseStats.Add(662, new byte[] {
			62,
			73,
			55,
			56,
			52,
			84
		});
            baseStats.Add(663, new byte[] {
			78,
			81,
			71,
			74,
			69,
			126
		});
            baseStats.Add(664, new byte[] {
			38,
			35,
			40,
			27,
			25,
			35
		});
            baseStats.Add(665, new byte[] {
			45,
			22,
			60,
			27,
			30,
			29
		});
            baseStats.Add(666, new byte[] {
			80,
			52,
			50,
			90,
			50,
			89
		});
            baseStats.Add(667, new byte[] {
			62,
			50,
			58,
			73,
			54,
			72
		});
            baseStats.Add(668, new byte[] {
			86,
			68,
			72,
			109,
			66,
			106
		});
            baseStats.Add(669, new byte[] {
			44,
			38,
			39,
			61,
			79,
			42
		});
            baseStats.Add(670, new byte[] {
			54,
			45,
			47,
			75,
			98,
			52
		});
            baseStats.Add(671, new byte[] {
			78,
			65,
			68,
			112,
			154,
			75
		});
            baseStats.Add(672, new byte[] {
			66,
			65,
			48,
			62,
			57,
			52
		});
            baseStats.Add(673, new byte[] {
			123,
			100,
			62,
			97,
			81,
			68
		});
            baseStats.Add(674, new byte[] {
			67,
			82,
			62,
			46,
			48,
			43
		});
            baseStats.Add(675, new byte[] {
			95,
			124,
			78,
			69,
			71,
			58
		});
            baseStats.Add(676, new byte[] {
			75,
			80,
			60,
			65,
			90,
			102
		});
            baseStats.Add(677, new byte[] {
			62,
			48,
			54,
			63,
			60,
			68
		});
            baseStats.Add(678, new byte[] {
			74,
			48,
			76,
			83,
			81,
			104
		});
            baseStats.Add(679, new byte[] {
			45,
			80,
			100,
			35,
			37,
			28
		});
            baseStats.Add(680, new byte[] {
			59,
			110,
			150,
			45,
			49,
			35
		});
            baseStats.Add(681, new byte[] {
			60,
			50,
			150,
			50,
			150,
			60
		});
            baseStats.Add(682, new byte[] {
			78,
			52,
			60,
			63,
			65,
			23
		});
            baseStats.Add(683, new byte[] {
			101,
			72,
			72,
			99,
			89,
			29
		});
            baseStats.Add(684, new byte[] {
			62,
			48,
			66,
			59,
			57,
			49
		});
            baseStats.Add(685, new byte[] {
			82,
			80,
			86,
			85,
			75,
			72
		});
            baseStats.Add(686, new byte[] {
			53,
			54,
			53,
			37,
			46,
			45
		});
            baseStats.Add(687, new byte[] {
			86,
			92,
			88,
			68,
			75,
			73
		});
            baseStats.Add(688, new byte[] {
			42,
			52,
			67,
			39,
			56,
			50
		});
            baseStats.Add(689, new byte[] {
			72,
			92,
			115,
			54,
			86,
			68
		});
            baseStats.Add(690, new byte[] {
			50,
			60,
			60,
			60,
			60,
			30
		});
            baseStats.Add(691, new byte[] {
			65,
			75,
			90,
			97,
			123,
			44
		});
            baseStats.Add(692, new byte[] {
			50,
			53,
			62,
			58,
			63,
			44
		});
            baseStats.Add(693, new byte[] {
			71,
			73,
			88,
			120,
			89,
			59
		});
            baseStats.Add(694, new byte[] {
			44,
			38,
			33,
			61,
			43,
			70
		});
            baseStats.Add(695, new byte[] {
			62,
			55,
			52,
			109,
			94,
			109
		});
            baseStats.Add(696, new byte[] {
			58,
			89,
			77,
			45,
			45,
			48
		});
            baseStats.Add(697, new byte[] {
			82,
			121,
			119,
			69,
			59,
			71
		});
            baseStats.Add(698, new byte[] {
			77,
			59,
			50,
			67,
			63,
			46
		});
            baseStats.Add(699, new byte[] {
			123,
			77,
			72,
			99,
			92,
			58
		});
            baseStats.Add(700, new byte[] {
			95,
			65,
			65,
			110,
			130,
			60
		});
            baseStats.Add(701, new byte[] {
			78,
			92,
			75,
			74,
			63,
			118
		});
            baseStats.Add(702, new byte[] {
			67,
			58,
			57,
			81,
			67,
			101
		});
            baseStats.Add(703, new byte[] {
			50,
			50,
			150,
			50,
			150,
			50
		});
            baseStats.Add(704, new byte[] {
			45,
			50,
			35,
			55,
			75,
			40
		});
            baseStats.Add(705, new byte[] {
			68,
			75,
			53,
			83,
			113,
			60
		});
            baseStats.Add(706, new byte[] {
			90,
			100,
			70,
			110,
			150,
			80
		});
            baseStats.Add(707, new byte[] {
			57,
			80,
			91,
			80,
			87,
			75
		});
            baseStats.Add(708, new byte[] {
			43,
			70,
			48,
			50,
			60,
			38
		});
            baseStats.Add(709, new byte[] {
			85,
			110,
			76,
			65,
			82,
			56
		});
            baseStats.Add(710, new byte[] {
			49,
			66,
			70,
			44,
			55,
			51
		});
            baseStats.Add(711, new byte[] {
			65,
			90,
			122,
			58,
			75,
			84
		});
            baseStats.Add(712, new byte[] {
			55,
			69,
			85,
			32,
			35,
			28
		});
            baseStats.Add(713, new byte[] {
			95,
			117,
			184,
			44,
			46,
			28
		});
            baseStats.Add(714, new byte[] {
			40,
			30,
			35,
			45,
			40,
			55
		});
            baseStats.Add(715, new byte[] {
			85,
			70,
			80,
			97,
			80,
			123
		});
            baseStats.Add(716, new byte[] {
			126,
			131,
			95,
			131,
			98,
			99
		});
            baseStats.Add(717, new byte[] {
			126,
			131,
			95,
			131,
			98,
			99
		});
            baseStats.Add(718, new byte[] {
			108,
			100,
			121,
			81,
			95,
			95
		});
            baseStats.Add(719, new byte[] {
			0,
			0,
			0,
			0,
			0,
			0
		});
            baseStats.Add(720, new byte[] {
			0,
			0,
			0,
			0,
			0,
			0
		});
            baseStats.Add(721, new byte[] {
			0,
			0,
			0,
			0,
			0,
			0
		});


            dictionariesInitialized = true;
        }


        public static uint maxExp(string s)
        {
            ushort pkm = (ushort)species.IndexOf(s);
            uint exp = 0;
            switch (expList[pkm])
            {
                case 0:
                    exp = slowlist[99];
                    break;
                case 1:
                    exp = mediumSlowList[99];
                    break;
                case 2:
                    exp = mediumFastList[99];
                    break;
                case 3:
                    exp = fastlist[99];
                    break;
                case 4:
                    exp = fluctuatinglist[99];
                    break;
                case 5:
                    exp = erraticlist[99];
                    break;
            }
            return exp;
        }

        public static byte calculateLevel(string s, uint exp)
        {
            ushort pkm = (ushort)species.IndexOf(s);
            byte level = 0;
            int y = 0;
            switch (expList[pkm])
            {
                case 0:
                    while (y < 100)
                    {
                        if (exp >= slowlist[level])
                        {
                            level += 1;
                        }
                        else
                        {
                            break;
                        }
                        if (level >= 100)
                        {
                            level = 100;
                        }
                        y += 1;
                    }
                    return level;
                case 1:
                    while (y < 100)
                    {
                        if (exp >= mediumSlowList[level])
                        {
                            level += 1;
                        }
                        else
                        {
                            break;
                        }
                        if (level >= 100)
                        {
                            level = 100;
                        }
                        y += 1;
                    }
                    return level;
                case 2:
                    while (y < 100)
                    {
                        if (exp >= mediumFastList[level])
                        {
                            level += 1;
                        }
                        else
                        {
                            break;
                        }
                        if (level >= 100)
                        {
                            level = 100;
                        }
                        y += 1;
                    }
                    return level;
                case 3:
                    while (y < 100)
                    {
                        if (exp >= fastlist[level])
                        {
                            level += 1;
                        }
                        else
                        {
                            break;
                        }
                        if (level >= 100)
                        {
                            level = 100;
                        }
                        y += 1;
                    }
                    return level;
                case 4:
                    while (y < 100)
                    {
                        if (exp >= fluctuatinglist[level])
                        {
                            level += 1;
                        }
                        else
                        {
                            break;
                        }
                        if (level >= 100)
                        {
                            level = 100;
                        }
                        y += 1;
                    }
                    return level;
                case 5:
                    while (y < 100)
                    {
                        if (exp >= erraticlist[level])
                        {
                            level += 1;
                        }
                        else
                        {
                            break;
                        }
                        if (level >= 100)
                        {
                            level = 100;
                        }
                        y += 1;
                    }
                    return level;
            }
            return 1;
        }

        public static uint calculateExp(string s, byte level)
        {
            ushort pkm = (ushort)species.IndexOf(s);
            uint exp = 0;
            if (level > 100)
            {
                level = 100;
            }
            switch (expList[pkm])
            {
                case 0:
                    exp = slowlist[level - 1];
                    break;
                case 1:
                    exp = mediumSlowList[level - 1];
                    break;
                case 2:
                    exp = mediumFastList[level - 1];
                    break;
                case 3:
                    exp = fastlist[level - 1];
                    break;
                case 4:
                    exp = fluctuatinglist[level - 1];
                    break;
                case 5:
                    exp = erraticlist[level - 1];
                    break;
            }
            return exp;
        }
    }
}
