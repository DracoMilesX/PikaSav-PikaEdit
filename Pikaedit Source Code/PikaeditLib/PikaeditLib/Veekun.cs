using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SQLite;

namespace Pikaedit_Lib
{
    /// <summary>
    /// Type of Moveset Search
    /// </summary>
    public enum MovesetType
    {
        All,
        Levelup,
        Egg_moves,
        Tutor,
        TMs_HMs,
        Shadow_Pokemon_Purified=9,
        Form_Exclusive,
        Event
    }

    /// <summary>
    /// Type of Moveset Search depending on level
    /// </summary>
    public enum SearchByLevelType
    {
        All,
        Less_Or_Equal,
        Less,
        Equal,
        Greater,
        Greater_Or_Equal
    }

    /// <summary>
    /// Type of Moveset Search depending on Gen
    /// </summary>
    public enum GenSearch
    {
        Gen_1=1,
        Gen_2,
        Gen_3,
        Gen_4,
        Gen_5,
        Gen_6,
        From_3_to_4,
        From_3_to_5,
        From_3_to_6,
        From_4_to_5,
        From_4_to_6,
        From_5_to_6
    }

    /// <summary>
    /// Contains methods to connect/disconnect and use Veekun's open source database
    /// Credits to Veekun open source Pokedex
    /// </summary>
    public static class Veekun
    {
        /// <summary>
        /// Indicates if the Connection to the database has been done
        /// </summary>
        public static bool isInitialized = false;
        private static SQLiteConnection sqlite_conn;
        private static SQLiteCommand sqlite_cmd;
        private static SQLiteDataReader sqlite_datareader;

        /// <summary>
        /// Initialize Database, write veekun.db at temporary folder and create connection
        /// </summary>
        public static void Initialize()
        {
            if (isInitialized) return;
            PkmLib.Initialize();
            try
            {
                File.WriteAllBytes(Path.GetTempPath() + "veekun.db", Properties.Resources.veekun_pokedex);
                //File.WriteAllBytes("veekun.db", Properties.Resources.veekun_pokedex);
            }
            catch (Exception e)
            {

            }
            sqlite_conn = new SQLiteConnection("Data Source=" + System.IO.Path.GetTempPath() + "veekun.db;Version=3;Journal Mode=Off;");
            //sqlite_conn = new SQLiteConnection("Data Source=veekun.db;Version=3;Journal Mode=Off;");
            try
            {
                sqlite_conn.Open();
                isInitialized = true;
            }
            catch
            {
                isInitialized = false;
            }
        }

