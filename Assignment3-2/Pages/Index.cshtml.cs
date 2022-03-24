using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment3_2.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Assignment3_2.Pages
{
    public class IndexModel : PageModel
    {
        [ViewData]
        public List<Student> students { get; set; }

        private readonly ILogger<IndexModel> _logger;

        private readonly dat154Context _context;

        public IndexModel(ILogger<IndexModel> logger, dat154Context context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task OnGetTaskAsync()
        {
            students = await _context.Student.ToListAsync();
        }

    }
}
