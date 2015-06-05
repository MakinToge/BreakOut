// ***********************************************************************
// Assembly         : BreakOut
// ***********************************************************************
// <copyright file="GameState.cs" company="">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// The BreakOut namespace.
/// </summary>
namespace BreakOut {
    /// <summary>
    /// Enum GameState
    /// </summary>
    public enum GameState {
        /// <summary>
        /// The main menu
        /// </summary>
        MainMenu,
        /// <summary>
        /// The difficulty selection
        /// </summary>
        DifficultySelection,
        /// <summary>
        /// The level selection
        /// </summary>
        LevelSelection,
        /// <summary>
        /// The in play page
        /// </summary>
        InPlay,
        /// <summary>
        /// The finish page
        /// </summary>
        Finish,
        /// <summary>
        /// The pause page
        /// </summary>
        Pause,
        /// <summary>
        /// The score page
        /// </summary>
        Score
    }
}
