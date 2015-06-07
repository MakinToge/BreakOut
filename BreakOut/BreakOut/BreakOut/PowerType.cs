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
    /// Enum PowerType
    /// </summary>
    public enum PowerType {
        /// <summary>
        /// The none
        /// </summary>
        None = 0,
        /// <summary>
        /// The plus one life
        /// </summary>
        PlusOneLife,
        /// <summary>
        /// The minus one life
        /// </summary>
        MinusOneLife,
        /// <summary>
        /// The on fire
        /// </summary>
        OnFire,
        /// <summary>
        /// The faster
        /// </summary>
        Faster,
        /// <summary>
        /// The slower
        /// </summary>
        Slower,
        /// <summary>
        /// The multi ball
        /// </summary>
        MultiBall,
        /// <summary>
        /// The smaller paddle
        /// </summary>
        SmallerPaddle,
        /// <summary>
        /// The larger paddle
        /// </summary>
        LargerPaddle,
        /// <summary>
        /// The invicibility
        /// </summary>
        Invicibility,
    }
}
