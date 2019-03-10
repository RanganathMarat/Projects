#region Copyright Koninklijke Philips Electronics N.V. 2016
//
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
// FILENAME: filename.cs
//
#endregion

using Philips.Platform.Presentation;
using System;
using System.Windows;

namespace PrismApplication
{
    /// <summary>
    /// Manages the Theme of Application
    /// </summary>
    public class ThemeManager
    {
        /// <summary>
        /// Current Theme
        /// </summary>
        private string _currentTheme = string.Empty;

        /// <summary>
        /// Constructor
        /// </summary>
        public ThemeManager()
        {
            ApplyTheme(Theme.Light);
        }

        /// <summary>
        /// Applies the specified theme
        /// </summary>
        /// <param name="theme">
        /// Theme to apply
        /// </param>
        public void ApplyTheme(string theme)
        {
            ThemeAndCultureUtility.SelectTheme(new Uri(theme, UriKind.RelativeOrAbsolute));
            _currentTheme = theme;
        }

        /// <summary>
        /// Current Theme
        /// </summary>
        public string CurrentTheme
        {
            get
            {
                return _currentTheme;
            }
        }

        public static class Theme
        {
            /// <summary>
            /// Toolkit Dark Theme
            /// </summary>
            public static string Dark = "Philips.Platform.Presentation;v16.4.0.0;223d991ebf2e6ef5;component/ExperienceIdentity/ExperienceIdentity.Dark.xaml";
            /// <summary>
            /// Toolkit Light Theme
            /// </summary>
            public static string Light = "Philips.Platform.Presentation;v16.4.0.0;223d991ebf2e6ef5;component/ExperienceIdentity/ExperienceIdentity.Light.xaml";
        }
    }
}

#region Revision History
// dd-mmm-yyyy  Name
//              Initial version
#endregion Revision History
