/*RubenPikachu's Pikaedit Library
 * This is a dll that can be used to develop Gen 5 and Gen 4 Pokemon apps and Pikaedit plugins, the latter the purpose of this dll to exist
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pikaedit_Lib
{

    /// <summary>
    /// Represents Pikaedit version targeted for this plugin
    /// </summary>
    public enum PluginGen
    {
        Gen5,
        Gen4,
        XY,
        Gen4_Gen5,
        Gen5_XY,
        Gen4_XY,
        All
    }

    /// <summary>
    /// Represents Processes available to be done after Plugin execution
    /// </summary>
    public enum Process
    {
        None,
        /// <summary>
        /// Load returned Pokemon to Pikaedit PKM Editor
        /// </summary>
        Load_Pkm,
        /// <summary>
        /// Update save file loaded in Pikaedit
        /// </summary>
        Update_Save_File,
        /// <summary>
        /// Calls the plugin updater to update only this plugin, you must add a new version verification system and have an if there is a new version change setPostProcess to this value
        /// </summary>
        Update_Plugin
    }

    /// <summary>
    /// Represents Requirements for plugin to be run
    /// </summary>
    public enum Requirement
    {
        None,
        /// <summary>
        /// Pikaedit must have a loaded Save File to run plugin
        /// </summary>
        Loaded_Save_File
    }

    /// <summary>
    /// Interface required to run this as a Pikaedit plugin
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Get used by Pikaedit to receive edited Pokemon data you send, Set used to receive loaded PKM in Pikaedit
        /// </summary>
        Pokemon getPokemon
        {
            get;
            set;
        }

        /// <summary>
        /// Get used by Pikaedit Gen 4 to receive edited Pokemon data you send, Set used to receive loaded PKM in Pikaedit Gen 4
        /// </summary>
        PokemonGen4 getGen4Pokemon
        {
            get;
            set;
        }

        /// <summary>
        /// Get used by Pikaedit XY to receive edited Pokemon data you send, Set used to receive loaded PKM in Pikaedit XY
        /// </summary>
        PokemonXY getXYPokemon
        {
            get;
            set;
        }

        /// <summary>
        /// Get used by Pikaedit to receive edited Save File data you send, Set used to reveive loaded Save File
        /// </summary>
        SaveFile getSaveFile
        {
            get;
            set;
        }

        /// <summary>
        /// Get used by Pikaedit Gen 4 to receive edited Save File data you send, Set used to reveive loaded Save File Gen 4
        /// </summary>
        SaveFileGen4 getGen4SaveFile
        {
            get;
            set;
        }

        /// <summary>
        /// Get used by Pikaedit to receive Process to run after plugin execution
        /// </summary>
        Process setPostProcess
        {
            get;
        }

        /// <summary>
        /// Get used by Pikaedit to receive Requirement needed to run plugin
        /// </summary>
        Requirement setRequirement
        {
            get;
        }

        /// <summary>
        /// Represents the plugin's name
        /// </summary>
        /// <returns>Plugin's name as string</returns>
        string getName();

        /// <summary>
        /// Represents the plugin's version
        /// </summary>
        /// <returns>Plugin's version as string</returns>
        string getVersion();

        /// <summary>
        /// Represents the plugin's author(s)
        /// </summary>
        /// <returns>Plugin's author(s) as string</returns>
        string getAuthor();

        /// <summary>
        /// Represents the plugin's information, shown as a tooltip when selecting plugin in Pikaedit
        /// </summary>
        /// <returns>Plugin's info</returns>
        string getPluginInfo();

        /// <summary>
        /// Process to do in Pikaedit, for Windows Forms use ShowDialog() to display them
        /// </summary>
        void Do();

        /// <summary>
        /// Receive current Pikaedit language
        /// </summary>
        PkmLib.PikaeditLanguages getLanguage
        {
            set;
        }

        /// <summary>
        /// Get used by Pikaedit to determine which version of Pikaedit can run this plugin, Set used to receive Pikaedit version running the plugin
        /// </summary>
        PluginGen getPluginGen
        {
            get;
            set;
        }
    }
}
