using Microsoft.AspNetCore.Mvc;
using Mmr.InterviewNet.Models;
using Mmr.InterviewNet.Services;
using System.Diagnostics;

namespace Mmr.InterviewNet.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDirectoryScanner _scanner;

        public HomeController(IDirectoryScanner scanner)
        {
            _scanner = scanner;
        }

        public IActionResult Index()
        {
            return View(new ScanDirectoryModel());
        }

        public IActionResult ScanDirectoryForm(ScanDirectoryModel model)
        {
            if (!Directory.Exists(model.DirectoryPath))
            {
                model.ErrorMessage = "Directory does not exist.";
                return View("Index", model);
            }

            model.Changes = _scanner.Scan(model.DirectoryPath);
            return View("Index", model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
