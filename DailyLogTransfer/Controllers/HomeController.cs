using DailyLogTransfer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;


namespace DailyLogTransfer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult ManageLogs()
        {
            return View();
        }

        public IActionResult GetTodaysLog()
         {
            List<FileInformation> ResultFiles = null;
            const string LogInfoPath = @"D:\PROJECT\PIE\HDFCERGO.PIP\HDFCERGO.PIP.RestServices\Log\LogInfo";
            DateTime currentDate = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy"));
            try
            {
                DirectoryInfo directory = new(@LogInfoPath);
                try
                {
                    FileInfo[] Files = directory.GetFiles("*.log");
                    foreach (var file in Files)
                    {
                        if (DateTime.Parse(file.CreationTime.ToString("dd/MM/yyyy")) == currentDate)
                        {
                            Console.WriteLine(file.Length);
                            Console.WriteLine(DateTime.Parse(file.CreationTime.ToString("dd/MM/yyyy")));
                            ResultFiles.Add(new FileInformation
                            {
                                FileName = file.Name,
                                FullPath = file.FullName,
                                CreationDate = file.CreationTime,
                                LastModificationDate = file.LastWriteTime
                            });
                        }
                    }
                }
                catch (FileNotFoundException)
                {

                    ViewBag.ErrorMsg = "File Not Found";
                }
                
            }
            catch (DirectoryNotFoundException)
            {
                ViewBag.ErrorMsg = "Directory Not Found";
                return View("~/Views/Home/LogListPartialView.cshtml", ResultFiles);
            }
            if (ResultFiles == null)
            {
                ViewBag.ErrorMsg = "File Not Found";
            }
            return View("~/Views/Home/LogListPartialView.cshtml",ResultFiles);
        }

        public IActionResult TransferFiles(string FileName, string FullPath)
        {
            string sourceFile = @FullPath;
            string[] DestinationFiles = {
                @"D:\PROJECT\PIE\HDFCERGO.PIP\HDFCERGO.PIP.RestServices\Log\server1\" +FileName,
                @"D:\PROJECT\PIE\HDFCERGO.PIP\HDFCERGO.PIP.RestServices\Log\server2\" +FileName,
                @"D:\PROJECT\PIE\HDFCERGO.PIP\HDFCERGO.PIP.RestServices\Log\server3\" +FileName,
                @"D:\PROJECT\PIE\HDFCERGO.PIP\HDFCERGO.PIP.RestServices\Log\server4\" +FileName,
                @"D:\PROJECT\PIE\HDFCERGO.PIP\HDFCERGO.PIP.RestServices\Log\server5\" +FileName,
                @"D:\PROJECT\PIE\HDFCERGO.PIP\HDFCERGO.PIP.RestServices\Log\server6\" +FileName,
                @"D:\PROJECT\PIE\HDFCERGO.PIP\HDFCERGO.PIP.RestServices\Log\server7\" +FileName,
                @"D:\PROJECT\PIE\HDFCERGO.PIP\HDFCERGO.PIP.RestServices\Log\server8\" +FileName
            };

            try
            {
                foreach (var destFile in DestinationFiles)
                {
                    System.IO.File.Copy(sourceFile, destFile, true);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e);
                return View("~/Views/Home/ManageLogs.cshtml");
            }
            ViewBag.TaskDoneMsg = "Task Done";

            return View("~/Views/Home/ManageLogs.cshtml");
        }
    }

}
