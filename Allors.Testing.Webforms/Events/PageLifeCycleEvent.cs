// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PageLifeCycleEvent.cs" company="allors bvba">
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
    public enum PageLifeCycleEvent
    {
        /// <summary>
        /// Raises the AbortTransaction event. (Inherited from TemplateControl.)
        /// </summary>
        OnAbortTransaction, 

        /// <summary>
        /// Determines whether the event for the server control is passed up the page's UI server control hierarchy. (Inherited from Control.) 
        /// </summary>
        OnBubbleEvent, 

        /// <summary>
        /// Raises the CommitTransaction event. (Inherited from TemplateControl.)
        /// </summary>
        OnCommitTransaction, 

        /// <summary>
        /// Raises the DataBinding event. (Inherited from Control.)
        /// </summary>
        OnDataBinding, 

        /// <summary>
        /// Raises the Error event. (Inherited from TemplateControl.)
        /// </summary>
        OnError, 

        /// <summary>
        /// Raises the Init event to initialize the page. (Overrides Control..::.OnInit(EventArgs).)
        /// </summary>
        OnInit, 

        /// <summary>
        /// Raises the InitComplete event after page initialization.
        /// </summary>
        OnInitComplete, 

        /// <summary>
        /// Raises the Load event. (Inherited from Control.)
        /// </summary>
        OnLoad, 

        /// <summary>
        /// Raises the LoadComplete event at the end of the page load stage.
        /// </summary>
        OnLoadComplete, 

        /// <summary>
        /// Raises the PreInit event at the beginning of page initialization.
        /// </summary>
        OnPreInit, 

        /// <summary>
        /// Raises the PreLoad event after postback data is loaded into the page server controls but before the OnLoad event.
        /// </summary>
        OnPreLoad, 

        /// <summary>
        /// Raises the PreRender event. (Inherited from Control.)
        /// </summary>
        OnPreRender, 

        /// <summary>
        /// Raises the PreRenderComplete event after the OnPreRenderComplete event and before the page is rendered.
        /// </summary>
        OnPreRenderComplete, 

        /// <summary>
        /// Raises the SaveStateComplete event after the page state has been saved to the persistence medium.
        /// </summary>
        OnSaveStateComplete, 

        /// <summary>
        /// Raises the Unload event. (Inherited from Control.) 
        /// </summary>
        OnUnload, 
    }
}