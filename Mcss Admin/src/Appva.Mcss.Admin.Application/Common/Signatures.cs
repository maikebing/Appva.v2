// <copyright file="Signatures.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>

namespace Appva.Mcss.Admin.Application.Common
{
    #region Imports.
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.Mcss.Admin.Application.Models;
    #endregion

    public static class Signatures
    {
        #region Properties

        /// <summary>
        /// Icon for competed signature
        /// </summary>
        public static readonly SignatureImage CompletedImage = Signatures.CreateSignatureImage("icn-ok.png", 1, "done");

        /// <summary>
        /// Icon for sent signature
        /// </summary>
        public static readonly SignatureImage SentImage = Signatures.CreateSignatureImage("icn-sent.png", 5, "sent");

        /// <summary>
        /// Icon for NotGiven signature
        /// </summary>
        public static readonly SignatureImage NotGivenImage = Signatures.CreateSignatureImage("icn-none.png", 3, "ungiven");

        /// <summary>
        /// Icon for cant recive signature
        /// </summary>
        public static readonly SignatureImage CantReciveImage = Signatures.CreateSignatureImage("icn-nothx.png", 4, "nothx");

        /// <summary>
        /// Icon for part signature
        /// </summary>
        public static readonly SignatureImage PartlyGivenImage = Signatures.CreateSignatureImage("icn-part.png", 2, "part");

        #endregion      
  
        #region Internal static members

        /// <summary>
        /// Creates a signature
        /// </summary>
        /// <param name="image"></param>
        /// <param name="sort"></param>
        /// <param name="cssClass"></param>
        /// <returns></returns>
        internal static SignatureImage CreateSignatureImage(string image, int sort, string cssClass)
        {
            return new SignatureImage
            {
                Image    = image,
                Sort     = sort,
                CssClass = cssClass
            };
        }

        #endregion
    }

    public class SignatureImage
    {
        /// <summary>
        /// The image name
        /// </summary>
        public string Image
        {
            get;
            set;
        }

        /// <summary>
        /// The image weight
        /// </summary>
        public int Sort
        {
            get;
            set;
        }

        /// <summary>
        /// CSS class which should be assosiated with the image
        /// </summary>
        public string CssClass
        {
            get;
            set;
        }
    }
}
