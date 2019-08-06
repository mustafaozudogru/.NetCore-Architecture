using System;
using System.Collections.Generic;
using System.Text;

namespace CoreArchitectureDesign.Core.Enums
{
    public enum ResultStatusCodes
    {
        /// <summary>
        /// Success
        /// </summary>
        Ok = 200,
        /// <summary>
        /// Unauthorized
        /// </summary>
        Unauthorized = 401,
        /// <summary>
        /// Forbidden
        /// </summary>
        Forbidden = 403,
        /// <summary>
        /// NotFound
        /// </summary>
        NotFound = 404,
        /// <summary>
        /// InternalServerError
        /// </summary>
        InternalServerError = 500,
        /// <summary>
        /// ExistingItem
        /// </summary>
        ExistingItem = 600,
        /// <summary>
        /// Warning
        /// </summary>
        Warning = 700,
        /// <summary>
        /// Info
        /// </summary>
        Info = 800
    }
}
