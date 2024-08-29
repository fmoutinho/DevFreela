﻿using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.UserQueries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<ResultViewModel<List<UserViewModel>>>
    {
        public GetAllUsersQuery() { }
    }
}
