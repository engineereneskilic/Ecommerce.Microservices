﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Shared.ControllerBases
{
    public class CustomBaseController  : ControllerBase
    {
        public IActionResult CreateActionResultInstance<T>(ResponseDto<T> response)
        {
            return new ObjectResult(response)
            {
               
                StatusCode = response.StatusCode,

            };
        }
    }
}
