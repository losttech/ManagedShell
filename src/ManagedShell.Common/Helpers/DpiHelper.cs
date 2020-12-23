﻿using System;
using System.Drawing;

namespace ManagedShell.Common.Helpers
{
    public class DpiHelper
    {
        // DPI at user logon to the system
        private static double? _oldDpiScale;
        public static double OldDpiScale
        {
            get
            {
                if (_oldDpiScale == null)
                {
                    _oldDpiScale = GetDpiScale();
                }

                return (double)_oldDpiScale;
            }
            set
            {
                _oldDpiScale = value;
            }
        }

        // Current system DPI; typically set on AppBar startup and on WM_DPICHANGED
        private static double? _dpiScale;
        public static double DpiScale
        {
            get
            {
                if (_dpiScale == null)
                {
                    _dpiScale = GetDpiScale();
                }

                return (double)_dpiScale;
            }
            set
            {
                _dpiScale = value;
            }
        }

        // SystemParameters class returns values based on logon DPI only; this calculates how we should scale that number if logon DPI != current DPI.
        public static double DpiScaleAdjustment
        {
            get { return DpiScale / OldDpiScale; }
        }

        /// <summary>
        /// Transforms device independent units (1/96 of an inch)
        /// to pixels
        /// </summary>
        /// <param name="unitX">a device independent unit value X</param>
        /// <param name="unitY">a device independent unit value Y</param>
        /// <param name="pixelX">returns the X value in pixels</param>
        /// <param name="pixelY">returns the Y value in pixels</param>
        public static void TransformToPixels(double unitX, double unitY, out int pixelX, out int pixelY)
        {
            pixelX = (int)(DpiScale * unitX);
            pixelY = (int)(DpiScale * unitY);
        }

        public static void TransformFromPixels(double unitX, double unitY, out int pixelX, out int pixelY)
        {
            pixelX = (int)(unitX / DpiScale);
            pixelY = (int)(unitY / DpiScale);
        }

        private static double GetDpiScale()
        {
            using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
            {
                return (g.DpiX / 96);
            }
        }

    }
}
