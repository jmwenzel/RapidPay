using Microsoft.AspNetCore.Mvc;
using RapidPay.App.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace RapidPay.App.Controllers
{
    /// <summary>
    /// Base Controller
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// Response Wrapper
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <param name="errorMessage"></param>
        /// <param name="successMessage"></param>
        /// <returns></returns>
        protected async Task<ApiResponse<T>> ExecuteAsync<T>
            (Func<Task<T>> method, string errorMessage = null, string successMessage = null)
        {
            var response = new ApiResponse<T>();

            try
            {
                var result = await method.Invoke();
                response.Data = result;
                if (result != null)
                {
                    response.AddSuccessMessage(successMessage);
                }
                else
                {
                    response.AddSuccessMessage("No results to display");
                }
            }
            catch (KeyNotFoundException ex)
            {
                this.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                HandlerErrorLog(ex, errorMessage, response);
            }
            catch (NotImplementedException ex)
            {
                this.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotImplemented;
                HandlerErrorLog(ex, errorMessage, response);
            }
            catch (NotSupportedException ex)
            {
                this.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotImplemented;
                HandlerErrorLog(ex, errorMessage, response);
            }
            catch (Exception ex)
            {
                this.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                HandlerErrorLog(ex, errorMessage, response);
            }

            return response;
        }

        private void HandlerErrorLog<T>(Exception ex,
            string defaultErrorMessage,
            ApiResponse<T> response)
        {
            if (string.IsNullOrEmpty(defaultErrorMessage))
                defaultErrorMessage = "An error occurred while processing the request.";

            if (ex is KeyNotFoundException || ex is FormatException || ex is NotImplementedException || 
                ex is NotSupportedException || ex is ArgumentException)
                defaultErrorMessage = defaultErrorMessage + " " + ex.Message;

            response.AddErrorMessage($"{defaultErrorMessage}");
        }
    }
}
