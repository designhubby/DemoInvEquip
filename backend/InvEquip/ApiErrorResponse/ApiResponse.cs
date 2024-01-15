using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//https://lurumad.github.io/problem-details-an-standard-way-for-specifying-errors-in-http-api-responses-asp.net-core

namespace InvEquip.ApiErrorResponse
{
    public static class ApiResponse
    {
        private static int _status;
        private static string _detail;
        private static string _title = "Error Occurred";
        private static ModelStateDictionary _modelErrors;
        public static int Status { 
            get
            {
                return _status;
            }
        }
        public static ObjectResult StatusCode424
        {
            get
            {
                return Response(424, GetMessageFromStatusCode(424), null);
            }
        }
        public static ObjectResult StatusCode404(string title = null)
        {

            return Response(404, title, title);
            
        }
        public static ObjectResult StatusCode500(string title = null)
        {

            return Response(500, title, title);

        }
        public static ObjectResult BadRequest(string title = null)
        {
            return Response(400, title);
        }

        public static ObjectResult BadRequest(ModelStateDictionary modelErrors , string title = null)
        {
            return Response(400, modelErrors, title);
        }


        private static string GetMessageFromStatusCode(int status)
        {
            return status switch
            {
                400=>"Bad Request",
                404 => "Not Found",
                500 => "An unhandled error occured",
                424 => "Dependency Fail",
                _=> null,
            };
        }
        public static ObjectResult Response(int statuscode, string title = null, string detail = null)
        {
            _status = statuscode;
            _detail = detail ?? GetMessageFromStatusCode(statuscode);
            _title = title ?? GetMessageFromStatusCode(statuscode);

            ProblemDetails problemDetails = new ProblemDetails
            {
                Status = statuscode,
                Title = _title,
                Detail = _detail,
            };

            return new ObjectResult(problemDetails)
            {
                ContentTypes = { "application/problem+json" },
                StatusCode = statuscode,
            };
        }
        public static ObjectResult Response(int statuscode, ModelStateDictionary modelErrors, string title = null, string detail = null)
        {
            _status = statuscode;
            _detail = detail ?? GetMessageFromStatusCode(statuscode);
            _title = title ?? GetMessageFromStatusCode(statuscode);
            _modelErrors = modelErrors;

            ValidationProblemDetails problemDetails = new ValidationProblemDetails(_modelErrors)
            {
                Status = statuscode,
                Title = _title,
                Detail = _detail,
            };

            return new ObjectResult(problemDetails)
            {
                ContentTypes = { "application/problem+json" },
                StatusCode = statuscode,
            };
        }
    }
}
