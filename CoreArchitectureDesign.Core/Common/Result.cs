using CoreArchitectureDesign.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreArchitectureDesign.Core.Common
{
    /// <summary>
    /// This class used to data flow between the data layer and the business layer
    /// </summary>
    /// <typeparam name="T">This is a model which is used in business layer</typeparam>
    public class Result<T>
    {
        public Result()
        {
            ResultStatus = true;
            ResultCode = (int)ResultStatusCodes.Ok;
            ResultMessage = ResultStatusCodes.Ok.ToString();
        }

        /// <summary>
        /// It is used for to get data that is return from business layer.
        /// </summary>
        public T ResultObject { get; set; }

        /// <summary>
        /// If it is problem when getting data from bussiness layer, err message can set to this field to show on presentation layer.
        /// </summary>
        public string ResultMessage { get; set; }

        /// <summary>
        /// This field for detailed message
        /// </summary>
        public string ResultInnerMessage { get; set; }

        /// <summary>
        /// Success/fail status can set to this field.
        /// </summary>
        public bool ResultStatus { get; set; }

        /// <summary>
        /// ResultStatusEnum codes can set to this field.
        /// </summary>
        public int ResultCode { get; set; }
    }
}
