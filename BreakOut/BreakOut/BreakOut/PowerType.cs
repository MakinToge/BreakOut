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
        None,
        PlusOneLife,
        MinusOneLife,
        OnFire,
        Faster,
        Slower,
        MultiBall,
        SmallerPaddle,
        LargerPaddle,
        Invicibility
    }
}
