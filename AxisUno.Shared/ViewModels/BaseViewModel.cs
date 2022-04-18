// <copyright file="BaseViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.ViewModels
{
    using System;
    using CommunityToolkit.Mvvm.ComponentModel;

    /// <summary>
    /// Describes base properties of a page.
    /// </summary>
    public class BaseViewModel : ObservableObject
    {
        private string? pageId = null;
        private string title;

        /// <summary>
        /// Gets id of the page.
        /// </summary>
        /// <date>15.04.2022.</date>
        public string PageId => this.pageId ?? (this.pageId = Guid.NewGuid().ToString());

        /// <summary>
        /// Gets or sets title of the page.
        /// </summary>
        /// <date>24.03.2022.</date>
        public string Title
        {
            get => this.title;
            set
            {
                this.SetProperty(ref this.title, value);

                if (this.PageTitleChanging != null)
                {
                    this.PageTitleChanging.Invoke(value);
                }
            }
        }

        /// <summary>
        /// Describes structure of the PageClosing event.
        /// </summary>
        /// <param name="pageId">Id of the page.</param>
        /// <date>15.04.2022.</date>
        public delegate void PageClosingDelegate(string pageId);

        /// <summary>
        /// Tells about closing of a page.
        /// </summary>
        /// <date>15.04.2022.</date>
        public PageClosingDelegate PageClosing;

        /// <summary>
        /// Describes structure of PageTitleChanged event.
        /// </summary>
        /// <param name="newPageTitle">New title of a page.</param>
        /// <date>15.04.2022.</date>
        public delegate void PageTitleChangingDelegate(string newPageTitle);

        /// <summary>
        /// Tells about changing of a title of page.
        /// </summary>
        /// <date>15.04.2022.</date>
        public PageTitleChangingDelegate PageTitleChanging;
    }
}