        /// <summary>
        /// Get a List containing the index numbers of the moves returned by the search, returns null if the database hasn't been Initialized
        /// </summary>
        /// <param name="species">Pokemon species</param>
        /// <param name="gen">Type of GenSearch</param>
        /// <param name="form">Pokemon form</param>
        /// <param name="type">Type of MovesetType Search</param>
        /// <param name="level">Pokemon level</param>
        /// <param name="levelSearch">Type of LevelSearch</param>
        /// <returns></returns>
        public static List<ushort> getMoveset(string species, GenSearch gen, string form = "None", MovesetType type=MovesetType.All, int level=0, SearchByLevelType levelSearch = SearchByLevelType.All)
        {
            if (!isInitialized) return null;
            int formValue = PkmLib.getFormValue(species, form);
            int pokedexNo = PkmLib.species.IndexOf(species);
            if (formValue!=0)
            {
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = "SELECT id FROM pokemon WHERE identifier LIKE '" + species + "-" + form + "';";
                sqlite_datareader = sqlite_cmd.ExecuteReader();
                if (sqlite_datareader.Read())
                {
                    pokedexNo = Convert.ToInt32(sqlite_datareader["id"]);
                }
            }
            sqlite_cmd = sqlite_conn.CreateCommand();
            string genS = "";
            switch (gen)
            {
                case GenSearch.From_3_to_4:
                    genS = "version_groups.generation_id >= 3 AND version_groups.generation_id <= 4";
                    break;
                case GenSearch.From_3_to_5:
                    genS = "version_groups.generation_id >= 3 AND version_groups.generation_id <= 5";
                    break;
                case GenSearch.From_3_to_6:
                    genS = "version_groups.generation_id >= 3 AND version_groups.generation_id <= 6";
                    break;
                case GenSearch.From_4_to_5:
                    genS = "version_groups.generation_id >= 4 AND version_groups.generation_id <= 5";
                    break;
                case GenSearch.From_4_to_6:
                    genS = "version_groups.generation_id >= 4 AND version_groups.generation_id <= 6";
                    break;
                case GenSearch.From_5_to_6:
                    genS = "version_groups.generation_id >= 5 AND version_groups.generation_id <= 6";
                    break;
                default:
                    genS = "version_groups.generation_id = " + (int)gen;
                    break;
            }
            if (type == MovesetType.All)
            {
                sqlite_cmd.CommandText = "SELECT DISTINCT move_id FROM pokemon_moves JOIN version_groups ON version_groups.id = pokemon_moves.version_group_id WHERE " + genS + " AND (pokemon_id = " + pokedexNo + " OR pokemon_id IN (SELECT evolves_from_species_id FROM pokemon_species WHERE evolution_chain_id IN(SELECT evolution_chain_id FROM pokemon_species WHERE id = " +
                    pokedexNo + ")));";
            }
            else
            {
                if (type == MovesetType.Levelup)
                {
                    if (levelSearch == SearchByLevelType.All)
                    {
                        sqlite_cmd.CommandText = "SELECT DISTINCT move_id FROM pokemon_moves JOIN version_groups ON version_groups.id = pokemon_moves.version_group_id WHERE " + genS + " AND pokemon_move_method_id = 1 AND (pokemon_id = " + pokedexNo + " OR pokemon_id IN (SELECT evolves_from_species_id FROM pokemon_species WHERE evolution_chain_id IN(SELECT evolution_chain_id FROM pokemon_species WHERE id = " +
                    pokedexNo + ")));";
                    }
                    else
                    {
                        string op = "";
                        switch (levelSearch)
                        {
                            case SearchByLevelType.Less_Or_Equal:
                                op = "<=";
                                break;
                            case SearchByLevelType.Less:
                                op = "<";
                                break;
                            case SearchByLevelType.Equal:
                                op = "=";
                                break;
                            case SearchByLevelType.Greater:
                                op = ">";
                                break;
                            case SearchByLevelType.Greater_Or_Equal:
                                op = ">=";
                                break;
                            default:
                                op = "<=";
                                break;
                        }
                        sqlite_cmd.CommandText = "SELECT DISTINCT move_id FROM pokemon_moves JOIN version_groups ON version_groups.id = pokemon_moves.version_group_id WHERE " + genS + " AND pokemon_move_method_id = 1 AND level" + op + " " + level + " AND (pokemon_id = " + pokedexNo + " OR pokemon_id IN (SELECT evolves_from_species_id FROM pokemon_species WHERE evolution_chain_id IN(SELECT evolution_chain_id FROM pokemon_species WHERE id = " +
                    pokedexNo + ")));";
                    }
                }
                else
                {
                    if (type == MovesetType.Egg_moves)
                    {
                        sqlite_cmd.CommandText = "SELECT DISTINCT move_id FROM pokemon_moves JOIN version_groups ON version_groups.id = pokemon_moves.version_group_id WHERE " + genS + " AND (pokemon_move_method_id=2 OR pokemon_move_method_id=6) AND (pokemon_id = " + pokedexNo + " OR pokemon_id IN (SELECT evolves_from_species_id FROM pokemon_species WHERE evolution_chain_id IN(SELECT evolution_chain_id FROM pokemon_species WHERE id = " +
                    pokedexNo + ")));";
                    }
                    else
                    {
                        sqlite_cmd.CommandText = "SELECT DISTINCT move_id FROM pokemon_moves JOIN version_groups ON version_groups.id = pokemon_moves.version_group_id WHERE " + genS + " AND pokemon_move_method_id=" + (int)type + " AND (pokemon_id = " + pokedexNo + " OR pokemon_id IN (SELECT evolves_from_species_id FROM pokemon_species WHERE evolution_chain_id IN(SELECT evolution_chain_id FROM pokemon_species WHERE id = " +
                    pokedexNo + ")));";
                    }
                }
            }
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            List<ushort> moveset = new List<ushort>();
            while (sqlite_datareader.Read())
            {
                moveset.Add(Convert.ToUInt16(sqlite_datareader["move_id"]));
            }
            return moveset;
        }

