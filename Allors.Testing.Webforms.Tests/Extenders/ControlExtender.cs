// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlExtender.cs" company="allors bvba">
//   Copyright 2008-2014 Allors bvba.
//   
//   This program is free software: you can redistribute it and/or modify
//   it under the terms of the GNU Lesser General Public License as published by
//   the Free Software Foundation, either version 3 of the License, or
//   (at your option) any later version.
//   
//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   GNU Lesser General Public License for more details.
//   
//   You should have received a copy of the GNU Lesser General Public License
//   along with this program.  If not, see http://www.gnu.org/licenses.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Allors.Testing.Webforms
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Web.UI;

    public static class ControlExtender 
    {
        /// <summary>
        /// Invoke a method on this control.
        /// Especially useful for private methods,
        /// because a type cast will not make those methods accessible. 
        /// </summary>
        /// <param name="control">The control</param>
        /// <param name="methodName">The name of the method to invoke</param>
        /// <param name="methodArgs">The parameter arguments for the method</param>
        public static void Invoke(this Control control, string methodName, params object[] methodArgs)
        {
            var type = control.GetType();
            var method = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            method.Invoke(control, methodArgs);
        }
        
        public static T Select<T>(this Control root) where T : Control
        {
            return (T)Select(root, typeof(T));
        }

        public static Control Select(this Control root, Type type)
        {
            var controls = SelectAll(root, type);

            switch (controls.Length)
            {
                case 0:
                    return null;
                case 1:
                    return controls[0];
                default:
                    throw new Exception("Multiple controls with type " + type + " found, use SelectAll(type) instead.");
            }
        }
        
        public static T[] SelectAll<T>(this Control root) where T : Control
        {
            var controls = new List<T>();
            foreach (var control in SelectAll(root, typeof(T)))
            {
                controls.Add((T)control);
            }

            return controls.ToArray();
        }

        public static Control[] SelectAll(this Control root, Type type)
        {
            var controls = new List<Control>();
            SelectAll(root, controls, type);
            return controls.ToArray();
        }

        public static T Select<T>(this Control root, string id) where T : Control
        {
            var controls = SelectAll<T>(root, id);

            switch (controls.Length)
            {
                case 0:
                    return null;
                case 1:
                    return controls[0];
                default:
                    throw new Exception("Multiple controls with id " + id + " found, use SelectAll(id) instead.");
            }
        }

        public static T[] SelectAll<T>(this Control root, string id) where T : Control
        {
            var controls = new List<T>();
            foreach (var control in SelectAll(root, typeof(T), id))
            {
                controls.Add((T)control);
            }

            return controls.ToArray();
        }

        public static Control[] SelectAll(this Control root, Type type, string id)
        {
            var controls = new List<Control>();
            SelectAll(root, controls, type, id.ToLower());
            return controls.ToArray();
        }

        public static Control[] SelectAll(this Control root, string id)
        {
            var controls = new List<Control>();
            SelectAll(root, controls, id.ToLower());
            return controls.ToArray();
        }

        private static void SelectAll(Control root, ICollection<Control> controls, Type type)
        {
            if (type.IsInstanceOfType(root))
            {
                controls.Add(root);
            }

            // Trigger EnsureChildControls()
            root.FindControl("X");

            foreach (Control childControl in root.Controls)
            {
                SelectAll(childControl, controls, type);
            }
        }

        private static void SelectAll(Control root, ICollection<Control> controls, string idToLower)
        {
            if (root.ID != null && root.ID.ToLower().Equals(idToLower))
            {
                controls.Add(root);
            }

            // Trigger EnsureChildControls()
            root.FindControl("X");

            foreach (Control childControl in root.Controls)
            {
                SelectAll(childControl, controls, idToLower);
            }
        }

        private static void SelectAll(Control root, ICollection<Control> controls, Type type, string idToLower)
        {
            if (root.ID != null && root.ID.ToLower().Equals(idToLower) && 
                type.IsInstanceOfType(root))
            {
                controls.Add(root);
            }

            // Trigger EnsureChildControls()
            root.FindControl("X");

            foreach (Control childControl in root.Controls)
            {
                SelectAll(childControl, controls, type, idToLower);
            }
        }
    }
}