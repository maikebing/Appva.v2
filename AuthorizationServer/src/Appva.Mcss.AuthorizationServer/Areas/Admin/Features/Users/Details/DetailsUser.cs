// <copyright file="DetailsUser.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models
{
    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class DetailsUser : Identity
    {
        /// <summary>
        /// Whether the user is active or not.
        /// </summary>
        public bool IsActive
        {
            get;
            set;
        }

        /// <summary>
        /// The user model meta data.
        /// </summary>
        public MetaData Resource
        {
            get;
            set;
        }

        /// <summary>
        /// The status of the user.
        /// E.g. verified, pending, banned.
        /// FIXME: Must be shown some how.
        /// </summary>
        public string Status
        {
            get;
            set;
        }

        /// <summary>
        /// A unique identitier for a user.
        /// E.g. for swedish citizens: personnummer.
        /// </summary>
        public string PersonalIdentityNumber
        {
            get;
            set;
        }

        /// <summary>
        /// The users' first name.
        /// E.g. "John".
        /// </summary>
        public string FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// The users' last name.
        /// E.g. "Doe".
        /// </summary>
        public string LastName
        {
            get;
            set;
        }

        /// <summary>
        /// The user's full name.
        /// </summary>
        public string FullName
        {
            get;
            set;
        }

        /// <summary>
        /// The users' e-mail address.
        /// E.g. "example@example.com".
        /// </summary>
        public string EmailAddress
        {
            get;
            set;
        }

        /// <summary>
        /// The users' mobile phone number.
        /// E.g. "+4670000000".
        /// </summary>
        public string MobilePhoneNumber
        {
            get;
            set;
        }

        /// <summary>
        /// The user's profile picture.
        /// </summary>
        public ProfilePicture ProfilePicture
        {
            get;
            set;
        }
    }
}