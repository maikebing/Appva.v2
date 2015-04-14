// <copyright file="Display.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Scheduler.Infrastructure.UI
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class Display
    {
        /// <summary>
        /// Prints the welcome message.
        /// <externalLink>
        ///     <linkText>ASCII art</linkText>
        ///     <linkUri>http://patorjk.com/software/taag/#p=display&f=Slant&t=%20%20%20%20MCSS%0AScheduler</linkUri>
        /// </externalLink>
        /// </summary>
        public static void Welcome()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(@"             __  ___________________            "); 
            Console.WriteLine(@"            /  |/  / ____/ ___/ ___/            ");
            Console.WriteLine(@"           / /|_/ / /    \__ \\__ \             ");
            Console.WriteLine(@"          / /  / / /___ ___/ /__/ /             ");
            Console.WriteLine(@"   _____ /_/  / /\____//____// __/   __         ");
            Console.WriteLine(@"  / ___/_____/ /_  ___  ____/ /_  __/ /__  _____");
            Console.WriteLine(@"  \__ \/ ___/ __ \/ _ \/ __  / / / / / _ \/ ___/");
            Console.WriteLine(@"  __/ / /__/ / / /  __/ /_/ / /_/ / /  __/ /    ");
            Console.WriteLine(@"/____/\___/_/ /_/\___/\__,_/\__,_/_/\___/_/     ");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }

        /// <summary>
        /// Prints system information.
        /// </summary>
        public static void Information()
        {
        }
    }
}