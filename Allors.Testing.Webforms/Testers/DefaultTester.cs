// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultTester.cs" company="allors bvba">
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
namespace Allors.Testing.Webforms.Testers
{
    using System;
    using System.Collections.Generic;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    internal class DefaultTester : ITester
    {
        public void SavePostData(Dictionary<string, string> postDataByControlUniqueId, Control control, Control autoPostBackControl)
        {
            if (!postDataByControlUniqueId.ContainsKey(control.UniqueID))
            {
                if (control is IButtonControl || control is HtmlButton)
                {
                    // Use Click or AutoPostBack
                }
                else if (control is CheckBoxList)
                {
                    var checkBoxList = (CheckBoxList)control;
                    for (var i = 0; i < checkBoxList.Items.Count; i++)
                    {
                        var listItem = checkBoxList.Items[i];
                        if (listItem.Selected)
                        {
                            postDataByControlUniqueId[control.UniqueID + "$" + i] = "on";
                        }
                    }
                }
                else if (control is RadioButton && control != autoPostBackControl)
                {
                    var radioButton = (RadioButton)control;
                    if (radioButton.Checked)
                    {
                        postDataByControlUniqueId[control.UniqueID] = control.UniqueID;
                    }
                }
                else if (control is CheckBox && control != autoPostBackControl)
                {
                    var checkBox = (CheckBox)control;
                    if (checkBox.Checked)
                    {
                        postDataByControlUniqueId[control.UniqueID] = "on";
                    }
                }
                else if (control is ITextControl)
                {
                    var textControl = (ITextControl)control;
                    postDataByControlUniqueId[control.UniqueID] = textControl.Text;
                }
            }
        }

        public void Click(Control control, WebformsTest webFormsTest)
        {
            if (control is ImageButton)
            {
                webFormsTest.PostDataByControlUniqueId[control.UniqueID + ".x"] = "1";
                webFormsTest.PostDataByControlUniqueId[control.UniqueID + ".y"] = "1";
                webFormsTest.PostDataByControlUniqueId["__EVENTARGUMENT"] = string.Empty;
                webFormsTest.PostDataByControlUniqueId["__EVENTTARGET"] = string.Empty;
                webFormsTest.PostDataByControlUniqueId["__LASTFOCUS"] = string.Empty;
            }
            else if (control is LinkButton)
            {
                webFormsTest.PostDataByControlUniqueId["__EVENTARGUMENT"] = string.Empty;
                webFormsTest.PostDataByControlUniqueId["__EVENTTARGET"] = control.UniqueID;
                webFormsTest.PostDataByControlUniqueId["__LASTFOCUS"] = string.Empty;
            }
            else if (control is IButtonControl)
            {
                webFormsTest.PostDataByControlUniqueId[control.UniqueID] = ((IButtonControl)control).Text;
                webFormsTest.PostDataByControlUniqueId["__EVENTARGUMENT"] = string.Empty;
                webFormsTest.PostDataByControlUniqueId["__EVENTTARGET"] = string.Empty;
                webFormsTest.PostDataByControlUniqueId["__LASTFOCUS"] = string.Empty;
            }
            else if (control is HtmlButton)
            {
                webFormsTest.PostDataByControlUniqueId[control.UniqueID] = string.Empty;
                webFormsTest.PostDataByControlUniqueId["__EVENTARGUMENT"] = string.Empty;
                webFormsTest.PostDataByControlUniqueId["__EVENTTARGET"] = string.Empty;
                webFormsTest.PostDataByControlUniqueId["__LASTFOCUS"] = string.Empty;
            }
            else
            {
                throw new Exception("Control " + control + " does not support Click().\nUse Browser.Click() with another control or Browser.Navigate().");
            }
        }

        public void Set(WebformsTest webFormsTest, ITextControl textControl, string text)
        {
            var control = (Control)textControl;
            webFormsTest.PostDataByControlUniqueId[control.UniqueID] = text;
        }

        public void Set(WebformsTest webFormsTest, ListControl listControl, int index)
        {
            if (listControl is CheckBoxList)
            {
                webFormsTest.PostDataByControlUniqueId[listControl.UniqueID + "$" + index] = "on";
            }
            else
            {
                var listItem = listControl.Items[index];
                webFormsTest.PostDataByControlUniqueId[listControl.UniqueID] = listItem.Value;
            }
        }

        public void Set(WebformsTest webFormsTest, ICheckBoxControl checkBoxControl, bool value)
        {
            var control = (Control)checkBoxControl;
            webFormsTest.PostDataByControlUniqueId[control.UniqueID] = "true";
        }

        public void AutoPostBack(Control control, WebformsTest webFormsTest)
        {
            if (control is RadioButton)
            {
                webFormsTest.PostDataByControlUniqueId["__LASTFOCUS"] = string.Empty;
                webFormsTest.PostDataByControlUniqueId["__EVENTARGUMENT"] = string.Empty;
                webFormsTest.PostDataByControlUniqueId["__EVENTTARGET"] = control.UniqueID;
                var radioButton = (RadioButton)control;
                if (!radioButton.Checked)
                {
                    webFormsTest.PostDataByControlUniqueId[control.UniqueID] = control.UniqueID;
                }
            }
            else if (control is CheckBox)
            {
                webFormsTest.PostDataByControlUniqueId["__LASTFOCUS"] = string.Empty;
                webFormsTest.PostDataByControlUniqueId["__EVENTARGUMENT"] = string.Empty;
                webFormsTest.PostDataByControlUniqueId["__EVENTTARGET"] = control.UniqueID;
                var checkBoxControl = (ICheckBoxControl)control;

                // Invert checked state
                if (!checkBoxControl.Checked)
                {
                    webFormsTest.PostDataByControlUniqueId[control.UniqueID] = "on";
                }
            }
            else if (control is ITextControl)
            {
                var textControl = (ITextControl)control;
                webFormsTest.PostDataByControlUniqueId["__LASTFOCUS"] = string.Empty;
                webFormsTest.PostDataByControlUniqueId["__EVENTARGUMENT"] = string.Empty;
                webFormsTest.PostDataByControlUniqueId["__EVENTTARGET"] = control.UniqueID;
                webFormsTest.PostDataByControlUniqueId[control.UniqueID] = textControl.Text;
            }
            else if (control is ListControl)
            {
                throw new Exception("Control " + control + " requires an index to AutoPostBack().");
            }
            else
            {
                throw new Exception("Control " + control + " does not support AutoPostBack().");
            }
        }

        public void AutoPostBack(Control control, WebformsTest webFormsTest, int index)
        {
            if (control is CheckBoxList)
            {
                webFormsTest.PostDataByControlUniqueId["__LASTFOCUS"] = string.Empty;
                webFormsTest.PostDataByControlUniqueId["__EVENTARGUMENT"] = string.Empty;
                webFormsTest.PostDataByControlUniqueId["__EVENTTARGET"] = control.UniqueID + "$" + index;
                webFormsTest.PostDataByControlUniqueId[control.UniqueID + "$" + index] = "on";
            }
            else if (control is ListControl)
            {
                var listControl = (ListControl)control;
                var listItem = listControl.Items[index];
                webFormsTest.PostDataByControlUniqueId["__LASTFOCUS"] = string.Empty;
                webFormsTest.PostDataByControlUniqueId["__EVENTARGUMENT"] = string.Empty;
                webFormsTest.PostDataByControlUniqueId["__EVENTTARGET"] = control.UniqueID;
                webFormsTest.PostDataByControlUniqueId[control.UniqueID] = listItem.Value;
            }
            else if (control is ITextControl)
            {
                var textControl = (ITextControl)control;
                webFormsTest.PostDataByControlUniqueId["__LASTFOCUS"] = string.Empty;
                webFormsTest.PostDataByControlUniqueId["__EVENTARGUMENT"] = string.Empty;
                webFormsTest.PostDataByControlUniqueId["__EVENTTARGET"] = control.UniqueID;
                webFormsTest.PostDataByControlUniqueId[control.UniqueID] = textControl.Text;
            }
            else if (control is ICheckBoxControl)
            {
                throw new Exception("Control " + control + " can not have an index to AutoPostBack().");
            }
            else
            {
                throw new Exception("Control " + control + " does not support AutoPostBack().");
            }
        }
    }
}