        /// <summary>
        /// Get a list containing 3 elements with a pokemon abilities, ordered by the ability index, the last one is the Hidden Ability
        /// </summary>
        /// <param name="species">Pokemon species</param>
        /// <param name="form">Pokemon form</param>
        /// <returns>A List containing 3 bytes for the pokemon abilities ordered by ability index, 0 represents there is no ability, the last one is the Hidden Ability</returns>
        public static List<byte> getAbilities(string species, string form="None")
        {
            if (!isInitialized) return null;
            int formValue = PkmLib.getFormValue(species, form);
            int pokedexNo = PkmLib.species.IndexOf(species);
            if (formValue != 0)
            {
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = "SELECT id FROM pokemon WHERE identifier LIKE '" + species + "-" + form + "';";
                sqlite_datareader = sqlite_cmd.ExecuteReader();
                if (sqlite_datareader.Read())
                {
                    pokedexNo = Convert.ToInt32(sqlite_datareader["id"]);
                }
            }
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT ability_id,slot FROM pokemon_abilities WHERE pokemon_id = " + pokedexNo + " ORDER BY slot ASC;";
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            List<byte> abilities = new List<byte>();
            abilities.Add(0);
            abilities.Add(0);
            abilities.Add(0);
            while (sqlite_datareader.Read())
            {
                abilities.Insert(Convert.ToInt32(sqlite_datareader["slot"]) - 1, Convert.ToByte(sqlite_datareader["ability_id"]));
            }
            return abilities;
        }

        /// <summary>
        /// Buscar tabla donde este la columna evolves_from
        /// </summary>
        /// <param name="species"></param>
        /// <returns></returns>
        public static List<ushort> getPreevolutions(string species)
        {
            if (!isInitialized) return null;
            int pokedexNo = PkmLib.species.IndexOf(species);
            bool isNull = false;
            List<ushort> s = new List<ushort>();
            do
            {
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = String.Format("SELECT evolves_from_species_id FROM pokemon_species WHERE id = {0};", pokedexNo);
                sqlite_datareader = sqlite_cmd.ExecuteReader();

                if (sqlite_datareader.Read())
                {
                    s.Add(Convert.ToUInt16(sqlite_datareader["evolves_from_species_id"]));
                    pokedexNo = Convert.ToInt32(sqlite_datareader["evolves_from_species_id"]);
                }
                else
                {
                    isNull = true;
                }
                sqlite_datareader.Close();
            } while (!isNull);
            if (s.Count == 0)
            {
                return null;
            }
            else
            {
                return s;
            }
        }

        //public static List<byte> getMovesPP()
        //{
        //    if (!isInitialized) return null;
        //    List<byte> pp = new List<byte>();
        //    sqlite_cmd = sqlite_conn.CreateCommand();
        //    sqlite_cmd.CommandText = "SELECT pp FROM moves ORDER BY id ASC;";
        //    sqlite_datareader = sqlite_cmd.ExecuteReader();
        //    pp.Add(0);
        //    while (sqlite_datareader.Read())
        //    {
        //        try
        //        {
        //            byte c = Convert.ToByte(Convert.ToDouble(sqlite_datareader["pp"]));
        //            pp.Add(c);
        //        }
        //        catch (Exception e)
        //        {
        //            pp.Add(0);
        //        }
        //    }
        //    return pp;
        //}

        /// <summary>
        /// Create a custom query and get the DataReader for that query, returns null if the database hasn't been Initialized
        /// </summary>
        /// <param name="query">A SQLite Query</param>
        /// <returns>SQLiteDataReader with the contents of the query result</returns>
        public static SQLiteDataReader getFromQuery(string query)
        {
            if (!isInitialized) return null;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = query;
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            return sqlite_datareader;
        }

        /// <summary>
        /// Finishes connection and deletes veekun.db from temporary folder
        /// </summary>
        public static void Close()
        {
            if (isInitialized)
            {
                sqlite_conn.Close();
                isInitialized = false;
                sqlite_conn.Dispose();
                sqlite_cmd.Dispose();
                GC.Collect();
                //while (File.Exists(Path.GetTempPath() + "veekun.db"))
                //{
                //    File.Delete(Path.GetTempPath() + "veekun.db");
                //}
            }
        }
    }
}
