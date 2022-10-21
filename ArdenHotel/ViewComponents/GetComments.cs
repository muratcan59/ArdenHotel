using ArdenHotel.Data;
using ArdenHotel.Entities;
using ArdenHotel.Models;
using ArdenHotel.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArdenHotel.ViewComponents
{
    public class GetComments : ViewComponent
    {
        private ApplicationDbContext context;

        public GetComments(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = (from comment in context.Set<Comment>()
                        where comment.IsMainPageShow == true
                        select comment).ToList();
            return View(data);
        }
    }
}
