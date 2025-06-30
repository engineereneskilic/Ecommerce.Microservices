using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using FreeCourse.Services.Order;

namespace FreeCourse.Shared.Dtos
{
    public class ResponseDto<T>
    {
        public T Data { get; private set; }

        [JsonIgnore]
        public int StatusCode { get; private set; }

        [JsonIgnore]
        public bool IsSuccessfull { get; set; }

        public List<string> Errors { get; private set; }



        public static ResponseDto<T> Success(T data, int statusCode)
        {
            return new ResponseDto<T> { Data = data, StatusCode = statusCode, IsSuccessfull = true };
        }
        public static ResponseDto<T> Success(int statusCode)
        {
            return new ResponseDto<T> {StatusCode = statusCode, IsSuccessfull = true };
        }

        //// Hatalı dönüş - birden fazla hata
        //public static ResponseDto<T> Fail(List<string> errors, int statusCode)
        //{
        //    return new ResponseDto<T>
        //    {
        //        Errors = errors,
        //        StatusCode = statusCode,
        //        IsSuccessfull = false
        //    };
        //}


        //public static ResponseDto<T> Fail(List<string> errors, int statusCode)
        //{
        //    return new ResponseDto<T> { Errors = errors, StatusCode = statusCode, IsSuccessfull = false };
        //}

        //public static ResponseDto<T> Fail(string error, int statusCode)
        //{
        //    return new ResponseDto<T> { Errors= new List<string>{error}, StatusCode = statusCode,IsSuccessfull = false};
        //}

        public static ResponseDto<T> Fail(List<string> errors, int statusCode)
        {
            return new ResponseDto<T>
            {
                Errors = errors,
                StatusCode = statusCode,
                IsSuccessfull = false
            };
        }


    }
}
